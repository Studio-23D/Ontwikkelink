using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFader : MonoBehaviour
{
    public enum FadeMode { In, Out }

    public List<Text> textsToFade;
    public List<Image> imagesToFade;

	[SerializeField] private FadeMode Mode;
    [SerializeField] private bool FadeOnAwake = false;
    [SerializeField] private float FadeDelay = 1f;
    [SerializeField] private float FadeStep = 0.01f;
    [SerializeField] private float FadeTime = 0.01f;

    private bool Fading = false;
    private bool FirstFade = false;

    private void OnEnable()
    {
        if (!FadeOnAwake) return;

        if (Mode == FadeMode.In)
        {
			FadeIn();
        }
        else
        {
			FadeOut();
		}
	}

    private void OnDisable()
    {
		SetAllAlphas(1);
        FirstFade = false;
	}

	public void FadeIn()
	{
		StartCoroutine(Fade(FadeStep));
	}


	public void FadeOut()
	{
        StartCoroutine(Fade(-FadeStep));
	}

	public IEnumerator Fade(float fadeStep)
    {
        if (Fading)
        {
            yield break;
        }

        if (!FirstFade)
        {
            yield return new WaitForSeconds(FadeDelay);
            FirstFade = true;
        }

        Fading = true;

		foreach (Text t in textsToFade)
		{
			SetTextAlpha(t, t.color.a + fadeStep);
		}

		foreach (Image i in imagesToFade)
		{
			SetImageAlpha(i, i.color.a + fadeStep);
		}

        yield return new WaitForSeconds(FadeTime);

        Fading = false;

		foreach (Image i in imagesToFade)
		{
			if (i.color.a > 0)
			{
				StartCoroutine(Fade(fadeStep));
				yield break;
			}
		}

		gameObject.SetActive(false);
    }


	public bool isFading()
	{
		return Fading;
	}

	private void SetAllAlphas(float a)
	{
		foreach (Text t in textsToFade)
		{
			t.color = new Color(t.color.r, t.color.g, t.color.b, a);
		}

		foreach (Image i in imagesToFade)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, a);
		}
	}

	private void SetTextAlpha(Text t, float a)
	{
		t.color = new Color(t.color.r, t.color.g, t.color.b, a);
	}

	private void SetImageAlpha(Image i, float a)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, a);
	}
}
