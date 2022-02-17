using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script taken from: https://www.youtube.com/watch?v=cTIAhwlvW9M
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
