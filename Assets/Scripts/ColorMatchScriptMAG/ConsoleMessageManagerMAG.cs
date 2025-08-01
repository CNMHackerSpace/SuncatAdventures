using UnityEngine;
using System;
using System.Collections;

public class ConsoleMessageManagerMAG : MonoBehaviour
{
    private bool muted = false;

    // For double Esc quit
    private bool escPressedOnce = false;
    private float escTimer = 0f;
    public float escTimeout = 2f;  // Seconds allowed between presses

    void Start()
    {
        StartCoroutine(LogMessageHourly());
    }

    IEnumerator LogMessageHourly()
    {
        while (true)
        {
            if (!muted)
            {
                Debug.Log("Press ESC to quit");
            }

            // Calculate time until next hour
            DateTime now = DateTime.Now;
            DateTime nextHour = now.AddHours(1).Date.AddHours(now.AddHours(1).Hour);
            TimeSpan waitTime = nextHour - now;

            yield return new WaitForSeconds((float)waitTime.TotalSeconds);
        }
    }

    void Update()
    {
        // Toggle console message mute
        if (Input.GetKeyDown(KeyCode.M))
        {
            muted = !muted;
            Debug.Log(muted ? "Notifications muted." : "Notifications unmuted.");
        }

        // Double ESC press to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPressedOnce)
            {
                escPressedOnce = true;
                escTimer = 0f;

                // Show cursor on first Esc press
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                Debug.Log("Press ESC again within 2 seconds to quit.");
            }
            else
            {
                Debug.Log("Quitting game...");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        // Count timer for second Esc press
        if (escPressedOnce)
        {
            escTimer += Time.deltaTime;
            if (escTimer > escTimeout)
            {
                escPressedOnce = false;
                Debug.Log("Esc timeout expired. Quit canceled.");
            }
        }
    }
}
