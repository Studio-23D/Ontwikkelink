using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenshotInteractions : MonoBehaviour, IPointerClickHandler
{
    private GameObject gallery;

    private Image inspectingBackgroundImage;
    private Image inspectingPreview;

    private string galleryTag = "Gallery";

    private bool inspectingToggle = false;

    private void Start() { GetBackground(); }

    private void Update()
    {
        if (inspectingToggle && Input.GetMouseButtonDown(0))
        {
            inspectingBackgroundImage.gameObject.SetActive(false);
            inspectingToggle = false;
        }
    }

    private void GetBackground()
    {
        gallery = GameObject.FindGameObjectWithTag(galleryTag);

        inspectingBackgroundImage = gallery.GetComponent<ScreenshotLoading>().background;
        inspectingPreview = gallery.GetComponent<ScreenshotLoading>().preview;
    }

    private void InteractionAction()
    { 
        if (!inspectingToggle)
        {
            inspectingBackgroundImage.gameObject.SetActive(true);
            inspectingPreview.sprite = this.gameObject.GetComponent<Image>().sprite;
            inspectingToggle = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData) { InteractionAction(); }
}
