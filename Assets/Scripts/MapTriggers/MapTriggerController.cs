using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerController : MonoBehaviour
{

    public DialogueManager dmanager;
    public GameObject object1;

    // Start is called before the first frame update
    void Start()
    {
        object1.GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "MapTrigger1")
        {
            object1.GetComponent<BoxCollider>().enabled = false;
        }

    }
}
