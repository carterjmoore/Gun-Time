using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{

    [Header("Game Objects")]
    public GameObject player;
    public GameObject gameController;
    public GameObject dialogueManager;

    [Header("Images")]
    public Image reboot;
    public Image blackScreen;


    public AudioClip staticRepair;
    public AudioClip audioTest;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.enabled = true;
        reboot.enabled = false;
        player.GetComponent<PlayerController>().movementMultiplier = 0.0f;
        StartCoroutine(IntroSequence());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(3);
        reboot.enabled = true;
        yield return new WaitForSeconds(3);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Enabling Audio Drivers...", 0, true);
        yield return new WaitForSeconds(2);
        gameController.GetComponent<AudioSource>().PlayOneShot(staticRepair);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Stablizing Audio Drivers...", 3, true);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Testing Audio Drivers...", 7, true);
        yield return new WaitForSeconds(9);
        gameController.GetComponent<AudioSource>().PlayOneShot(audioTest);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Audio Drivers Initialized", 4, true);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Reconnecting Last Communications...", 6, true);

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: Hey!", 9, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: Hey! You there?", 10, false);
        yield return new WaitForSeconds(10);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Visor Systems Partially Damaged", 0, true);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Executing Visor Reboot...", 3, true);

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: Oh thank god! I couldn't see anything through your visor.", 3, false);
        yield return new WaitForSeconds(7);
        reboot.enabled = false;
        blackScreen.enabled = false;

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: Hey would you look at that! Visor Optics are operational.", 3, false);
        yield return new WaitForSeconds(7);
        gameController.GetComponent<GameController>().BGM1.loop = true;
        gameController.GetComponent<GameController>().BGM1.Play();

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: Can you look around?", 0, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: You landed in a spare storage compartment labelled -E7_3- meaning there should be a door leading to -E7_2- beside you.", 3, false);
        yield return new WaitForSeconds(12);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Rebooting Motion Navigations...", 10, true);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("| Navigations Online", 34, true);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[Movement]:\nW A S D", 37, true);

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: What, did you already forget why you're here?", 0, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: You must have knocked your head pretty hard. Well re-focus, because we're hitting something big.", 5, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[???]: As of now you currently reside in the HexUS Corps Headquarters, curtasy of your pal [-EEVL_I-].", 13, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Of course I am your loyal companion who maintains a studious field in digital technics!", 21, false);
        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Too bad I am limited to the visuals of your visor but I'm more than capable of helping you.", 29, false);
        yield return new WaitForSeconds(37);
        player.GetComponent<PlayerController>().movementMultiplier = 10.0f;

        dialogueManager.GetComponent<DialogueManager>().PlayerSay("[-EEVL_I-]: Your Motion Navs are back online! Head to that door over there.", 0, false);
    }
}
