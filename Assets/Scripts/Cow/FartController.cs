using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartController : MonoBehaviour
{
    [HideInInspector]
    public float speed; 

    public float lifeSpan;
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
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag.Equals("Player"))
        {
            PatchLassoController lasso = other.transform.GetChild(0).GetComponent<PatchLassoController>();

            foreach(GameObject rope in lasso.ropes)
            {
                if(rope)rope.GetComponent<RopeController>().breakRope();
            }
            destroy();
        }
        
    }
}
