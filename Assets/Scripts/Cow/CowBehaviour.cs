using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    public string currentState;

    public GameObject bindingCircle;
    public GameObject bindingProgressUI;
    

    private WanderController wanderController;
    private ScaredBehaviour scaredBehaviour;
    private SteeringAi steer;

    public GameObject player;
    public LassoBehaviour cowHit;
    public float followDistance;

    public LayerMask scary;
    public float scaredRadius;
    public float scareUpdateFrequency;
    public float scareSpeed;

    public float followSpeed;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        wanderController = GetComponent(typeof(WanderController)) as WanderController;
        scaredBehaviour = GetComponent(typeof(ScaredBehaviour)) as ScaredBehaviour;
        steer = GetComponent<SteeringAi>();
        cowHit = GetComponent<LassoBehaviour>();
        steer.followDistance = followDistance;
        rb= GetComponent<Rigidbody2D>();
        anim    = GetComponent<Animator>();

        wanderState();

        wanderController.enabled = true;
        scaredBehaviour.enabled = false;

        InvokeRepeating("spooky", 0, scareUpdateFrequency);
    }
    
    private void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            anim.Play("Run");
        }
        else anim.Play("Idle");    
        if (player)
        {
            cowHit.player = player;
            steer.player = player;

        }        
        

        if (currentState.Equals("Hit"))
        {
            float scale = (float) (cowHit.timeToLassoAC / cowHit.timeToLasso * 0.9);
            Vector2 scaleVector = new Vector2(scale, scale);

            bindingProgressUI.transform.localScale = scaleVector;
        }
    }

    public void wanderState()
    {
        currentState = "Wander";
        scaredBehaviour.returnState = currentState;
        wanderController.enabled = true;
        scaredBehaviour.enabled = false;
        steer.enabled = false;
        cowHit.enabled = false;
        bindingCircle.SetActive(false);
    }
    
    public void followState()
    {
        currentState = "Lassoed";
        scaredBehaviour.returnState = currentState;
        wanderController.enabled = false;
        scaredBehaviour.enabled = false;
        steer.enabled = true;
        steer.speed = followSpeed;
        steer.flee = false;
        steer.followDistance = followDistance;
        cowHit.enabled = false;
        bindingCircle.SetActive(false);
    }
    
    public void hitState()
    {
        currentState = "Hit";
        wanderController.enabled = false;
        scaredBehaviour.enabled = false;
        cowHit.enabled = true;
        steer.enabled = true;
        steer.flee = true;
        cowHit.player = player;
        Invoke("setLassoedStatePlayer", 0.05f);
        bindingCircle.SetActive(true);
    }

    public void scaredState(List<GameObject> p)
    {
        currentState = "Scared";
        wanderController.enabled = false;
        scaredBehaviour.enabled = true;
        scaredBehaviour.scary = p;
        
        scaredBehaviour.scareSpeed = scareSpeed;
        steer.enabled = false;
        cowHit.enabled = false;
        bindingCircle.SetActive(false);
    }

    public void setLassoedStatePlayer()
    {
        cowHit.player = player;
    }

    public void spooky()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, scaredRadius, scary);
        if(collider.Length > 0)
        {
            List<GameObject> poo = new List<GameObject>();
            for(int i=0; i<collider.Length; i++)
            {
                poo.Add(collider[i].gameObject);
            }
            scaredState(poo);
        }
        
    }
}
