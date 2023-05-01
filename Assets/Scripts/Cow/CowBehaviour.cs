using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    public string currentState;
    public int state;
    public Cow cow;
    
    private Rigidbody2D rb;
    private WanderController wanderController;
    private ScaredBehaviour scaredBehaviour;
    private FollowBehaviour followBehaviour;
    private SteeringAi steer;
    
    public GameObject player;
    public LassoBehaviour cowHit;
    public float followDistance;

    public float followSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderController = GetComponent(typeof(WanderController)) as WanderController;
        scaredBehaviour = GetComponent(typeof(ScaredBehaviour)) as ScaredBehaviour;
        followBehaviour = GetComponent(typeof (FollowBehaviour)) as FollowBehaviour;
        steer = GetComponent<SteeringAi>();
        cowHit = cowHit.GetComponent<LassoBehaviour>();
        steer.followDistance = followDistance;
        
        wanderState();

        wanderController.enabled = true;
        scaredBehaviour.enabled = false;
        followBehaviour.enabled = false;
    }
    
    private void Update()
    {
        if (player)
        {
            cowHit.player = player;
            steer.player = player;
        }
        //if (hit)
        //{
        //    cowHit.enabled = true;
        //    cowHit.player = player;
        //    wanderController.enabled = false;
        //    scaredBehaviour.enabled = false;
        //    steer.flee = true;
        //    return;
        //}
        //else
        //{
        //    cowHit.enabled = false;
        //    steer.enabled = false;
        //}
        //if(lassoed && !scared)
        //{
        //    steer.enabled = true;
        //    steer.flee = false;
        //    steer.speed = followSpeed;
        //    wanderController.enabled = false;
        //}
        //if (scared)
        //{
        //    followBehaviour.enabled = false;
        //    wanderController.enabled = false;
        //    scaredBehaviour.enabled = true;
        //}
        //if (!lassoed && !scared)
        //{
        //    followBehaviour.enabled = false;
        //    wanderController.enabled = true;
        //    scaredBehaviour.enabled = false;
        //    steer.enabled = false;
        //}
    }

    public void wanderState()
    {
        currentState = "Wander";
        wanderController.enabled = true;
        scaredBehaviour.enabled = false;
        steer.enabled = false;
        cowHit.enabled = false;
    }
    
    public void followState()
    {
        currentState = "Lassoed";
        wanderController.enabled = false;
        scaredBehaviour.enabled = false;
        steer.enabled = true;
        steer.speed = followSpeed;
        steer.flee = false;
        steer.followDistance = followDistance;
        cowHit.enabled = false;
    }
    
    public void hitState()
    {
        currentState = "Hit";
        wanderController.enabled = false;
        scaredBehaviour.enabled = false;
        cowHit.enabled = true;
        cowHit.player = player;
        Invoke("setLassoedStatePlayer", 0.05f);
    }

    public void scaredState()
    {
        currentState = "Scared";
        wanderController.enabled = false;
        scaredBehaviour.enabled = true;
        steer.enabled = false;
        cowHit.enabled = false;
    }

    public void setLassoedStatePlayer()
    {
        cowHit.player = player;
    }
}
