using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : ShootableEntity
{

    [Header("Shooting")]
    public float reloadTime = 1f;
    public float projectileSpeed = 2f;
    public float projectileSpread = 2f;
    public LayerMask ignoreMask;

    [Header("References")]
    public GameObject player;
    public GameObject projectileType;
    public GameController gameController;

    PlayerController playerController;
    Rigidbody rb;

    bool canAttack;

    public AudioSource AttackSound;


    protected override void Start()
    {
        base.Start();
        playerController = player.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        canAttack = true;
        rb.drag = 2f; //Same as airdrag of player and chasers, so launching shooter will be consistent
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
            AttackSound.Play();
            GameObject firedProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponent<Collider>());

            //Rigidbody rbP = firedProjectile.GetComponent<Rigidbody>();

            /*
            float projectileRSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            float projectileUSpread = Random.Range(-projectileSpread * 0.5f, projectileSpread * 0.5f);
            */

            //note that this is currenly hardcoded for KILL bullet in the component reference
            Vector3 shootDirection = (player.transform.position - transform.position).normalized;
            firedProjectile.GetComponent<KillBulletController>().InitProjectile(transform.position + transform.forward*0.5f, shootDirection * projectileSpeed);

            /*
            rbP.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            rbP.AddForce(transform.right * projectileRSpread, ForceMode.Impulse);
            rbP.AddForce(transform.up * projectileUSpread, ForceMode.Impulse);
            */
            canAttack = false;

            StartCoroutine(ResetAttack());
            Destroy(firedProjectile, 15f);
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
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity, ~ignoreMask))
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