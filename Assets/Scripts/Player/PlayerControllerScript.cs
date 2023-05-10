using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControllerScript : MonoBehaviour
{
    public float trotSpeed;
    public float gallopSpeed;

    public Animator animator;

    private bool gallop;
    private List<Cow> cows;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cows = new List<Cow>();
    }

    private void Update()
    {
        var inputH = Input.GetAxisRaw("Horizontal");
        var inputV = Input.GetAxisRaw("Vertical");

        velocity.x = inputH;
        velocity.y = inputV;

        velocity.Normalize();

        if (inputH > 0 && !faceRight)
        {
            flipSprite();
        }
        if (inputH < 0 && faceRight)
        {
            flipSprite();
        }

        if (!gallop)
        {
            rb.velocity = velocity * trotSpeed;
        }
        else
        {
            rb.velocity = velocity * gallopSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            gallop = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            gallop = false;
        }

        animator.SetFloat("speed", rb.velocity.magnitude);
        animator.SetBool("galloping", gallop);
    }

    public void addCow(Cow cow)
    {
        cows.Add(cow);
    }

    public void removeCow(Cow cow)
    {
        cows.Remove(cow);
    }

    public List<Cow> getCows()
    {
        return cows;
    }

    private void flipSprite()
    {
        faceRight = !faceRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            List<GameObject> ropes = this.transform.GetChild(0).gameObject.GetComponent<PatchLassoController>().ropes;
            foreach(GameObject gm in ropes)
            {
                gm.GetComponent<RopeController>().breakRope();
            }

            Debug.Log("hit");
        }
    }
}