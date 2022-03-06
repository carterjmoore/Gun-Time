using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : ShootableEntity
{

    [Header("Shooting")]
    public float reloadTime = 1f;
    public float projectileSpeed = 2f;
    public float projectileSpread = 2f;

    [Header("References")]
    public GameObject player;
    public GameObject projectileType;
    public GameController gameController;

    PlayerController playerController;
    Rigidbody rb;

    bool canAttack;

    protected override void Start()
    {
        base.Start();
        playerController = player.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        canAttack = true;
    }

    void FixedUpdate()
    {
        if (!gameController.shootersEnabled())
        {
            return;
        }

        //Check if player is dead and if the player is visible
        if (!playerController.IsDead() && playerVisible())
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            shooterShoot();
        }
    }

    private void shooterShoot()
    {
        if (canAttack && timeMultiplier() != 0)
        {
            GameObject firedProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponent<Collider>());
            
            //Rigidbody rbP = firedProjectile.GetComponent<Rigidbody>();

            /*
            float projectileRSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            float projectileUSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            */

            //note that this is currenly hardcoded for KILL bullet in the component reference
            firedProjectile.GetComponent<KillBulletController>().InitProjectile(transform.position + transform.forward*0.5f, transform.forward * projectileSpeed);

            /*
            rbP.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            rbP.AddForce(transform.right * projectileRSpread, ForceMode.Impulse);
            rbP.AddForce(transform.up * projectileUSpread, ForceMode.Impulse);
            */
            canAttack = false;

            StartCoroutine(ResetAttack());
            Destroy(firedProjectile, 10f);
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSecondsRealtime(reloadTime / timeMultiplier());
        canAttack = true;
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