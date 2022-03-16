using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    public float speed = 1f;
    Vector3 rotationPosition;
    // Start is called before the first frame update
    void Start()
    {
        rotationPosition = new Vector3(0, 65, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationPosition, Vector3.up, speed * Time.deltaTime);
        transform.LookAt(rotationPosition);
    }
}
