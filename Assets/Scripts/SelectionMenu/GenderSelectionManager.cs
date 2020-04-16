using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region PUBLIC_CLASS 
public class GenderSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionMenu;

    private string gender;

    private void Start() { selectionMenu.SetActive(true); }
    
    public void SetGender(string genderParameter)
    {
        gender = genderParameter;
        ChangeScenery();
    }

    public string GetGender
    {
        get { return gender; }
        set { gender = value; }
    }

    private void ChangeScenery()
    {
        selectionMenu.SetActive(false);
    }
}
#endregion