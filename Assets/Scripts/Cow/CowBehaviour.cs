using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private WanderController wanderController;
    public bool lassoed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        wanderController = GetComponent(typeof(WanderController)) as WanderController;
        wanderController.enabled = true;
    }

    void Update()
    {
        if (lassoed)
        {
            wanderController.enabled = false;

            if((rb.velocity.magnitude - Vector2.zero.magnitude) > 0.1)
            {
                rb.velocity = rb.velocity * 0.973f;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
