using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector3 initialValue;

    public void Reset()
    {
        initialValue = Vector3.zero;
    }
}

