using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "FishType", menuName = "Fish/FishType", order = 1)]
public class FishType : ScriptableObject
{
    [Header("General")]
    public Mesh mesh;
    public int minSize;
    public int maxSize;
    public bool carnivorous;
    public bool herbivorous;
    public float detectionRange;

    [Header("Aspect")]

    public bool hasSpecificColor;
    public Color specificColor;
    public float colorVariation;

    [Header("Movement")]
    public float normalSpeed;
    public float hungerSpeed;
}