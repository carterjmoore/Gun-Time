using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script taken from: https://www.youtube.com/watch?v=cTIAhwlvW9M
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    Rigidbody rb;
    BoxCollider bc;

    bool dead;
    float deathRotationSpeed;
    float deathZRotation;
    float deathXRotation;
    float deathYRotation;
    int deathXRotationDirection;
    float tolerance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
        rb.useGravity = false;

        deathRotationSpeed = 180f;
        deathZRotation = 0f;
        deathXRotationDirection = 0;
        dead = false;
    }

    void Update()
    {
        //If alive, follow player
        if (!dead)
        {
            transform.position = cameraPosition.position;
        }

        //If dead, rotate camera until it is laying sideways on the floor
        else if (deathZRotation != 90f || deathXRotation != 0f)
        {
            float amountToRotate = deathRotationSpeed * Time.deltaTime;
            deathZRotation = Mathf.Clamp(deathZRotation + amountToRotate, 0f, 90f);
            if (deathXRotationDirection == 1) deathXRotation = Mathf.Clamp(deathXRotation + amountToRotate * deathXRotationDirection, -90f, 0f);
            else if (deathXRotationDirection == -1) deathXRotation = Mathf.Clamp(deathXRotation + amountToRotate * deathXRotationDirection, 0f, 90f);
            transform.rotation = Quaternion.Euler(deathXRotation, deathYRotation, deathZRotation);
        }
    }

    public void TriggerDeath()
    {
        //Enable rigidbody and box collider so that camera falls
        dead = true;
        GameObject player = transform.parent.gameObject;
        transform.parent = null;
        bc.enabled = true;
        rb.useGravity = true;
        player.gameObject.SetActive(false);

        //Save values needed for camera death rotation
        deathXRotationDirection = transform.rotation.eulerAngles.x <= 90 ? -1 : 1;
        deathXRotation = transform.rotation.eulerAngles.x <= 90 ? transform.rotation.eulerAngles.x : transform.rotation.eulerAngles.x - 360;
        deathYRotation = transform.rotation.eulerAngles.y;
    }
}
