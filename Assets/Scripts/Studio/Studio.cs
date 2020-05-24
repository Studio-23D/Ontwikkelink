using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Studio : MonoBehaviour
{
    [SerializeField] private Transform characterContainer;
    [SerializeField] private float rotationSpeed;
    private Character character;

    private void Awake()
    {
        if (characterContainer.childCount > 0)
        {
            character = characterContainer.GetChild(0).GetComponent<Character>();
        }
    }

    public void SwapCharacter(Character character)
    {
        if (this.character != null) Destroy(this.character.gameObject);
        this.character = Instantiate<Character>(character, characterContainer);
    }

    public void SwapItem(AppearanceItem item)
    {
        character.AddItem(item);
    }

    public void RotateCharacterLeft()
    {
        characterContainer.transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime);
    }

    public void RotateCharacterRight()
    {
        characterContainer.transform.Rotate(Vector3.down, -rotationSpeed * Time.deltaTime);
    }
}
