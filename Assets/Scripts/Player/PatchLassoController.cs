using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchLassoController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed;
    public float growthSpeed;
    public float radius;

    private Rigidbody2D rb;
    private LineRenderer lr;
    private float angle;
    public  float curRadius;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    private void Update()
    {
        if(lr.positionCount > 0)
        {
            lr.positionCount = 2;
            if (player != null) lr.SetPosition(0, player.transform.position);
            else { lr.SetPosition(0, this.transform.position); }
            lr.SetPosition(1, this.transform.position);
        }
        if(curRadius <= 0)
        {
            lr.positionCount = 0;
        }
        if (Input.GetMouseButton(0)) lr.positionCount = 2;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            angle += rotationSpeed;
            if(curRadius < radius)curRadius += growthSpeed * Time.deltaTime;

            float x = player.transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * curRadius;
            float y = player.transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * curRadius;

            transform.position = new Vector2(x, y);

            return;
        }

        if (curRadius > 0)
        {
            Debug.Log("PULLLOUT !!!");
            curRadius -= growthSpeed * Time.deltaTime;
            angle += rotationSpeed;
            float x = player.transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * curRadius;
            float y = player.transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * curRadius;

            transform.position = new Vector2(x, y);
        }

        
    }
}
