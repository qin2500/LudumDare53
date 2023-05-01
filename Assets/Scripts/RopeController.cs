using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public GameObject player;
    public GameObject cow;
    public float maxDist;

    private LineRenderer lr;


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && cow)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, player.transform.position);
            lr.SetPosition(1, cow.transform.position);
        }

        if (Vector2.Distance(player.transform.position, cow.transform.position) > maxDist)
        {
            player.transform.GetChild(0).GetComponent<PatchLassoController>().ropping = false;
            cow.GetComponent<CowBehaviour>().wanderState();
            destroy();
        }
    }

    public void destroy()
    {
        player.GetComponent<PlayerControllerScript>().removeCow(GetComponent<Cow>());
        Destroy(this.gameObject);
    }
}
