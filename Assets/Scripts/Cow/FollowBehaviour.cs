using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;
    }
}
