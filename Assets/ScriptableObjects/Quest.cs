using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Objects/Quest")]
public class Quest : ScriptableObject
{
    public new string name;
    public float timeLimit;
    public float timePassed;
    public bool questActive;

    public Vector2 deliveryPos;

    public int[] requiredCows;

    private GameObject player;
    private PlayerControllerScript playerScript;    

    void Awake()
    {
        timePassed = 0;
        player = GameObject.Find(name = "cowboy");
        playerScript = player.GetComponent<PlayerControllerScript>();
    }

    public void completeQuest()
    {
        GameObject.Find(name = "Mailbox").GetComponent<Mail>().concludeQuest(this);
        Debug.Log(name + " : won");
    }

    public void failQuest()
    {
        GameObject.Find(name = "Mailbox").GetComponent<Mail>().concludeQuest(this);
        Debug.Log(name + " : lost");
    }

    public void startQuest()
    {
        Debug.Log(name + " started");
        questActive = true;
    }

    public bool checkCowInventory()
    {
        List<Cow> temp = playerScript.getCows();

        foreach(Cow cow in temp)
        {
            requiredCows[cow.id] -= 1;
        }

        return requiredCows.Sum() <= 0;
    }
}
