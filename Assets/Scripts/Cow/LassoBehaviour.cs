using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LassoBehaviour : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    private GunController gun;
    
    public GameObject fartCannon;

    public float lassoedSpeed;
    public float timeToLasso;
    public float lassoRadius;
    [HideInInspector]
    public GameObject rope;

    private SteeringAi steer;
    [HideInInspector]
    public float timeToLassoAC;

    private float fartAC;

    void Start()
    {
        steer = GetComponent<SteeringAi>();
        steer.enabled= true;
        steer.flee = true;

        Invoke("poopoo", 1);
         
        gun = GetComponent<GunController>();
    }
    // Update is called once per frame
    void Update()
    {
        fartCannon.GetComponent<FollowPlayerDirection>().player = player;
        steer.speed = lassoedSpeed;
        //lassoing logic
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if(Input.GetMouseButtonDown(0)) rope.GetComponent<RopeController>().breakRope();
        if (dist <= lassoRadius)
        {
            if (timeToLassoAC + Time.deltaTime >= timeToLasso)
            {
                Debug.Log("Lassoed!!!");
                GetComponent<CowBehaviour>().followState();
                timeToLassoAC = 0;
                player.transform.GetChild(0).GetComponent<PatchLassoController>().roping = false;
                this.gameObject.tag = "Untagged";
                
                player.GetComponent<PlayerControllerScript>().addCow(GetComponent<Cow>());
            }
            else
            {
                timeToLassoAC += Time.deltaTime;
            }
        }
        else timeToLassoAC = 0;


        //farting logic

        if (fartAC + Time.deltaTime > gun.fart.fartFrequency)
        {
            gun.fire();
            fartAC = 0;
        }
        else fartAC += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, lassoRadius);
                   

    }
    private void poopoo()
    {
        fartCannon.GetComponent<FollowPlayerDirection>().player = player; 
    }




}
