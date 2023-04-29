using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private Vector2 velocity;


    void Start()
    {
       rb= GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        var inputH = Input.GetAxisRaw("Horizontal");
        var inputV = Input.GetAxisRaw("Vertical");
        velocity.x = inputH;
        velocity.y = inputV;

    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }
}
