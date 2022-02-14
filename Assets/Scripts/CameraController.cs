using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspiration for camera controller from the following video: https://www.youtube.com/watch?v=E5zNi_SSP_w
//Modified by Carter Moore
public class CameraController : MonoBehaviour
{
    public float sensitivity = 100f;

    [SerializeField] private Transform cam;
    [SerializeField] private Transform orientation;

    private float mouseX;
    private float mouseY;

    private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        //Hide cursor and lock it to center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        handleInput();

        //For vertical rotation, rotate the camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        //For horizontal rotation, rotate the orienation game object
        //We rotate this object instead of the player object itself to reduce jittering
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void handleInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivity * multiplier;
        xRotation -= +mouseY * sensitivity * multiplier;

        //Clamp our vertical cam movement so it can't look farther than straight up or down.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
