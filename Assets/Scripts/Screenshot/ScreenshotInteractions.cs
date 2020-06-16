using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
///  This script is responsible for the interaction with previews, and inspecting images
/// </summary> 

public class ScreenshotInteractions : MonoBehaviour, IPointerClickHandler
{
    private GameObject gallery;

    private Image inspectingBackgroundImage;
    private Image inspectingPreview;

    private string galleryTag = "Gallery";

    private bool inspectingToggle = false;

    private void Start() { GetInspectionElements(); }

    private void Update()
    {
        if (inspectingToggle && Input.GetMouseButtonDown(0))
        {
            inspectingBackgroundImage.gameObject.SetActive(false);
            inspectingToggle = false;
        }
    }

    private void GetInspectionElements()
    {
        gallery = GameObject.FindGameObjectWithTag(galleryTag);
        inspectingBackgroundImage = gallery.GetComponent<ScreenshotLoading>().background;
        inspectingPreview = gallery.GetComponent<ScreenshotLoading>().preview;
    }

    private void InteractionAction()
    {
        /* Conditions if the preview has an image */
        bool hasImage = this.gameObject.GetComponent<Image>().sprite != null ? true : false;

        /* Conditions for toggling inspection */
        if (!inspectingToggle && hasImage)
        {
            inspectingBackgroundImage.gameObject.SetActive(true);
            inspectingPreview.sprite = this.gameObject.GetComponent<Image>().sprite;
            inspectingToggle = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData) { InteractionAction(); }
}
