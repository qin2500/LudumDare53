using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Client", menuName = "Objects/Client")]

public class Client : ScriptableObject
{
    public int[] requiredCows;
    public int rMin;
    public int rMax;
    public Vector2 spawnOrigin;

    private Quest quest;
    private Vector2 location;

    private void Awake()
    {
        int radius = Random.Range(rMin, rMax);
        float angle = Random.value * Mathf.PI * 2;
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        location = spawnOrigin + new Vector2(x, y);
    }

    public void setQuest(Quest quest)
    {
        this.quest = quest;
    }

    public Quest getQuest()
    {
        return quest;
    }

    public void failQuest(QuestManager questManager)
    {
        Debug.Log(name + " fail");
        questManager.concludeQuest(quest, false);
    }

    public void completeQuest(QuestManager questManager)
    {
        Debug.Log(name + " success");
        questManager.concludeQuest(quest, true);
    }

    public Vector2 getLocation()
    {
        return location;
    }

    public bool checkCowInventory(List<Cow> cows)
    {
        foreach (Cow cow in cows)
        {
            requiredCows[cow.id] -= 1;
        }

        return requiredCows.Sum() <= 0;
    }
}
