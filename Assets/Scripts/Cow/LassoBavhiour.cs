using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LassoBavhiour : MonoBehaviour
{

    public GameObject player;
    public GameObject fart;   
    public float fartFrequency;
    public float lassoedSpeed;
    public float timeToLasso;
    public float lassoRadius;


    private SteeringAi steer;
    public float timeToLassoAC;

    void Start()
    {
        steer = GetComponent<SteeringAi>();
        steer.enabled= true;
        steer.flee = true;
        steer.speed = lassoedSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        
        if(dist <= lassoRadius)
        {
            if(timeToLassoAC + Time.deltaTime >= timeToLasso)
            {
                Debug.Log(timeToLassoAC);
                Debug.Log("Lassoed!!!");
                GetComponent<CowBehaviour>().followState();
                timeToLassoAC = 0;
            }
            else
            {
                timeToLassoAC += Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, lassoRadius);
                   

    }


}
