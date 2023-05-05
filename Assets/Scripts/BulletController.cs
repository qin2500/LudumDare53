using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public GameObject deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
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
}
