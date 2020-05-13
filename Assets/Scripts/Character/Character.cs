using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform hairPosition;
    [SerializeField] private Transform torsoPosition;
    [SerializeField] private Transform legsPosition;
    [SerializeField] private Transform feetPosition;

    public Dictionary<AppearanceItemType, Transform> appearenceItemLocations = new Dictionary<AppearanceItemType, Transform>();

    private void OnEnable()
    {
        appearenceItemLocations.Add(AppearanceItemType.Hair, hairPosition);
        appearenceItemLocations.Add(AppearanceItemType.Torso, torsoPosition);
        appearenceItemLocations.Add(AppearanceItemType.Legs, legsPosition);
        appearenceItemLocations.Add(AppearanceItemType.Feet, feetPosition);
    }
}
