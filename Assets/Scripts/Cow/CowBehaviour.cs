using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private WanderController wanderController;
    private ScaredBehaviour scaredBehaviour;
    private FollowBehaviour followBehaviour;
    public bool lassoed = false;
    public bool scared = false;

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
        if (lassoed && !scared)
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

        if(scared)
        {
            followBehaviour.enabled = false;
            wanderController.enabled = false;
            scaredBehaviour.enabled = true;
        }

        if(!lassoed && !scared)
        {
            followBehaviour.enabled = false;
            wanderController.enabled = true;
            scaredBehaviour.enabled = false;
        }
    }
}
