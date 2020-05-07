using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform hairPosition;
    [SerializeField] private Transform torsoPosition;
    [SerializeField] private Transform legsPosition;
    [SerializeField] private Transform feetPosition;

    public Dictionary<string, Transform> appearenceItemLocations = new Dictionary<string, Transform>();

    private void OnEnable()
    {
        appearenceItemLocations.Add("hair", hairPosition);
        appearenceItemLocations.Add("torso", torsoPosition);
        appearenceItemLocations.Add("legs", legsPosition);
        appearenceItemLocations.Add("feet", feetPosition);
    }
}
