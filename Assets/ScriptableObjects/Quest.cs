using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Objects/Quest")]
public class Quest : ScriptableObject
{
    public new string name;
    public ArrayList requiredCows;
    public float timeLimit;
    public float timePassed;

    public Vector2 deliveryPos;

    void Start()
    {
        timePassed = 0;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
    }
}
