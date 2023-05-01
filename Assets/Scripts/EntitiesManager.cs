using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public GameObject player;

    public int maxNumCows;
    public Cow[] cowPrefabs;

    public float xMin;
    public float yMin;
    public float xMax;
    public float yMax;

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

    public void removeQuestCows(int[] cows)
    {
        foreach (Cow cow in playerControllerScript.getCows())
        {
            if(cows[cow.id] > 0)
            {
                cows[cow.id]--;
                questCows.Remove(cow);
                cowEntities.Remove(cow);
                Destroy(cow);
            }
        }

        spawnCows();
    }

    private void spawnCows()
    {
        for (int i = cowEntities.Count; i < maxNumCows; i++)
        {
            int cowType = Random.Range(0, cowPrefabs.Length);
            Vector2 location = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            Cow spawn = Instantiate(cowPrefabs[cowType]);
            spawn.transform.position = location;

            cowEntities.Append<Cow>(spawn);
        }
    }
}
