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

    private void Start() {
        creditTextObject.text = creditText;
    }
    public void GoBackToPark()
    {
        SceneManager.LoadScene("MainScene");
    }
}
