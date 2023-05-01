using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject player;
    public GameObject client;
    public EntitiesManager entitiesManager;

    private float passedTime;
    private float deltaTime = 2;

    public Quest[] possibleQuests;
    private PlayerControllerScript playerControllerScript;
    private List<Quest> visibleQuests;

    void Start()
    {
        passedTime = 0;
        visibleQuests = new List<Quest>();
        playerControllerScript = player.GetComponent<PlayerControllerScript>();
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
            visibleQuests.Add(newQuest);
            acceptQuest(newQuest);
            entitiesManager.addQuestCows(newQuest.requiredCows);
        }

        foreach (Quest quest in visibleQuests)
        {
            if (quest.questActive)
            {
                quest.progressTime();

                if (quest.getTimePassed() > quest.timeLimit)
                {
                    concludeQuest(quest, false);
                }
            }
        }
    }

    public void acceptQuest(Quest quest)
    {
        quest.startQuest();

        ClientController temp = Instantiate(client).GetComponent<ClientController>();
        temp.setQuestManager(this);
    }
    public void concludeQuest(Quest quest, bool sucess)
    {
        Debug.Log(quest.name + " : sucess: " + sucess);
        quest.questActive = false;

        entitiesManager.removeQuestCows(quest.requiredCows, sucess);
    }

    public List<Quest> getActiveQuests()
    {
        return visibleQuests;
    }
}
