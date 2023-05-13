using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerDirection : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 dir = (playerPos - (Vector2)transform.position).normalized;
            transform.up = dir;
        }
    }
}
