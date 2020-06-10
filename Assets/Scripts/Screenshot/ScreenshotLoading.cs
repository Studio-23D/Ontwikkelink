using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotLoading : MonoBehaviour
{
    string[] files = null;

    float galleryPreviewAmount = 4;

    int fileIndex = 0;
    int previewIndex = 0;
    int containerIndex = 0;

    [SerializeField] private GameObject containerPrefab;

    //[SerializeField] private Image[] previews;
    [SerializeField] private List<Image> previews;
    [SerializeField] private List<GameObject> containers;

    [SerializeField] private Text amount;
    [SerializeField] private Text amountPrefabs;

    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        backButton.onClick.AddListener(() => GalleryCirculation(backButton));
        nextButton.onClick.AddListener(() => GalleryCirculation(nextButton));

        /* Conditions at start for the first container */
        backButton.gameObject.SetActive(false);

        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");

        //InstantiateContainers();

        if (files.Length > 0)
        {
            InstantiateContainers();

            for (int i = 0; i < files.Length; i++)
            {
                SetImagesOnPreviews();
            }
        }

        /* Condition if there is only one container */
        if (containers.Count <= 1) { nextButton.gameObject.SetActive(false); }
    }

    private void InstantiateContainers()
    {
        int amountToInstantiate = Mathf.CeilToInt(files.Length / galleryPreviewAmount);
        //int amountToInstantiate = Mathf.CeilToInt(13 / galleryPreviewAmount);

        amount.text = amountToInstantiate.ToString();

        for (int i = 0; i < amountToInstantiate; i++)
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

        amountPrefabs.text = containers.Count.ToString();

        for (int i = 1; i < containers.Count; i++) { containers[i].SetActive(false); }
    }

    private void SetImagesOnPreviews()
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
}
