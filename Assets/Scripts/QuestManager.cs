using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject player;
    public GameObject client;
    public EntitiesManager entitiesManager;
    /*public QuestDisplay questDisplay;*/
    public PlayerInteraction playerInteraction;
    private float passedTime;
    private float deltaTime = 2;

    public Quest[] possibleQuests;
    private PlayerControllerScript playerControllerScript;
    /*    private List<Quest> visibleQuests; */
    private Quest currentQuest;
    
    void Start()
    {
        passedTime = 0;
        /*questDisplay.questPanel.SetActive(false);*/
        /*visibleQuests = new List<Quest>();*/
        playerControllerScript = player.GetComponent<PlayerControllerScript>();

        generateNewQuest();
    }

    void Update()
    {
        /*passedTime += Time.deltaTime;
        if (questDisplay.open == false && playerInteraction.getColliding() && Input.GetKeyDown(KeyCode.E))
        {
            questDisplay.Display();
        }*/
        /*if (passedTime > deltaTime && visibleQuests.Count <= 5)
        {
            deltaTime = Random.Range(200, 400);
            passedTime = 0;
            Quest newQuest = possibleQuests[Random.Range(0, possibleQuests.Length)];
            Debug.Log(newQuest.name + " init");
            visibleQuests.Add(newQuest);
            currentQuest = newQuest;
            
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
        }*/

        if (currentQuest != null)
        {
            currentQuest.progressTime();

            if (currentQuest.getTimePassed() > currentQuest.timeLimit)
            {
                concludeQuest(currentQuest, false);
            }
        }
    }

    public void acceptQuest(Quest quest)
    {
        quest.startQuest();

        GameObject tempClient = Instantiate(client);

        ClientController temp = tempClient.GetComponent<ClientController>();
        temp.setQuestManager(this);
        quest.setClientInstance(tempClient);
    }
    public void concludeQuest(Quest quest, bool sucess)
    {
        /*Debug.Log(quest.name + " : sucess: " + sucess);*/
        /*visibleQuests.Remove(quest);*/
        quest.questActive = false;
        quest.destoryClientInstance();

        entitiesManager.removeQuestCows(quest.requiredCows, sucess);

        generateNewQuest();
    }

    /*public List<Quest> getActiveQuests()
    {   
        return visibleQuests;
    }*/

    private void generateNewQuest()
    {
        Quest newQuest = possibleQuests[Random.Range(0, possibleQuests.Length)];
        /*Debug.Log(newQuest.name + " init");*/
        currentQuest = newQuest;

        entitiesManager.addQuestCows(newQuest.requiredCows);

        acceptQuest(newQuest);
    }

    public Quest getActiveQuest()
    {
        return currentQuest;
    }
}
