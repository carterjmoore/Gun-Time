using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBulletController : MonoBehaviour
{

    public float speed = 3f;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity != null)
        {
            transform.Translate(velocity * Time.deltaTime * speed);
        }
    }

    public void InitProjectile(Vector3 pos, Vector3 vel)
    {

        transform.position = pos;
        velocity = vel;

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
        }

        Destroy(gameObject);

    }
}
