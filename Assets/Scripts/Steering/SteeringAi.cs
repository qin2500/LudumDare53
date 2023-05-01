using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SteeringAi : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool flee;
    public float speed;
    public float followDistance;
    public List<GameObject> fleeObjects;
    public float fleeDistance;
    public SeekBehaviour steeringScript;
    [HideInInspector]
    public GameObject player;
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    bool following = false;
    public GameObject dummy;
    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
        rb = GetComponent<Rigidbody2D>();   
       
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        if (fleeObjects.Count > 0)
        {
            dummy.SetActive(true);
            Vector2 average = averagePosition();
            dummy.transform.position = average;
            dummy.layer = 7;
            aiData.currentTarget = dummy.transform;
        }
        else dummy.SetActive(false);
        if (aiData.currentTarget != null)
        {
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
        if(flee)
        {
            if ((fleeObjects.Count > 0) && Vector2.Distance(transform.position, aiData.currentTarget.transform.position) < fleeDistance) rb.velocity = movementInput.normalized * speed;
            else if (Vector2.Distance(transform.position, player.transform.position) < fleeDistance) rb.velocity = movementInput.normalized * speed;
        }
        else
        {
            //Debug.Log(Vector2.Distance(this.transform.position, aiData.currentTarget.transform.position));
            if (aiData.currentTarget  && Vector2.Distance(this.transform.position, aiData.currentTarget.transform.position) > followDistance)
            {
                rb.velocity = movementInput.normalized * speed;

            }
        }
        

    }
    private IEnumerator ChaseAndAttack()
    {
        if (aiData.currentTarget == null)
        {
            //Stopping Logic
            Debug.Log("Stopping");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            //Chase logic
            movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData, flee);
            yield return new WaitForSeconds(aiUpdateDelay);
            StartCoroutine(ChaseAndAttack());  

        }

    }

    public Vector2 averagePosition()
    {
        Vector2 average = Vector2.zero;
        int count = 0;
        foreach(GameObject g in fleeObjects)
        {
            average += (Vector2)g.transform.position; 
            count++;
        }
        return average * (1 / count);
    }
}
