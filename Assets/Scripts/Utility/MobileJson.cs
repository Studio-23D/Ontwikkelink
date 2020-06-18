using UnityEngine;
using System.IO;

/// <summary>
///  This script is responsible for writing and loading-in Json files on mobile.
///  This script is currently used in the 'ScreenshotSaving' script.
///  To use the 'LoadJsonFromResources' function: create a Json file will at the given data path (JSONFiles/Json/).
/// </summary> 

public class MobileJson : MonoBehaviour
{
    private string dataPath = Application.persistentDataPath;
    private string fileName = "Data";

    private string resourcePath = "JSONFiles/Json/";

    public void InitializeDataFile()
    {
        if(!File.Exists(dataPath + "/" + fileName + ".json")) { File.WriteAllText(dataPath + "/" + fileName + ".json", ""); }
    }

    public void WriteJson(JsonObject jsonObject)
    {
        string json = JsonUtility.ToJson(jsonObject);
        File.WriteAllText(dataPath + "/" + fileName + ".json", json);
    }

    public JsonObject LoadJson()
    {
        JsonObject jsonObject = new JsonObject();

        string json = File.ReadAllText(dataPath + "/" + fileName + ".json");
        JsonUtility.FromJsonOverwrite(json, jsonObject);

        return jsonObject;
    }

    public JsonObject LoadJsonFromResources()
    {
        JsonObject jsonObject = new JsonObject();

        string path = resourcePath + fileName + ".json";
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        string json = textAsset.text;
        JsonUtility.FromJsonOverwrite(json, jsonObject);

        return jsonObject;
    }
}

public class JsonObject
{
    public int id;

    public JsonObject(int index)
    {
        id = index;
    }

    public JsonObject() { }
}
