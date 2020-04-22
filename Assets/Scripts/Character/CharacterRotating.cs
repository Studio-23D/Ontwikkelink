using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotating : MonoBehaviour
{
	// TODO create regions
	// TODO set rotation only if mouse hold down on character (raw image on UI)

    private bool buttonHeldDown = false;
    [SerializeField] private bool freeRotation = false;

    private string direction;
    private int rotateMomentum = 100;

    protected Vector3 savedMousePosition;

    [SerializeField]
    private RectTransform rect;

	[SerializeField]
	private Transform characterContainer;
	[SerializeField]
	private string characterTag = "Character";

	private GameObject character;

    private void Update()
    {
		if (!character)
		{
			character = GameObject.FindGameObjectWithTag(characterTag);
			return;
		}

		if (buttonHeldDown)
        {
            RotateByButton();
        }
        else if (!buttonHeldDown)
        {
            if (Input.GetMouseButtonDown(0)) { savedMousePosition = Input.mousePosition; }
            if (Input.GetMouseButton(0) && rect.rect.Contains(new Vector2(Input.mousePosition.x - 1100, Input.mousePosition.y))) { RotateByMouse(); }
        }

    }

    public void HoldRotateButton(string directionParameter)
    {
        buttonHeldDown = true;
        direction = directionParameter;
    }

    public void ReleaseRotateButton() { buttonHeldDown = false; }

    private void RotateByMouse()
    {
        if (freeRotation)
        {
            Vector3 delta = (Input.mousePosition - savedMousePosition);
            savedMousePosition = Input.mousePosition;

            Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            character.transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.5f, axis) * character.transform.rotation;
        }
        else if (!freeRotation)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotateMomentum * Mathf.Deg2Rad;
            character.transform.Rotate(Vector3.down, rotationX);
        }
    }

    private void RotateByButton()
    {
        switch (direction)
        {
            case "Left":
                character.transform.Rotate(Vector3.down, -rotateMomentum * Time.deltaTime);
                break;
            case "Right":
                character.transform.Rotate(Vector3.down, rotateMomentum * Time.deltaTime);
                break;
        }
    }
}
