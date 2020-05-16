using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script is part of the sreenshot system.
///  It checks for an input event to call the screenshot functionality
/// </summary> 

public class ScreenshotButton : MonoBehaviour
{
    private Button screenshotButton;
    private ScreenshotCapture screenshotCapture;

    private void Awake()
    {
        screenshotButton = this.gameObject.GetComponent<Button>();
        screenshotCapture = this.gameObject.GetComponent<ScreenshotCapture>();
    }

    private void Start() { screenshotButton.onClick.AddListener(TakeScreenshot); }

    private void TakeScreenshot() { screenshotCapture.TakeScreenshotWithButton(); }
}
