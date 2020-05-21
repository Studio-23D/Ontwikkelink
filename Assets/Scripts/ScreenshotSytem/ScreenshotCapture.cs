using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script is part of the sreenshot system.
///  It captures a screenshot, with a custom resolution, and stores it locally- to later be processed by a screenshot saving system.
///  Needs input to be called
/// </summary> 

public class ScreenshotCapture : MonoBehaviour
{
    [SerializeField] private Image blinkImage;
    [SerializeField] private Camera screenshotCamera;
    [SerializeField] private bool isRunningAndroid;
    [SerializeField] private bool blink;
    [SerializeField] private GameObject[] UIElements;

    private ScreenshotSaving screenshotSaving;

    private WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    private float blinkTime = 0.20f;

    private void Awake()
    {
        screenshotSaving = this.gameObject.GetComponent<ScreenshotSaving>();
        blinkImage.enabled = false;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android) { isRunningAndroid = true; }

    #if UNITY_ANDROID
        isRunningAndroid = true;
    #else 
        isRunningAndroid  = false;
    #endif
    }

    public void TakeScreenshotWithButton()
    {
        TakeScreenshot();
    }

    private void TakeScreenshot()
    {
        if (isRunningAndroid) { StartCoroutine(ScreenshotProcessMobile()); }
        else { StartCoroutine(ScreenshotProcessDesktop(screenshotCamera.pixelWidth, screenshotCamera.pixelHeight)); }
    }

    private IEnumerator ScreenshotProcessDesktop(int width, int height)
    {
        VisibleUI(UIElements, false);

        screenshotCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);

        yield return frameEnd;

        RenderTexture renderTexture = screenshotCamera.targetTexture;

        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        renderResult.ReadPixels(rect, 0, 0);

        byte[] byteArray = renderResult.EncodeToPNG();

        screenshotSaving.SaveImage(byteArray);

        RenderTexture.ReleaseTemporary(renderTexture);
        screenshotCamera.targetTexture = null;

        if (blink) { StartCoroutine(Blink(blinkImage, blinkTime)); }

        VisibleUI(UIElements, true);
    }

    private IEnumerator ScreenshotProcessMobile()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy");
        string fileName = ("Screenshot " + timeStamp + ".png");

        ScreenCapture.CaptureScreenshot(fileName);

        yield return frameEnd;
    }

    private IEnumerator Blink(Image image, float time)
    {
        image.enabled = true;
        yield return new WaitForSeconds(time);
        image.enabled = false;
    }

    private void VisibleUI(GameObject[] elementArray, bool elementsActive)
    {
        float alpha = 1.0f;

        if (!elementsActive) { alpha = 0.0f; }
        else { alpha = 1.0f; }

        for (int i = 0; i < elementArray.Length; i++) { elementArray[i].GetComponent<CanvasRenderer>().SetAlpha(alpha); }

        // ADD: check if the element in the array is a button and disable the child (text) aswell
    }
}
