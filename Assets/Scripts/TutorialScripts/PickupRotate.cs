using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotate : MonoBehaviour
{

    GameObject pickup;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        pickup = this.gameObject;
        pickup.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        pickup.transform.Rotate(new Vector3(0, 50f, 0) * Time.deltaTime);

        //if (player.GetComponent<PlayerController>().PlayerCanFire())
        //{
        //    pickup.SetActive(false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(false);
        }
    }
}
