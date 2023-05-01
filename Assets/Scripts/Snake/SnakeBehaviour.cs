using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    public int terrorLevel;
    private Rigidbody2D rb;
    private WanderController wanderController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wanderController = GetComponent<WanderController>();

        wanderController.enabled = true;
    }

    void Update()
    {
        
    }
}
