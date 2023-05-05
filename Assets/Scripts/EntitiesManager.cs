using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        foreach (Cow cow in cowEntities)
        {
            Vector2 location = cow.transform.position;

            if (((Vector2) player.transform.position - location).magnitude > 15 && !questCows.Contains(cow))
            {
                replacements.Add(cow);
            }
        }

        for (int j=0; j < cows.Length; j++)
        {
            Vector2 location = cowEntities[j].transform.position;
            Destroy(cowEntities[j]);

            cowEntities[j] = Instantiate<Cow>(cowPrefabs[cows[j]]);
            cowEntities[j].transform.position = location;
            questCows.Add(cowEntities[j]);
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
                    Destroy(cow);
                }
            }
        }

        spawnCows();
    }

    private void spawnCows()
    {
        int tierOne = maxNumCows / 2;
        int tierTwo = maxNumCows / 3;
        int tierThree = maxNumCows / 6;

        /*spawnCowsInBox(0, 2, -30, 90, -30, 80, tierOne);
        spawnCowsInBox(2, 4, -60, 130, -60, 100, tierTwo);
        spawnCowsInBox(4, 6, -80, 160, -80, 120, tierThree);*/

        spawnCowsInBox(0, 2, 0, 0, 0, 0, tierOne);
        spawnCowsInBox(2, 4, 90, 90, 0, 0, tierTwo);
        spawnCowsInBox(4, 6, 180, 180, 0, 0, tierThree);
    }

    private void spawnCowsInBox(int indexStart, int indexEnd, float xMin, float xMax, float yMin, float yMax, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int cowType = Random.Range(indexStart, indexEnd);
            Vector2 location = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            Cow spawn = Instantiate(cowPrefabs[cowType]);
            spawn.transform.position = location;

            cowEntities.Append<Cow>(spawn);
        }
    }
}
