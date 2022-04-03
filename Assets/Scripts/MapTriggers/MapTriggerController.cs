using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerController : MonoBehaviour
{

    public GameObject object1;
    public GameObject dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private bool IsObject()
    {
        if (object1 != null)
        {
            return true;
        }
        return false;
    }

    IEnumerator DoorTrigger(GameObject door, float delay)
    {

        yield return new WaitForSeconds(delay);
        if (door.GetComponent<DoorController>().CheckDoorOpen())
        {
            door.GetComponent<DoorController>().OpenDoor();
        }
        else if (!door.GetComponent<DoorController>().CheckDoorOpen())
        {
            door.GetComponent<DoorController>().CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "MapTrigger1" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorTrigger(object1, 18));
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Alright this door is currently locked as the building has detected an unknown entity.", 2, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: I think thats a little rude of them so I'll be opening that door myself, give me a sec...", 10, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: and voila! Like magic, get your head through!", 18, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "MapTrigger2" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorTrigger(object1, 32));
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Alright, you've probably forgotten why we're here. First off we're stealing something.", 8, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Its utility is beyond mine and your understanding, and we are gonna use it to get out of here.", 16, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Here I'll open the next door for you.", 32, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 40, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "MapTrigger3" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            object1.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "PickupTrigger" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[Weapon Mechanics]:\n Left Click to fire [Speed]\nRight Click to fire [Slow]", 0, true);

            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: There it is! Thats what we're looking for!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Its highly experimental so I advise you be careful.", 6, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: However its utility... if I can find it in the files...", 14, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Ah there it is! It can progress the flow of time of certain objects by accelerating or de-accelerating them.", 20, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Ooo this is exciting!", 32, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 38, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "DialogueTrigger1" && other.gameObject.CompareTag("Player"))
        {

            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "DialogueTrigger2" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("Help", 30, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}