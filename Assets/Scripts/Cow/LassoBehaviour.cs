using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LassoBehaviour : MonoBehaviour
{
    public GameObject player;
<<<<<<< Updated upstream
    public GameObject fart;   
=======
    
    
>>>>>>> Stashed changes

    public float fartFrequency;
    public float lassoedSpeed;
    public float timeToLasso;
    public float lassoRadius;
    public GameObject rope;

    private SteeringAi steer;
    public float timeToLassoAC;

<<<<<<< Updated upstream
=======
    [HideInInspector]
    public FartCannonController fartController;

    private float fartAC;

>>>>>>> Stashed changes
    void Start()
    {
        steer = GetComponent<SteeringAi>();
        steer.enabled= true;
        steer.flee = true;
        steer.speed = lassoedSpeed;
<<<<<<< Updated upstream
=======

        
        fartController = GetComponent<FartCannonController>();
        fartController.player = player;
>>>>>>> Stashed changes
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if(Input.GetMouseButtonDown(0)) rope.GetComponent<RopeController>().breakRope();
        if (dist <= lassoRadius)
        {
            if (timeToLassoAC + Time.deltaTime >= timeToLasso)
            {
                Debug.Log(timeToLassoAC);
                Debug.Log("Lassoed!!!");
                GetComponent<CowBehaviour>().followState();
                timeToLassoAC = 0;
                player.transform.GetChild(0).GetComponent<PatchLassoController>().ropping = false;
                this.gameObject.tag = "Untagged";
                
                player.GetComponent<PlayerControllerScript>().addCow(GetComponent<Cow>());
            }
            else
            {
                timeToLassoAC += Time.deltaTime;
            }
        }
        else timeToLassoAC = 0;
<<<<<<< Updated upstream
=======


        //farting logic

        if (fartAC + Time.deltaTime > fartController.fart.fartFrequency)
        {
            fartController.fire();
            fartAC = 0;
        }
        else fartAC += Time.deltaTime;
>>>>>>> Stashed changes
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, lassoRadius);
                   

    }

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes

}
