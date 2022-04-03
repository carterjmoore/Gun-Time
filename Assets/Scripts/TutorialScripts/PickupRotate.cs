using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotate : MonoBehaviour
{

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 50f, 0) * Time.deltaTime);

        //if (player.GetComponent<PlayerController>().PlayerCanFire())
        //{
        //    pickup.SetActive(false);
        //}
    }
}
