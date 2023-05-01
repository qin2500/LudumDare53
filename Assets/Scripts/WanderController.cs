using UnityEngine;

public class WanderController : MonoBehaviour
{
    public bool showGizmos;
    public WanderingEntity entity;

    private float wanderAreaStartDegree;
    private float wanderAreaEndDegree;

    private Vector2 velocity;
    private Rigidbody2D rb;
    public bool wanderOriginFollowsEntity;

    private float wanderFrequencyAC;

    private Vector2 lastPos;
    private Vector2 wanderOrigin;

    private float travelDist;
    private float wanderRange;
    private void OnEnable()
    {
        wanderOrigin = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        wanderRange = -1;
        wanderOrigin = transform.position;
    }

    void Update()
    {
        travelDist = Vector2.Distance(transform.position, lastPos);

        if (travelDist >= wanderRange || rb.velocity.magnitude < 0.01f)
        {
            rb.velocity = Vector2.zero;

            if (wanderFrequencyAC + Time.deltaTime >= entity.wanderFrequency)
            {
                float p = Random.value;

                if (p <= entity.wanderProbability)
                {
                    wanderRange = Random.Range(entity.wanderRangeMin, entity.wanderRangeMax);
                    velocity = pickDirection();
                    lastPos = this.transform.position;
                    wanderFrequencyAC = 0;

                    if (wanderOriginFollowsEntity)
                    {
                        wanderOrigin = this.transform.position;
                    }
                }
            }
            else
            {
                wanderFrequencyAC += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (travelDist <= wanderRange) rb.velocity = velocity * entity.wanderSpeed;
    }

    private Vector2 pickDirection()
    {
        float timeac = 0;

        while (true)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-Mathf.Sqrt(1 - Mathf.Pow(x, 2)), Mathf.Sqrt(1 - Mathf.Pow(x, 2)));

            //Debug.Log(x + " | " + y);
            Vector2 dir = new Vector2(x, y).normalized;

            //float dist = Vector2.Distance(wanderOrigin, (Vector2)transform.position + dir*wanderRange);

            Vector2 newPos = (Vector2)transform.position + dir * wanderRange;
            newPos -= wanderOrigin;
            Vector2 polarPos = new Vector2(Mathf.Sqrt(Mathf.Pow(newPos.x, 2) + Mathf.Pow(newPos.y, 2)), Mathf.Atan(newPos.y / newPos.x));

            //if (polarPos.y < 0) polarPos.y += 2 * Mathf.PI;

            if (polarPos.x < entity.wanderAreaRadius) return dir;

            //if (dist < wanderAreaRadius) return dir;

            timeac += Time.deltaTime;
            if (timeac > 2) return ((Vector2)transform.position - wanderOrigin).normalized;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, entity.wanderRangeMin);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, entity.wanderRangeMax);
            Gizmos.color = Color.green;

            if (wanderOrigin != Vector2.zero)
            {
                Gizmos.DrawWireSphere(wanderOrigin, entity.wanderAreaRadius);

                Gizmos.color = Color.yellow;

                Vector2 direction = new Vector2(Mathf.Cos(wanderAreaStartDegree), Mathf.Sin(wanderAreaStartDegree));
                direction *= entity.wanderAreaRadius;
                direction += wanderOrigin;
            
                Gizmos.DrawLine(wanderOrigin, direction);

                direction = new Vector2(Mathf.Cos(wanderAreaEndDegree), Mathf.Sin(wanderAreaEndDegree));
                direction *= entity.wanderAreaRadius;
                direction += wanderOrigin;
            
                Gizmos.DrawLine(wanderOrigin, direction);
            }
            else Gizmos.DrawWireSphere(transform.position, entity.wanderAreaRadius);
        }
        
    }
}