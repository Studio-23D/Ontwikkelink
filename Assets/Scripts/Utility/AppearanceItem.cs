using UnityEngine;

public class AppearanceItem : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AnimationClip animation;
	public Sprite Icon => icon;

	public void SetColor(Color color)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetColor("BaseColor", color);
    }
    public void SetPattern(Texture2D texture)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetTexture("Pattern", texture);
    }
    public void SetTextile(Texture2D texture)
    {
        this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetTexture("Textile", texture);
    }

	private void Start()
	{
		SetAnimatorTime();
	}

	private void SetAnimatorTime()
	{
		Animator animator = GetComponentInChildren<Animator>();

		animator.Play(animation.name, 0, GetCurrentAnimatorTime(FindObjectOfType<Character>().GetComponentInChildren<Animator>()));
	}

	private float GetCurrentAnimatorTime(Animator targetAnim, int layer = 0)
	{
		AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);
		float currentTime = animState.normalizedTime % 1;
		return currentTime;
	}
}
