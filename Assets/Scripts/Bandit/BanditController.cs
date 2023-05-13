using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public bool stunned;
    public WanderController wanderController;
    public GameObject player;
    public GameObject bullet;
    public SteeringAi steer;

    public float moveSpeed;
    public float bulletSpeed;
    public float fireRate;
    public float detectionRange;
    public float fireRange;

    public GameObject Muzzle1;
    public GameObject Muzzle2;  
    
    public string currentState;

    private Rigidbody2D rb;
    private bool faceRight;
    private float fireAC = 0;

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

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if(dist < fireRange)
        {
            fireAC += Time.deltaTime;

            if (fireAC > fireRate)
            {
                fire();
            }
            steer.enabled = false;
        }
        else
        {
            followState();
            fireAC = 0;
        }

        if (rb.velocity.x > 0 && !faceRight) flip();
        else if (rb.velocity.x < 0 && faceRight) flip();
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

    public void fire()
    {
        GameObject bulletInstance = Instantiate(bullet, Muzzle1.transform.position, Quaternion.identity);
        Vector2 dir = (player.transform.position- Muzzle1.transform.position).normalized;

        Debug.Log(dir.x + ", " + transform.localScale.x);
        if (dir.x > 0 && !faceRight)
        {
            flip();
        }
        else if (dir.x < 0 && faceRight)
        {
            flip();
        }

        bulletInstance.GetComponent<BulletController>().setTravelDir(dir);
        bulletInstance.GetComponent<BulletController>().speed = bulletSpeed;

        bulletInstance = Instantiate(bullet, Muzzle2.transform.position, Quaternion.identity);
        dir = (player.transform.position - Muzzle2.transform.position).normalized;
        bulletInstance.GetComponent<BulletController>().setTravelDir(dir);
        bulletInstance.GetComponent<BulletController>().speed = bulletSpeed;

        fireAC = 0;
    }

    private void flip()
    {
        Debug.Log("flip");

        faceRight = !faceRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }
}
