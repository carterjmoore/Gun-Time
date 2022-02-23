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
    Vector3 targetPos;
    Vector3 playerPosRightAfterLostLOS; //Store the player pos a fraction of a second after they leave LOS, to improve chasing
    float distanceTolerance;

    bool haveAdjustedTarget;
    bool alreadyAttacked;

    protected override void Start()
    {
        base.Start();
        playerController = player.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        targetPos = transform.position;
        playerPosRightAfterLostLOS = targetPos;
        haveAdjustedTarget = true;
        alreadyAttacked = false;
    }

    void FixedUpdate()
    {
        //If just lost sight of player, set targetPos to slightly ahead of its current position, to make sure chaser gets around corner
        if (!haveAdjustedTarget)
        {
            targetPos += getTargetDir() * 1f;
            StartCoroutine(setPlayerPosRightAfterLostLOS());
            haveAdjustedTarget = true;
        }

        //Check if player is dead and if the player is visible
        if (!playerController.IsDead() && playerVisible())
        {
            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
            targetPos = player.transform.position;
            haveAdjustedTarget = false;
            shooterShoot();
            rb.velocity = Vector3.zero;
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
            firedProjectile.GetComponent<KillBulletController>().InitProjectile(transform.position, transform.forward * 4f);

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

    Vector3 getTargetDir()
    {
        return (targetPos - transform.position).normalized;
    }

    bool checkIfWithinTolerance()
    {
        //Ignore Y coordinate for distance checking
        Vector2 thisPosNoY = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosNoY = new Vector2(targetPos.x, targetPos.z);
        return distanceTolerance > Vector2.Distance(thisPosNoY, targetPosNoY);
    }

    IEnumerator setPlayerPosRightAfterLostLOS()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        playerPosRightAfterLostLOS = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}