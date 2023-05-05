using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public bool stunned;
    public WanderController wanderController;
    public SteeringAi steer;

    public GameObject bullet;

    public float moveSpeed;
    public float bulletSpeed;
    public float fireRate;
    public float detectionRange;
    public float fireRange;

    public GameObject Muzzle1;
    public GameObject Muzzle2;  

    
    public string currentState;


    private Rigidbody2D rb;
    public GameObject player;

    void Start()
    {
        stunned = false;
        wanderState();
        wanderController = GetComponent(typeof(WanderController)) as WanderController;

        steer = GetComponent<SteeringAi>();
        steer.flee = false;

        rb = GetComponent<Rigidbody2D>();

        wanderState();
        steer.enabled = false;
    }

    private float fireAC = 0;
    // Update is called once per frame
    void Update()
    {
        //default state is wander

        //when detect player within range x, attack()


        //states:

        //attack: walk up to a certain distance from the player and begin shooting
        //when player runs a certain distance away from the bandit, reset state to wandering
        //bandit has infinite ammo
        //add a little spread to the shooting so its not aimbot, but i can handle that part just get the pathfinding done
        //if possible have the bandit follow a set of predetermined points on the map
        // - basically a world roaming path
        if(!player)
        {
            steer.enabled = false;
            Collider2D[] vision = Physics2D.OverlapCircleAll(transform.position, detectionRange);
            foreach(Collider2D c in vision)
            {
                if (c.gameObject.tag.Equals("Player"))
                {
                    player = c.gameObject;
                }
            }
        }
        
        if(player)
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if(dist < fireRange)
            {
                if (fireAC + Time.deltaTime >= fireRate)
                {
                    fire();
                }
                else fireRate += Time.deltaTime;
                steer.enabled = false;
            }
            else
            {
                followState();
            }
        }
            
    }
    public void wanderState()
    {
        currentState = "wander";
        wanderController.enabled = true;
        steer.enabled = false;
    }

    public void followState()
    {
        currentState = "follow";
        steer.enabled = true;
        steer.flee = false;
        steer.speed = moveSpeed;
        wanderController.enabled = false;

    }

    public void  fire()
    {
        GameObject bulletInstance = Instantiate(bullet, Muzzle1.transform.position, Quaternion.identity);
        Vector2 dir = Muzzle1.transform.position - transform.position;
        bulletInstance.transform.up = dir;
        bulletInstance.GetComponent<BulletController>().speed = bulletSpeed;

        bulletInstance = Instantiate(bullet, Muzzle2.transform.position, Quaternion.identity);
        dir = Muzzle2.transform.position - transform.position;
        bulletInstance.transform.up = dir;
        bulletInstance.GetComponent<BulletController>().speed = bulletSpeed;

    }
}
