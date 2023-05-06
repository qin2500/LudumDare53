using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EntitiesManager : MonoBehaviour
{
    public GameObject player;

    public int maxNumCows;
    public Cow[] cowPrefabs;

    private List<Cow> cowEntities;
    private List<Cow> questCows;
    private PlayerControllerScript playerControllerScript;

    void Start()
    {
        cowEntities = new List<Cow>();
        questCows = new List<Cow>();    

        spawnCows();
        playerControllerScript = player.GetComponent<PlayerControllerScript>();
    }

    void Update()
    {
        
    }

    public void addQuestCows(int[] cows)
    {
        List<Cow> replacements = new List<Cow>();
        int index = 0;

        foreach (Cow cow in cowEntities)
        {
            Vector2 location = cow.transform.position;

            if (((Vector2)player.transform.position - location).magnitude > 15 && !questCows.Contains(cow))
            {
                replacements.Add(cow);
            }
        }

        foreach (Cow cow in replacements)
        {
            Vector2 location = cow.transform.position;

            cowEntities.Remove(cow);
            DestroyImmediate(cow);

            Cow newCow = Instantiate(cowPrefabs[cows[index]]);
            cowEntities.Add(newCow);
            newCow.transform.position = location;
            questCows.Add(newCow);
        }
    }

    public void removeQuestCows(int[] cows, bool questSucess)
    {
        foreach (Cow cow in playerControllerScript.getCows())
        {
            if(cows[cow.id] > 0)
            {
                cows[cow.id]--;
                questCows.Remove(cow);

                if(questSucess)
                {
                    cowEntities.Remove(cow);
                    DestroyImmediate(cow);
                }
            }
        }
        spawnCows();
    }

    private void spawnCows()
    {
        int amount = maxNumCows - cowEntities.Count;

        Debug.Log(amount + ": " + cowEntities.Count);

        int tierOne = amount / 2;
        int tierTwo = amount / 3;
        int tierThree = amount / 6;

        /*spawnCowsInBox(0, 2, -30, 90, -30, 80, tierOne);
        spawnCowsInBox(2, 4, -60, 130, -60, 100, tierTwo);
        spawnCowsInBox(4, 6, -80, 160, -80, 120, tierThree);*/

        spawnCowsInBox(0, 2, 0, 0, 0, 0, tierOne);
        spawnCowsInBox(2, 4, 90, 90, 0, 0, tierTwo);
        spawnCowsInBox(4, 6, 180, 180, 0, 0, tierThree);
    }

    private void spawnCowsInBox(int indexStart, int indexEnd, float xMin, float xMax, float yMin, float yMax, int amount)
    {
        Debug.Log("called with: " + amount);

        if (amount == 0) return;

        for (int i = 0; i < amount; i++)
        {
            int cowType = Random.Range(indexStart, indexEnd);
            Vector2 location = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            Cow spawn = Instantiate(cowPrefabs[cowType]);
            spawn.transform.position = location;

            cowEntities.Add(spawn);
        }
    }
}
