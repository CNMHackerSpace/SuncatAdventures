using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text balloonsRemaining;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private Text dartCounter; // This is the text field displaying dart count
    [SerializeField] private CountdownTimer countdownTimer;
    private int _score;

    private DartInventory dartInventory; // Reference to DartInventory script

    void Start()
    {
        _score = 0;
        settingsPopup.OnCloseClicked();

        // Find and store reference to DartInventory script
        dartInventory = FindObjectOfType<DartInventory>();
    }

    private void Update()
    {
        // Update dart count text
        if (dartCounter != null && dartInventory != null)
        {
            dartCounter.text = dartInventory.GetDartCount().ToString();
        }
    }

    private void OnEnemyHit()
    {
        _score += 1;
        balloonsRemaining.text = _score.ToString();
    }

    public void OnOpenSettings()
    {
        settingsPopup.Open();
        Debug.Log("Opening settings");
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }
}
