using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public bool stunned;
    public WanderController wanderController;
    public AttackBehaviour attackBehaviour;

    public GameObject player;
    public float attackSpeed;
    public float fireRate;
    public float fireAccuracy;
    public float detectionRange;

    private SteeringAi steer;
    private string currentState;

    private Rigidbody2D rb;

    void Start()
    {
        stunned = false;
        wanderState();
        wanderController = GetComponent(typeof(WanderController)) as WanderController;
        attackBehaviour = GetComponent(typeof(AttackBehaviour)) as AttackBehaviour;

        steer = GetComponent<SteeringAi>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //default state is wander

        //when detect player within range x, attack()


        //states:

        //attack: walk up to a certain distance from the player and begin shooting
        //when player runs a certain distance away from the bandit, reset state to wandering
        //bandit has infinite ammo
        //add a little spread to the shooting so its not aimbot, but i can handle that part just get the pathfinding done
        //if possible have the bandit follow a set of predetermined points on the map
            // - basically a world roaming path
    }
    public void wanderState()
    {
        currentState = "wander";
        wanderController.enabled = true;
        attackBehaviour.enabled = false;
    }

    public void attackState()
    {
        currentState = "attack";
        wanderController.enabled = false;
        attackBehaviour.enabled = true;
    }
}
