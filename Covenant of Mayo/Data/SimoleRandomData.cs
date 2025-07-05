using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimoleRandomData : ScriptableObject
{
    public int iterations = 100, walkLength = 100;
    public bool startRandomlyEachIteration = true;
}
