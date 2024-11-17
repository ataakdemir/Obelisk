using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public VectorValue startingPosition;

    void Start()
    {
        transform.position = startingPosition.initialValue;
    }
}
