using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneController : MonoBehaviour
{
    public GameController controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.TriggerWin();
            
            //Detach camera from player, then disable the player
            other.gameObject.GetComponentInChildren<MoveCamera>().gameObject.transform.parent = null;
            other.gameObject.SetActive(false);
        }
    }
}
