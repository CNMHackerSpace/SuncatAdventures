using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    
    [SerializeField] private Button btnMainLevel;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnQuit;
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void OnCloseClicked()
    {
        this.gameObject.SetActive(false);
    }
    public void OnMainLevelClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }



    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }
}

