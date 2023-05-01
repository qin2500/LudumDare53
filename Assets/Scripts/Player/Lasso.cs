using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    private new Camera camera;
    public GameObject player;

    public GameObject lasso;
    public float maxLength;
    public float maxVel;
    public float minVel;
    public float chargeRate;

    public float charge;

    private bool charging;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float camDis = camera.transform.position.z - transform.position.z;
        Vector2 mouseScreenPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camDis));
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;


        //Lasso Throwing Logic
        if (Input.GetMouseButton(0))
        {
            charging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            charging = false;
            GameObject tempLasso = Instantiate(lasso, transform.position, transform.rotation);
            LassoController lassCont = tempLasso.GetComponent<LassoController>();
            lassCont.velocity = direction * (minVel + ((charge * maxVel - minVel)));
            lassCont.player = player;
            lassCont.maxLength = maxLength;
            charge = 0;
        }
    }

    private void FixedUpdate()
    {
        if (charging && charge <= 1) charge += chargeRate;
    }
}
