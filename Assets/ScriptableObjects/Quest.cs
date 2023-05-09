using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Objects/Quest")]
public class Quest : ScriptableObject
{
    public new string name;
    public float timeLimit;
    public bool questActive = false;
    public new string text;

    private float timePassed;
    private Client client;
    private GameObject clientInstance;

    public int[] requiredCows;

    void Awake()
    {
        timePassed = 0;
    }

    public void startQuest()
    {
        Debug.Log(name + " started");

        questActive = true;

        client = CreateInstance<Client>();
        client.requiredCows = this.requiredCows;
        client.rMin = 0;
        client.rMax = 2;
        client.spawnOrigin = new Vector2(3, 3);
        client.setQuest(this);
    }

    public void progressTime()
    {
        this.timePassed += Time.deltaTime;
    }

    public float getTimePassed()
    {
        return this.timePassed;
    }

    public void setClientInstance(GameObject clientInstance)
    {
        this.clientInstance = clientInstance;
    }

    public void destoryClientInstance()
    {
        DestroyImmediate(this.clientInstance);
    }

    public Client getClient()
    {
        return this.client;
    }
}
