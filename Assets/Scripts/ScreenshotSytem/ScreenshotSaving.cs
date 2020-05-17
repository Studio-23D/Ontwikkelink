using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

/// <summary>
///  This script is part of the sreenshot system.
///  It saves a screenshot, and stores system data at the given data path.
/// </summary> 

public class ScreenshotSaving : MonoBehaviour
{
    private string destinationFolderImage;
    private string destinationFolderImageData;
    private string destinationFolderImageDataFile;

    private ushort imageID;

    private string imageDataFileName = "Data.txt";
    private string defaultValue = "0";

    private void Start()
    {
        InstantiateDirectory();
        imageID = GetImageData();
    }

    public void SaveImage(byte[] byteArrayToSave)
    {
        File.WriteAllBytes(destinationFolderImage + ImageName(), byteArrayToSave);
        SaveImageData();
    }

    private string ImageName()
    {
        string imageName = "(" + GetImageData() + ")";

        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy");
        string fileName = ("Screenshot " + timeStamp + " " + imageName + ".png");

        imageID++;

        SaveImageData();

        return fileName;
    }

    private void SaveImageData()
    {
        string data = imageID.ToString();
        File.WriteAllText(destinationFolderImageDataFile, data);
    }

    private ushort GetImageData()
    {
        string data = File.ReadAllText(destinationFolderImageDataFile);
        imageID = ushort.Parse(data);
        return imageID;
    }

    private void InstantiateDirectory()
    {
        destinationFolderImage = Application.dataPath + "/../Screenshots/";
        destinationFolderImageData = Application.dataPath + "/../Screenshots/Data/";
        destinationFolderImageDataFile = destinationFolderImageData + imageDataFileName;

        CheckDirectory(destinationFolderImage, false);
        CheckDirectory(destinationFolderImageData, true);
    }

    private void CheckDirectory(string directoryPath, bool fileInstantiation)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            if (fileInstantiation) { File.WriteAllText(directoryPath, defaultValue); }
        }
    }
}
