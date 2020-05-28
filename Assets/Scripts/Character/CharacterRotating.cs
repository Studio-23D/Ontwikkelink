using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotating : MonoBehaviour
{
    [SerializeField] private bool freeRotation = false;
	[SerializeField] private RectTransform characterImage;
	[SerializeField] private Transform characterContainer;
	[SerializeField] private float rotateSpeed = 100;
	[SerializeField] private InputManager inputManager;
	[SerializeField] private List<Transform> fieldsToBeSelected;

	private bool buttonHeldDown = false;
	private string direction;

    protected Vector3 savedMousePosition;

	private void Awake()
	{
		inputManager.OnTouch += CheckTouch;
	}


	private void CheckTouch(int touchCount, Vector3 startPosition, Transform selectedView)
	{
		if (!fieldsToBeSelected.Contains(selectedView)) return;

		RotateByMouse(startPosition);
	}


    private void RotateByMouse(Vector3 savedMousePosition)
    {
        if (!freeRotation)
		{
			float rotationX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
			characterContainer.transform.Rotate(Vector3.down, rotationX);
        }
        else
        {
			Vector3 delta = (Input.mousePosition - savedMousePosition);
			savedMousePosition = Input.mousePosition;

			Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
			characterContainer.transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.5f, axis) * characterContainer.transform.rotation;
		}
    }

    private void RotateByButton()
    {
        switch (direction)
        {
            case "Left":
                characterContainer.transform.Rotate(Vector3.down, -rotateSpeed * Time.deltaTime);
                break;
            case "Right":
                characterContainer.transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
                break;
        }
    }
}
