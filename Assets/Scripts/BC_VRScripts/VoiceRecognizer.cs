using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VoiceRecognizer : MonoBehaviour
{
    public string witServerToken = "XUAVXOLWCCM6FFFX6PPD7YB7B2GKB5VL";
    public TextMeshProUGUI transcriptText;
    public VideoPlayer linkStartVideo;
    public Canvas videoCanvas;
    public string sceneToLoad = "GameScene";

    private AudioClip micClip;
    private bool isRecording = false;
    private int sampleRate = 16000;
    private bool linkStartTriggered = false;

    void Start()
    {


        Debug.Log("VoiceRecognizer started");

        if (videoCanvas != null)
            videoCanvas.enabled = true;

        if (linkStartVideo != null)
        {
            linkStartVideo.Prepare();
            StartCoroutine(WaitForVideoToPrepare());
        }
        StartCoroutine(LoopVoiceCapture());
    }

    IEnumerator WaitForVideoToPrepare()
    {
        while (!linkStartVideo.isPrepared)
        {
            Debug.Log("Preparing video...");
            yield return null;
        }

        Debug.Log("Video prepared.");

        // If you want to hide the canvas now, do this (optional):
        videoCanvas.enabled = false;
    }

    IEnumerator LoopVoiceCapture()
    {
        while (!linkStartTriggered)
        {
            Debug.Log("Starting recording...");
            StartRecording();
            yield return new WaitForSeconds(5f);

            Debug.Log("Stopping recording...");
            StopRecording();
            yield return new WaitForSeconds(1f);
        }
    }

    void StartRecording()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("No microphone detected");
            return;
        }

        micClip = Microphone.Start(null, false, 5, sampleRate);
        isRecording = true;
    }

    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(null);
        isRecording = false;

        byte[] wavData = WavUtility.FromAudioClip(micClip);
        StartCoroutine(SendToWit(wavData));
    }

    IEnumerator SendToWit(byte[] wav)
    {
        string url = "https://api.wit.ai/speech?v=20230202";
        UnityWebRequest req = new UnityWebRequest(url, "POST");
        req.uploadHandler = new UploadHandlerRaw(wav);
        req.downloadHandler = new DownloadHandlerBuffer();

        string cleanedToken = witServerToken.Trim().Replace("\"", "").Replace("\n", "").Replace("\r", "");
        req.SetRequestHeader("Authorization", "Bearer " + cleanedToken);
        req.SetRequestHeader("Content-Type", "audio/wav");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            string json = req.downloadHandler.text;
            string parsedText = ParseWitResponse(json);
            Debug.Log("Wit.ai returned: " + parsedText);

            if (transcriptText != null)
                transcriptText.text = parsedText;

            if (!string.IsNullOrEmpty(parsedText) && parsedText.ToLower().Contains("link start"))
            {
                Debug.Log("'Link Start' detected.");
                linkStartTriggered = true;
                StartCoroutine(PlayLinkStartSequence());
            }
        }
        else
        {
            Debug.LogError("Wit.ai error: " + req.error);
        }
    }

    string ParseWitResponse(string json)
    {
        MatchCollection matches = Regex.Matches(json, "\"text\"\\s*:\\s*\"([^\"]+)\"");
        string bestResult = "";
        foreach (Match match in matches)
        {
            string current = match.Groups[1].Value;
            if (current.Length > bestResult.Length)
                bestResult = current;
        }

        return bestResult;
    }

    IEnumerator PlayLinkStartSequence()
    {
        Debug.Log("Starting video sequence...");

        if (videoCanvas != null)
            videoCanvas.enabled = true;

        if (linkStartVideo != null)
        {
            linkStartVideo.Play();
            Debug.Log("Video started...");

            float timeout = 15f;
            float elapsed = 0f;
            while (linkStartVideo.isPlaying && elapsed < timeout)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            Debug.Log("Video finished or timeout.");
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
