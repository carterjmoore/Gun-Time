using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the inner hitbox of player and shooter bullets, and deals with the bullets
//colliding with things that they have no effect on (like walls).
public class InnerBulletHitboxController : MonoBehaviour
{
    public bool forKillBullet = false;
    GameObject projectile;

    private void Start()
    {
        projectile = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!forKillBullet && !obj.CompareTag("Speed") && !obj.CompareTag("Slow")) destroyProjectile();
        if (forKillBullet && !obj.CompareTag("Kill") && !obj.CompareTag("PhysEnemy") && !obj.CompareTag("Laser")) destroyProjectile();
    }

    void destroyProjectile()
    {
        Destroy(gameObject);
        Destroy(projectile);
    }
}
