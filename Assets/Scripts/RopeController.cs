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
        cow.GetComponent<LassoBehaviour>().rope = this.gameObject;
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

            breakRope();

        }

    }

    public void breakRope()
    {
        player.transform.GetChild(0).GetComponent<PatchLassoController>().roping = false;
        cow.GetComponent<LassoBehaviour>().timeToLassoAC = 0;
        cow.GetComponent<CowBehaviour>().wanderState();
        cow.tag = "Cow";
        player.GetComponent<PlayerControllerScript>().removeCow(GetComponent<Cow>());
        destroy();
    }

    public void destroy()
    {
        player.GetComponent<PlayerControllerScript>().removeCow(GetComponent<Cow>());
        Destroy(this.gameObject);
    }
}
