using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    public Cow cow;
    private Rigidbody2D rb;
    private WanderController wanderController;
    private ScaredBehaviour scaredBehaviour;
    private FollowBehaviour followBehaviour;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderController = GetComponent(typeof(WanderController)) as WanderController;
        scaredBehaviour = GetComponent(typeof(ScaredBehaviour)) as ScaredBehaviour;
        followBehaviour = GetComponent(typeof (FollowBehaviour)) as FollowBehaviour;

        wanderController.enabled = true;
        scaredBehaviour.enabled = false;
        followBehaviour.enabled = false;
    }

    void Update()
    {
        if (cow.lassoed && !cow.scared)
        {
            wanderController.enabled = false;
            scaredBehaviour.enabled = false;

            if ((rb.velocity.magnitude - Vector2.zero.magnitude) > 0.1)
            {
                rb.velocity = rb.velocity * 0.973f;
            }
            else
            {
                rb.velocity = Vector2.zero;
                followBehaviour.enabled = true;
            }
        }

        if(cow.scared)
        {
            followBehaviour.enabled = false;
            wanderController.enabled = false;
            scaredBehaviour.enabled = true;
        }

        if(!cow.lassoed && !cow.scared)
        {
            followBehaviour.enabled = false;
            wanderController.enabled = true;
            scaredBehaviour.enabled = false;
        }
    }
}
