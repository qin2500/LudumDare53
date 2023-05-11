using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public GameObject sprite;
    public bool faceingRight;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x > 0 && !faceingRight) flip();
        else if (rb.velocity.x < 0 && faceingRight) flip();
    }

    private void flip()
    {
        faceingRight = !faceingRight;
        Vector3 scaler = sprite.transform.localScale;
        scaler.x *= -1;
        sprite.transform.localScale = scaler;
    }
}
