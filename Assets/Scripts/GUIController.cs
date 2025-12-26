using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public string creditText = "";
    public TMP_Text creditTextObject;

    private void Start()
    {
        creditTextObject.text = creditText;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GoBackToPark();
        }
    }
    public void GoBackToPark()
    {
        Debug.Log("Going back to main scene");
        SceneManager.LoadScene("MainScene");
    }
}
