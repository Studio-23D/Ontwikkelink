using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script is part of the sreenshot system.
///  It captures a screenshot, with a custom resolution, and stores it locally- to later be processed by a screenshot saving system.
///  Needs input to be called
/// </summary> 

[RequireComponent(typeof(ScreenshotSaving), typeof(ScreenshotSharing))]
public class ScreenshotCapture : MonoBehaviour
{
    [SerializeField] private ViewManager viewManager;
	[SerializeField] private GameObject screenshotMenu;
    [SerializeField] private Image blinkImage;
	[SerializeField] private RawImage screenshotContainer;
	[SerializeField] private Camera screenshotCamera;
    [SerializeField] private GameObject[] UIElements;
    [SerializeField] private bool blink;
	[SerializeField] private float blinkTime = 0.20f;

    private ScreenshotSaving screenshotSaving;
	private Texture2D renderResult;
	private byte[] screenshotData;
	public string screenshotPath;
	public string screenshotName;

    int imageID = 1;

	private void Awake()
    {
        screenshotSaving = GetComponent<ScreenshotSaving>();
	}


    public void CaptureScreenshot()
	{
		screenshotCamera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
		StartCoroutine(ScreenshotProcess(screenshotCamera.pixelWidth, screenshotCamera.pixelHeight));
	}

	public void SetScreenshotTo(RawImage image)
	{
		image.texture = renderResult;
	}

	public void SaveScreenshotToGallery()
	{
		screenshotSaving.SaveImageToGallery(screenshotData, screenshotName);
	}

	public void SaveScreenshot()
	{
		screenshotSaving.SaveImage(screenshotData);
	}

	public void ShareScreenshot()
	{
		ScreenshotSharing.ShareImage(screenshotPath);
	}

	private IEnumerator ScreenshotProcess(int width, int height)
    {
		ActiveUIElements(false);

        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = screenshotCamera.targetTexture;
		renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
		Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

		renderResult.ReadPixels(rect, 0, 0);
		renderResult.Apply();
		SetScreenshotTo(screenshotContainer);

		screenshotData = renderResult.EncodeToPNG();

        RenderTexture.ReleaseTemporary(renderTexture);
        screenshotCamera.targetTexture = null;

		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			yield return new WaitForEndOfFrame();

			string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy");
			screenshotName = ("Screenshot_" + timeStamp + "_" + imageID + ".png");
			screenshotPath = Path.Combine(Application.persistentDataPath, screenshotName);

            imageID++;

			ScreenCapture.CaptureScreenshot(screenshotName, 1);
		}

		ActiveUIElements(true);

		viewManager.ChangeViewTo(screenshotMenu);
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

	private void ActiveUIElements(bool active)
	{
		foreach (GameObject element in UIElements)
		{
			element.SetActive(active);
		}
	}
}
