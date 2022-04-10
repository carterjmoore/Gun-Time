using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door;

    public Material inactiveMat;
    public Material activeMat;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = inactiveMat;
        GetComponent<Renderer>().material = activeMat;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!door.GetComponent<DoorController>().CheckDoorOpen())
        {
            gameObject.GetComponent<Renderer>().material = activeMat;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = inactiveMat;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.GetComponent<DoorController>().OpenDoor();
        }
    }
}