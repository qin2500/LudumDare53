using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoController : MonoBehaviour
{
    public GameObject player;
    public Vector2 velocity;
    public float speed;
    public float maxLength;

    private Rigidbody2D rb;
    private LineRenderer lr;
    private bool awaitingDoom;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        rb.velocity = player.transform.InverseTransformDirection(velocity);

        this.transform.parent = player.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) lr.SetPosition(0, player.transform.position);
        else { lr.SetPosition(0, this.transform.position); }
        lr.SetPosition(1, this.transform.position);

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1 && Mathf.Abs(player.transform.position.y - transform.position.y) < 1 && awaitingDoom) destroy();
        
    }

    private float WAIT = 0;
    private void FixedUpdate()
    {
        //Vector2 curVel = rb.velocity;
        //rb.velocity = player.transform.TransformDirection(curVel);
        if(Vector2.Distance(transform.position, player.transform.position) >= maxLength) rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        if (WAIT + Time.deltaTime >= 1)
        {
            if (player.GetComponent<Rigidbody2D>().velocity == rb.velocity || awaitingDoom)
            {
                Debug.Log((transform.position - player.transform.position).normalized * speed * 10);
                rb.velocity = (transform.position - player.transform.position).normalized * velocity.magnitude * -5;
                awaitingDoom = true;
            }
        }
        else WAIT += Time.deltaTime;

        
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
