using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchLassoController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed;
    public float growthSpeed;
    public float radius;
    public GameObject rope;
    public float ropeMaxDist;
    public bool ropping;
    public Transform hand;

    private Rigidbody2D rb;
    private LineRenderer lr;
    private CircleCollider2D cl;
    private float angle;
    private  float curRadius;
    private CowBehaviour moo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        cl = GetComponent<CircleCollider2D>(); 
        lr.positionCount = 0;
    }

    private void Update()
    {
        if(lr.positionCount > 0)
        {
            lr.positionCount = 2;
            if (player != null) lr.SetPosition(0, hand.transform.position);
            else { lr.SetPosition(0, this.transform.position); }
            lr.SetPosition(1, this.transform.position);
        }
        if(curRadius <= 0)
        {
            lr.positionCount = 0;
            cl.enabled = false;
        }
        if (Input.GetMouseButton(0) && !ropping) lr.positionCount = 2;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0) && !ropping)
        {
            cl.enabled = true;
            angle += rotationSpeed;
            if(curRadius < radius)curRadius += growthSpeed * Time.deltaTime;

            float x = player.transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * curRadius;
            float y = player.transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * curRadius;

            transform.position = new Vector2(x, y);

            return;
        }
        if (ropping)
            curRadius = 0;
        
        if (curRadius > 0)
        {
            //Debug.Log("PULLLOUT !!!");
            curRadius -= growthSpeed * Time.deltaTime;
            angle += rotationSpeed;
            float x = player.transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * curRadius;
            float y = player.transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * curRadius;

            transform.position = new Vector2(x, y);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cow")
        {
            ropping = true;
            CowBehaviour cow = collision.gameObject.GetComponent<CowBehaviour>();
            moo = cow;
            cow.hitState();
            cow.player = this.gameObject.transform.parent.gameObject;


            GameObject connection = Instantiate(rope, this.transform.position, Quaternion.identity);
            RopeController ropeController = connection.GetComponent<RopeController>();
            ropeController.player = player;
            ropeController.cow = collision.gameObject;
            ropeController.maxDist = ropeMaxDist;
        }
    }

    //private void LateUpdate()
    //{
    //    moo.player = this.gameObject;
    //}

}
