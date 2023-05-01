using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScaredBehaviour : MonoBehaviour
{
    private CowBehaviour cowBehaviour;

    public List<GameObject> scary;
    public String returnState;
    public float scareSpeed;
    private SteeringAi steer;

    void Start()
    {
        steer = GetComponent<SteeringAi>();
        cowBehaviour = GetComponent<CowBehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {

        runAway();
        //if detect scary things, flee()

        //set cowBehaviour.scared = true while inside scared radius
    }

    //public (Vector2, int) flee(GameObject other)
    //{
    //    SnakeBehaviour temp = other.GetComponent<SnakeBehaviour>();

    //    int fleeSpeed = (int) math.ceil(temp.terrorLevel * 1.5);
    //    int fleeDistance = (int) math.ceil(temp.terrorLevel * 2);

    //    Vector2 direction = ((Vector2) temp.transform.position - (Vector2) this.transform.position).normalized;
    //    Vector2 endPos = direction * fleeDistance;

    //    return ( endPos, fleeSpeed );
    //}

    public void runAway()
    {
        steer.enabled = true;
        steer.flee = true;
        steer.speed = scareSpeed;
        steer.fleeObjects = scary;

        Invoke("returnS", 2);
    }

    public void returnS()
    {
        Debug.Log("Finishing Runaway: " + returnState);
        if (string.Equals(returnState, "Lassoed")) cowBehaviour.followState();
        else if (string.Equals(returnState, "Wander")) cowBehaviour.wanderState();
    }
}
