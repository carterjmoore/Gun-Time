using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerController : MonoBehaviour
{

    public GameObject object1;
    public GameObject dialogueManager;

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
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Alright this door is currently locked as the building has detected an intruder.", 2, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: I think thats a little rude of them so I'll be opening that door myself, give me a sec...", 10, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: and voila! Like magic, get your head through!", 18, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "MapTrigger2" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorTrigger(object1, 23));
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 0, true);

            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Alright, you've probably forgotten why we're here. First off we're stealing something.", 2, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Its utility is beyond mine and your understanding, and we are gonna use it to get out of here.", 10, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Here I'll open the next door for you.", 20, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 25, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "MapTrigger3" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            object1.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "PickupTrigger" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[Weapon]:\nLeft Click to fire [Speed]\nRight Click to fire [Slow]", 0, true);

            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: There it is! Thats what we're looking for!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Its highly experimental so I advise you be careful.", 6, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: However its utility... if I can find it in the files...", 14, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Ah there it is! It can progress the flow of time of certain objects by accelerating or de-accelerating them.", 20, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Ooo this is exciting!", 30, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 34, false);
            object1.SetActive(false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "SpawnTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[Note]: Red cubes are dangerous", 8, true);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 16, true);

            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Hold up, they decided to send drones!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Just touching them could destroy you completely, be careful!", 8, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Though they are kind of cute lookin...", 16, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 24, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "MapTrigger4" && IsObject() && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorTrigger(object1, 26));
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: This will be the exit out of the storage zone, you're heading into the big guns now so watch yourself.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: I detect dozens of unfriendly faces beyond this door so stay on your toes.", 12, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Don't worry I'll still be here, I wanna see that device in full action!", 20, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: What a miraculous device we've stumbled upon...", 28, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "ChasmTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[Note]: Collision between two projectiles will nullify both projectiles.", 10, true);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 20, true);

            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: This room looked a lot smaller in the files...", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Watch it! Those projectiles are quite unfriendly, you can stop the bullet when it collides with your own!", 6, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 14, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "ChasmTrigger2" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Those lasers will destroy you when your body comes in contact, best to leave them untouched.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 12, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "GauntletTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: This area is used to test the functionalities of those blast robots you saw earlier.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 12, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "HallwayOfDeathTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: I'm detecting large quantities of hostiles in this area.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: I think the best course of action to high-tail it to the door quickly!", 8, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 16, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "FloorIsLavaTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Huh, this room is called 'The Lava Floor Experiment', yet I'm not seeing any natural sources of heat anywhere.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 12, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "FloorIsLavaTrigger2" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Oh! I get it...", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: ...because the floor is made of lasers!", 4, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 10, false);
            this.GetComponent<BoxCollider>().enabled = false;

        }
        if (this.name == "AmbushTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: This room holds significance to the facility.", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: It mass produces these hostile robots in a sickly manner.", 6, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Which also means this must be a pleasant place to be in...", 14, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 22, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "AmbushTrigger2" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: HEY! ABOVE YOU!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 6, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "TowerTrigger1" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: This area is interesting, I hope you enjoy verticality!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: You know I wonder... how does anyone even navigate through this place?", 10, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 20, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (this.name == "TowerTrigger2" && other.gameObject.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: The extraction point from the facility should be just on the other side of this room!", 0, false);
            dialogueManager.GetComponent<DialogueManager>().PlayerSay("", 10, false);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}