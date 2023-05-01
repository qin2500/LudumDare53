using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public GameObject player;
    public SteeringAi steer;

    void Start()
    {
        steer = GetComponent<SteeringAi>();
        steer.enabled = true;
        steer.flee = false;
    }

    void Update()
    {
        
    }
}
