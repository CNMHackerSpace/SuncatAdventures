using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlightLight;
    private bool isOn = true;

    void Start()
    {
        if (flashlightLight == null)
            flashlightLight = GetComponentInChildren<Light>();
    }

    public void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
    }
}
