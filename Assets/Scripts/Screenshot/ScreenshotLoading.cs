using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script is resposible for loading in back-end files, instantiating containers and applying images to previews.
/// </summary> 

public class ScreenshotLoading : MonoBehaviour
{
    [SerializeField] private GameObject containerPrefab;

    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button enterButton;

    [SerializeField] private List<Image> previews;
    [SerializeField] private List<GameObject> containers;

    private string[] files = null;

    private float containerPreviewAmount = 4;

    private int fileIndex = 0;
    private int previewIndex = 0;
    private int containerIndex = 0;

    private int currentFileIndex = 0;
    private int currentContainerIndex = 0;

    private int containerAmount = 0;

    private bool initialSetupDone = false;

    [SerializeField] private Image inspectingBackground;
    [SerializeField] private Image inspectingPreview;

    public Image background
    {
        get { return inspectingBackground; }
        set { inspectingBackground = value; }
    }

    public Image preview
    {
        get { return inspectingPreview; }
        set { inspectingPreview = value; }
    }

    private void Start()
    {
        /* Apply functions to the buttons */
        backButton.onClick.AddListener(() => GalleryCirculation(backButton));
        nextButton.onClick.AddListener(() => GalleryCirculation(nextButton));
        enterButton.onClick.AddListener(UpdateGallery);

        /* Conditions at start for the first container */
        backButton.gameObject.SetActive(false);

        GetDirectoryFiles();

        if (files.Length > 0)
        {
            InstantiateContainers();

            for (int i = 0; i < files.Length; i++)
            {
                SetImagesOnPreviews(false);
            }
        }

        /* Condition if there is only one container */
        if (containers.Count <= 1) { nextButton.gameObject.SetActive(false); }
    }

    private void InstantiateContainers()
    {
        containerAmount = Mathf.CeilToInt(files.Length / containerPreviewAmount);

        /* Instantiates the prefabs */
        for (int i = 0; i < containerAmount; i++)
        {
            GameObject instantiatedContainer = Instantiate(containerPrefab);
            instantiatedContainer.transform.SetParent(this.gameObject.transform, false);

            containers.Add(instantiatedContainer);
        }

        /* Gets the all the children images */
        for (int i = 0; i < containers.Count; i++)
        {
            for (int j = 0; j < containers[i].transform.childCount; j++)
            {
                previews.Add(containers[i].gameObject.transform.GetChild(j).GetComponent<Image>());
            }
        }

        /* Deactivates the remaining containers */
        for (int i = 1; i < containers.Count; i++)
        {
            containers[i].SetActive(false);
        }

        currentContainerIndex = containers.Count;
        currentFileIndex = files.Length;

        initialSetupDone = true;
    }

    private void SetImagesOnPreviews(bool update)
    {
        string filePath = files[fileIndex];

        Texture2D texture = GetScreenshotImage(filePath);
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 position = new Vector2(0.5f, 0.5f);

        Sprite sprite = Sprite.Create(texture, rect, position);

        previews[previewIndex].sprite = sprite;

        fileIndex++;
        previewIndex = fileIndex;
    }

    private void UpdateGallery()
    {
        if (initialSetupDone)
        {
            GetDirectoryFiles();

            /* Conditions if new files have been added */
            if (files.Length > currentFileIndex)
            {
                /* Calculates the current amount of containers */
                int newContainerAmount = Mathf.CeilToInt(files.Length / containerPreviewAmount);
                int containerDifference = newContainerAmount - currentContainerIndex;

                /* Adding the new containers */
                if (containerDifference > 0)
                {
                    for (int i = 0; i < containerDifference; i++)
                    {
                        GameObject instantiatedContainer = Instantiate(containerPrefab);
                        instantiatedContainer.transform.SetParent(this.gameObject.transform, false);

                        containers.Add(instantiatedContainer);
                    }

                    for (int j = currentContainerIndex; j < currentContainerIndex + containerDifference; j++)
                    {
                        for (int k = 0; k < containers[j].transform.childCount; k++)
                        {
                            previews.Add(containers[j].gameObject.transform.GetChild(k).GetComponent<Image>());
                        }

                        containers[j].SetActive(false);
                    }

                    currentContainerIndex = containers.Count;
                    nextButton.gameObject.SetActive(true);
                }

                /* Calculates the difference between current and new files */
                int fileDifference = files.Length - currentFileIndex;

                currentFileIndex += 1;

                /* Adding the new files to the previews*/
                for (int i = currentFileIndex; i < currentFileIndex + fileDifference; i++)
                {
                    SetImagesOnPreviews(true);
                }
            }
        }
    }

    private void GalleryCirculation(Button type)
    {
        containers[containerIndex].SetActive(false);
            
        if (type == backButton) { containerIndex--; }
        else if (type == nextButton) { containerIndex++; }

        if (containerIndex == 0) { backButton.gameObject.SetActive(false); }
        if (containerIndex > 0) { backButton.gameObject.SetActive(true); }

        /* Conditions if the current container is the last container */
        if (containerIndex == (containers.Count - 1)) { nextButton.gameObject.SetActive(false); }
        else { nextButton.gameObject.SetActive(true); }

        containers[containerIndex].SetActive(true);
    }

    private void GetDirectoryFiles()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
    }

    private Texture2D GetScreenshotImage(string path)
    {
        int width = 2, height = 2;

        Texture2D texture = null;

        byte[] fileBytes;

        if (File.Exists(path))
        {
            fileBytes = File.ReadAllBytes(path);
            texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }

        return texture;
    }
}
