using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScaredBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private CowBehaviour cowBehaviour;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cowBehaviour = GetComponent<CowBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //if detect scary things, flee()

        //set cowBehaviour.scared = true while inside scared radius
    }

    public (Vector2, int) flee(GameObject other)
    {
        SnakeBehaviour temp = other.GetComponent<SnakeBehaviour>();

        int fleeSpeed = (int) math.ceil(temp.terrorLevel * 1.5);
        int fleeDistance = (int) math.ceil(temp.terrorLevel * 2);

        Vector2 direction = ((Vector2) temp.transform.position - (Vector2) this.transform.position).normalized;
        Vector2 endPos = direction * fleeDistance;

        return ( endPos, fleeSpeed );
    }
}
