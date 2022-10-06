using UnityEngine;

public class BasicRobotController : RobotController
{
    
    // Throwing
    [Header("Throwing Settings")]
    public Transform ShotStart;
    public GameObject BananaProjectile;
    public float ProjectileSpeed;
    public float FireRate = 0.3f;
    private float NextFire;
    private float NextSwing;

    public AudioClip dmg;
    

    protected override void Update()
    {
        /*
        // Health
        slider.value = health / maxHealth;
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
         */
        // Movement
        if (/*health > 0 && */stop == false)
        {
            timer += Time.deltaTime;
            float range = Vector3.Distance(player.transform.position, agent.transform.position);
            Vector3 facing = (player.transform.position - agent.transform.position).normalized;
            float dotProd = Vector3.Dot(facing, agent.transform.forward);

            // LockOn
            if (range < playerRadius)
            {
                agent.SetDestination(player.transform.position);
                //animator.SetBool("isWalking", true);
                //animator.SetBool("isRunning", true);
                timer = wanderTimer;

                if (range >= (playerRadius / 5) && NextFire <= Time.time && dotProd > 0.99)
                {
                    //attack();
                }
                if (range < (playerRadius / 5) && NextSwing <= Time.time && dotProd > 0.99)
                {
                   // meleeattack();
                }
            }

            // Wander
            else if (timer >= wanderTimer)
            {
                //animator.SetBool("isRunning", false);
                //animator.SetBool("isWalking", true);
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }

            // Idle
            else if (agent.remainingDistance == 0)
            {
                //animator.SetBool("isRunning", false);
                //animator.SetBool("isWalking", false);
            }
        }
        // Stopping
        else if (/*health > 0 &&*/ stop == true)
        {
            StopMove();
        }
        // Death
        else
        {
            StopMove();
            //animator.SetBool("isDead", true);
        }
    }


    /*
    void attack()
    {
        animator.SetFloat("shootMultiplier", 1f);
        animator.SetBool("isThrow", true);
        stop = true;
    }

    void meleeattack()
    {
        animator.SetBool("isAttack", true);
        stop = true;
    }
    public void ShootEnd()
    {
        animator.SetBool("isThrow", false);
        stop = false;
    }
    public void meleeEnd()
    {
        animator.SetBool("isAttack", false);
        stop = false;
        NextSwing = Time.time + FireRate;
    }
    public void castProjectile()
    {
        NextFire = Time.time + FireRate;
        GameObject Temp = Instantiate(BananaProjectile, ShotStart.position, ShotStart.rotation);
        Temp.GetComponent<Rigidbody>().velocity = Temp.transform.forward * ProjectileSpeed;
    }
    public void playSound()
    {
        audioSource.PlayOneShot(dmg, 0.7F);
    }
    */
}


