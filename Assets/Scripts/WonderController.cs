using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WonderController : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;
    public bool wonderOriginFollowsEntity;

    public float wonderAreaRadius;
    private float wonderAreaStartDegree;
    private float wonderAreaEndDegree;

    public float wonderFrequency;
    private float wonderFrequencyAC;
    public float wonderProbobility;
    
    public float wonderRangeMax;
    public float wonderRangeMin;
    public float wonderRange;
    public float wonderSpeed;
    private Vector2 lastPos;
    private Vector2 wonderOrigin;

    private float travelDist;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        wonderRange = -1;
        wonderOrigin = transform.position;
    }

    void Update()
    {
        travelDist = Vector2.Distance(transform.position, lastPos);
        if(travelDist >= wonderRange)
        {
            rb.velocity = Vector2.zero;
            if(wonderFrequencyAC + Time.deltaTime >= wonderFrequency)
            {
                float p = Random.value;
                if(p <= wonderProbobility)
                {
                    wonderRange = Random.Range(wonderRangeMin, wonderRangeMax);
                    velocity = pickDirection();
                    lastPos = this.transform.position;
                    wonderFrequencyAC = 0;
                    if (wonderOriginFollowsEntity) wonderOrigin = this.transform.position;
                }
            }
            else
            {
                wonderFrequencyAC += Time.deltaTime;
            }
        }
        
    }
    
    private void FixedUpdate()
    {
        
        if (travelDist <= wonderRange)rb.velocity = velocity * wonderSpeed;
    }

    private Vector2 pickDirection()
    {
        float timeac = 0;
        while(true)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-Mathf.Sqrt(1 - Mathf.Pow(x, 2)), Mathf.Sqrt(1 - Mathf.Pow(x, 2)));


            //Debug.Log(x + " | " + y);
            Vector2 dir = new Vector2(x,y).normalized;


            //float dist = Vector2.Distance(wonderOrigin, (Vector2)transform.position + dir*wonderRange);

            Vector2 newPos = (Vector2)transform.position + dir * wonderRange;
            newPos -= wonderOrigin;
            Vector2 polarPos = new Vector2(Mathf.Sqrt(Mathf.Pow(newPos.x,2) + Mathf.Pow(newPos.y,2)), Mathf.Atan(newPos.y/newPos.x));

            //if (polarPos.y < 0) polarPos.y += 2 * Mathf.PI;

            Debug.Log(polarPos.y);
            if (polarPos.x < wonderAreaRadius) return dir;

            //if (dist < wonderAreaRadius) return dir;

            timeac += Time.deltaTime;
            if (timeac > 2) return Vector2.up;
        }
        
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, wonderRangeMin);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, wonderRangeMax);
        Gizmos.color = Color.green;
        if (wonderOrigin != Vector2.zero)
        {
            Gizmos.DrawWireSphere(wonderOrigin, wonderAreaRadius);

            Gizmos.color = Color.yellow;

            Vector2 direction = new Vector2(Mathf.Cos(wonderAreaStartDegree), Mathf.Sin(wonderAreaStartDegree));
            direction *= wonderAreaRadius;
            direction += wonderOrigin;
            
            Gizmos.DrawLine(wonderOrigin, direction);

            direction = new Vector2(Mathf.Cos(wonderAreaEndDegree), Mathf.Sin(wonderAreaEndDegree));
            direction *= wonderAreaRadius;
            direction += wonderOrigin;
            
            Gizmos.DrawLine(wonderOrigin, direction);
        }
    }
}
