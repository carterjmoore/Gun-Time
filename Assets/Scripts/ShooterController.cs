using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : ShootableEntity
{
    public float speed = 8f;
    public float fireRate = 2f;
    public GameObject player;
    public GameObject projectileType;
    public float projectileSpeed = 2f;
    public float projectileSpread = 2f;

    PlayerController playerController;
    Rigidbody rb;

    bool alreadyAttacked;

    protected override void Start()
    {
        base.Start();
        playerController = player.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        alreadyAttacked = false;
    }

    void FixedUpdate()
    {
        //Check if player is dead and if the player is visible
        if (!playerController.IsDead() && playerVisible())
        {
            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
            shooterShoot();
        }
    }

    private void shooterShoot()
    {
        if (!alreadyAttacked)
        {
            GameObject firedProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponent<Collider>());
            
            //Rigidbody rbP = firedProjectile.GetComponent<Rigidbody>();

            /*
            float projectileRSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            float projectileUSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            */

            //note that this is currenly hardcoded for KILL bullet in the component reference
            firedProjectile.GetComponent<KillBulletController>().InitProjectile(transform.position + transform.forward*0.5f, transform.forward * 4f);

            /*
            rbP.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            rbP.AddForce(transform.right * projectileRSpread, ForceMode.Impulse);
            rbP.AddForce(transform.up * projectileUSpread, ForceMode.Impulse);
            */
            alreadyAttacked = true;

            if (timeMultiplier() != 0)
            {
                Invoke(nameof(ResetAttack),  fireRate / (timeMultiplier() * speed));
                Destroy(firedProjectile, 10f);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Check if player is visible to the enemy
    bool playerVisible()
    {
        //Raycast from chaser to player to check if player is visible
        RaycastHit hit;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity))
        {
            //If raycast hits player before anything else, player is visible
            if (hit.transform.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }
}