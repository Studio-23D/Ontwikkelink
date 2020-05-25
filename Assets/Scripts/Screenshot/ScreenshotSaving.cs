using System.IO;
using UnityEngine;

/// <summary>
///  This script is part of the sreenshot system.
///  It saves a screenshot, and stores system data at the given data path.
/// </summary> 

public class ScreenshotSaving : MonoBehaviour
{
	public string GetImageFolderPath => imageFolderPath;
	public string GetImageFolderName => imageFolderName;
	public string GetImagePath => Path.Combine(imageFolderPath, lastImageName);

	[SerializeField] private ColorFader savedScreenshotWindow = null;
	[SerializeField] private string imageFolderName = "Screenshots";
	[SerializeField] private string galleryAlbumName = "Ontwikkelink";
	[SerializeField] private string imageExtension = ".png";

	private string lastImageName = "";
	private string imageFolderPath = "";

	private void Start()
	{
		InitDirectory();
	}

	public void SaveImage(byte[] byteArrayToSave)
    {
        File.WriteAllBytes(Path.Combine(imageFolderPath, GetImageName()), byteArrayToSave);
		lastImageName = GetImageName();
	}


	public void SaveImageToGallery(byte[] data, string imageName)
	{
		savedScreenshotWindow.gameObject.SetActive(false);
		savedScreenshotWindow.gameObject.SetActive(true);

		if (SystemInfo.deviceType != DeviceType.Handheld)
		{
			print("Saved image to gallary");
		}
		else
		{
			NativeGallery.SaveImageToGallery(data, galleryAlbumName, imageName);
		}
	}

	private void InitDirectory()
	{
		imageFolderPath = Path.Combine(Application.persistentDataPath, imageFolderName);
		CheckDirectory(imageFolderPath);
	}

	private string GetImageName()
    {
        string imageID = "(" + GetImageAmount(imageFolderPath) + ")";
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy");
        string fileName = ("Ontwikkelink " + timeStamp + " " + imageID + imageExtension);

        return fileName;
    }

    public void CheckDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

	private int GetImageAmount(string path)
	{
		DirectoryInfo info = new DirectoryInfo(path);
		FileInfo[] fileInfo = info.GetFiles();
		int imageAmount = 0;

		foreach (FileInfo f in fileInfo)
		{
			if (!f.Name.Contains(imageExtension)) continue;

			imageAmount++;
		}

		return imageAmount;
	}
}
