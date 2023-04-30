using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public GameObject player;

    private float passedTime;
    private float deltaTime = 2;

    public Quest[] possibleQuests;
    private List<Quest> visibleQuests;

    void Start()
    {
        passedTime = 0;
        visibleQuests = new List<Quest>();
    }

    void Update()
    {
        passedTime += Time.deltaTime;

        if (passedTime > deltaTime)
        {
            deltaTime = Random.Range(200, 400);
            passedTime = 0;

            Quest newQuest = possibleQuests[Random.Range(0, possibleQuests.Length)];
            Debug.Log(newQuest.name + " init");
            newQuest.startQuest();
            visibleQuests.Add(newQuest);
        }

        foreach (Quest quest in visibleQuests)
        {
            if (quest.questActive)
            {
                quest.timePassed += Time.deltaTime;

                if (quest.timePassed > quest.timeLimit)
                {
                    quest.failQuest();
                }

                if ((Vector2) player.transform.position == quest.deliveryPos && quest.checkCowInventory())
                {
                    quest.completeQuest();
                }
            }
        }
    }

    public void acceptQuest(Quest quest)
    {
        quest.startQuest();
    }
    public void concludeQuest(Quest quest)
    {
        Debug.Log(quest.name + " : concluded");
        quest.questActive = false;
    }

    public List<Quest> getActiveQuests()
    {
        return visibleQuests;
    }
}
