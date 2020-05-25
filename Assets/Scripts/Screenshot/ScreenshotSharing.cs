using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotSharing : MonoBehaviour
{
	public static string ShareSubject { get { return shareSubject; } }
	public static string ShareMessage { get { return shareMessage; } }

	private static string shareSubject = "Deel jouw ontwerp!";
	private static string shareMessage = "Kijk mijn mooie zelf ontworpen personage," +
										"helemaal zelf gemaakt en gedeeld met Ontwikkelink!";

	public static void ShareImage(string path)
	{
		ShareImage(ShareMessage, ShareSubject, path);
	}

	/// <summary>
	/// Opens share window to share code via external sources
	/// </summary>
	/// <param name="message"></param>
	/// <param name="subject"></param>
	private static void ShareImage(string message, string subject, string screenshotPath)
	{
#if UNITY_EDITOR
		print("SHARE IMAGE");
#elif UNITY_ANDROID
		//current activity context
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		//Create intent for action send
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

		//create file object of the screenshot captured
		AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", screenshotPath);

		//create FileProvider class object
		AndroidJavaClass fileProviderClass = new AndroidJavaClass("android.support.v4.content.FileProvider");

		object[] providerParams = new object[3];
		providerParams[0] = currentActivity;
		providerParams[1] = "com.studio23d.ontwikkelink.provider";
		providerParams[2] = fileObject;

		//instead of parsing the uri, will get the uri from file using FileProvider
		AndroidJavaObject uriObject = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", providerParams);

		//put image and string extra
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/png");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

		//additionally grant permission to read the uri
		intentObject.Call<AndroidJavaObject>("addFlags", intentClass.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION"));

		AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareSubject);
		currentActivity.Call("startActivity", chooser);
#endif
	}

}
