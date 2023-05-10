using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public GameObject deathParticle;

    private Vector2 travelDir;

    void Start()
    {
        Invoke("destroy", lifeSpan);
    }

    void Update()
    {
        transform.Translate(travelDir * speed * Time.deltaTime);
    }
    private void destroy()
    {
        if (deathParticle) Instantiate(deathParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroy();
    }

    public void setTravelDir(Vector2 dir)
    {
        this.travelDir = dir;
    }
}
