using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDetection : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private CanvasGroup canvasGroupToEnable, canvasGroupToDisable;

    private float fadeDuration = 0.25f;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        FadeEffect();
    }

    private void FadeEffect()
    {
        StartCoroutine(FadeEffect(canvasGroupToEnable, canvasGroupToEnable.alpha, 1, fadeDuration));
        StartCoroutine(FadeEffect(canvasGroupToDisable, canvasGroupToDisable.alpha, 0, fadeDuration));
    }

    private IEnumerator FadeEffect(CanvasGroup canvasGroupParameter, float start, float end, float lerp)
    {
        float timeInitializeLerping = Time.time;
        float timeSinceInitialize = (Time.time - timeInitializeLerping);
        float percentageComplete = (timeSinceInitialize / lerp);

        while(true)
        {
            timeSinceInitialize = (Time.time - timeInitializeLerping);
            percentageComplete = (timeSinceInitialize / lerp);

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            canvasGroupParameter.alpha = currentValue;

            if(percentageComplete >= 1) { break; }

            yield return new WaitForEndOfFrame();
        }
    }
}
