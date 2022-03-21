using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaserController : MonoBehaviour
{

    public float speed = 6f;

    Vector3 pos1;
    public Vector3 pos2;

    float distance;
    float fraction;
    bool movingBack;
    public AudioSource LaserNoise;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.position;
        distance = Vector3.Distance(pos1, pos2);
        movingBack = false;
        fraction = 0;
    }

    void FixedUpdate()
    {
        if (!movingBack)
        {
            fraction += speed / distance * Time.deltaTime;
            fraction = Mathf.Clamp01(fraction);
            transform.position = Vector3.Lerp(pos1, pos2, fraction);
            if (fraction == 1) movingBack = true;
        }
        else
        {
            fraction -= speed / distance * Time.deltaTime;
            fraction = Mathf.Clamp01(fraction);
            transform.position = Vector3.Lerp(pos1, pos2, fraction);
            if (fraction == 0) movingBack = false;
        }
    }



     void OnTriggerEnter(Collider other)
     {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
            LaserNoise.Play();

        }

        //Allow to fire through other enemies
        else if (other.gameObject.CompareTag("PhysEnemy"))
        {
            Destroy(other.gameObject);
            LaserNoise.Play();
        }
     }
}
