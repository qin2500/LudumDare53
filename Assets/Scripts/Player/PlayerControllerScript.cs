using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float trotSpeed;
    public float gallopSpeed;

    private bool gallop;
    private List<Cow> cows;
    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cows = new List<Cow>();
    }

    void Update()
    {
        var inputH = Input.GetAxisRaw("Horizontal");
        var inputV = Input.GetAxisRaw("Vertical");

        velocity.x = inputH;
        velocity.y = inputV;

        velocity.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            gallop = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            gallop= false;
        }
    }

    private void FixedUpdate()
    {
        if (!gallop)
        {
            rb.velocity = velocity * trotSpeed;
        }
        else
        {
            rb.velocity = velocity * gallopSpeed;
        }
    }

    public List<Cow> getCows()
    {
        return cows;
    }
}