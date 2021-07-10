using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meshes and Materials", menuName = "Bruteforce/Data/Meshes and Materials")]
public class MeshAndMaterials : ScriptableObject
{
    public bool start;
    public Race[] races;
    public Job[] jobs;
}
