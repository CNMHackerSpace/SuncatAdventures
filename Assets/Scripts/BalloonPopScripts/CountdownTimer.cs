using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownLabel;
    private float elapsedTime;
    private float timeRemaining;
    private int minutes = 5;
    private int seconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 300.0f;
        this.countdownLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt((timeRemaining - elapsedTime) / 60);
        seconds = Mathf.FloorToInt((timeRemaining - elapsedTime) % 60);
        this.countdownLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
