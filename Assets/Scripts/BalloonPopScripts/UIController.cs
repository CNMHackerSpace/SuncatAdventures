using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text balloonsRemaining;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private Text dartCounter;
    [SerializeField] private CountdownTimer countdownTimer;
    private int _score;

    void Awake()
    {
        //Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void OnDestroy()
    {
        //Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
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
    // Start is called before the first frame update

    void Start()
    {
        _score = 0;

        settingsPopup.OnCloseClicked();
       
    }

    // Update is called once per frame
    private void OnEnemyHit()
    {
        _score += 1;
        balloonsRemaining.text = _score.ToString();
    }
}
