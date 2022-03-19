using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspiration for camera controller from the following video: https://www.youtube.com/watch?v=E5zNi_SSP_w
//Modified by Carter Moore
public class CameraController : MonoBehaviour
{
    public float sensitivity = 100f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    bool isDead;

    private void Start()
    {
        isDead = false;
    }

    private void Update()
    {
        //If dead, player can't control camera anymore
        if (isDead) return;
        
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

    public void TriggerDeath() {
        isDead = true;
    }
}
