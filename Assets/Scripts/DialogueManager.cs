using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    public Text minipopText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerSay(string toWrite, float delay, bool mini)
    {
        StartCoroutine(LiveType(toWrite, delay, mini));
    }

    public void BlankPlayerSay(bool isMinipop)
    {
        if (!isMinipop)
        {
            dialogueText.text = "";
        }
        else
        {
            minipopText.text = "";
        }
    }

    IEnumerator LiveType(string dialogue, float delay, bool isMinipop)
    {
        yield return new WaitForSeconds(delay);
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
