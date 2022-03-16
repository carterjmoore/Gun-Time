using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    public Text minipopText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(liveType("Hello User MANN WATRARWRAWEASDWASDWAS!", false));
        StartCoroutine(liveType("[Movement]: W, A, S, D", true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator liveType(string dialogue, bool isMinipop)
    {
        if (!isMinipop)
        {
            dialogueText.text = "";
            foreach (char letter in dialogue.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            minipopText.text = "";
            foreach (char letter in dialogue.ToCharArray())
            {
                minipopText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }



    }
}
