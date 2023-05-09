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
    public FartPattern fart;
    public GameObject fartBullet;
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
        steer.speed = lassoedSpeed;

        fartCannon.GetComponent<FollowPlayerDirection>().player = player; 
    }
    // Update is called once per frame
    void Update()
    {
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

        if (fartAC + Time.deltaTime > fart.fartFrequency)
        {
            fire();
            fartAC = 0;
        }
        else fartAC += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, lassoRadius);
                   

    }

    public void fire()
    {
        if(fart.isShotgun && fart.spread > 1)
        {
            shotGunFire();
        }
        else
        {
            float thisRot;
            float angle;
            if (fart.followsPlayer) thisRot = fartCannon.transform.rotation.eulerAngles.z;
            else thisRot = Random.Range(360f, 0f);
            if (fart.spread < 1) angle = thisRot;
            else
            {
                float upperBound = thisRot - (fart.spread / 2);
                float lowerBound = thisRot + (fart.spread / 2);
                angle = Random.Range(lowerBound, upperBound);
            }
            makeFart(angle);
        }
    }

    private void shotGunFire()
    {
        if (!fart.isShotgun) return;
        float thisRot;
        float angle;
        if (fart.followsPlayer) thisRot = fartCannon.transform.rotation.eulerAngles.z;
        else thisRot = Random.Range(360f, 0f);
        float increment = fart.spread / fart.bulletCount;
        angle = thisRot - (fart.spread / 2);

        for(int i=0; i<fart.bulletCount; i++)
        {
            makeFart(angle);
            angle += increment;
        }
    }

    private void makeFart(float angle)
    {
        GameObject bullet =  Instantiate(fartBullet, fartCannon.transform.position, Quaternion.Euler(0, 0, angle));       
        FartController controller = bullet.GetComponent<FartController>();
        SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();

        bullet.transform.localScale *= fart.fartSize;
        sprite.color = fart.fartColor;
        controller.speed = fart.bulletSpeed;
        
    }


}
