using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public GameObject player;
    public SteeringAi steer;
    private Rigidbody2D rb;

    void Start()
    {
        steer= GetComponent<SteeringAi>();
        steer.enabled= true;
        steer.flee = false;
    }

    void Update()
    {
        //rb.velocity = player.GetComponent<Rigidbody2D>().velocity;
    }
}
