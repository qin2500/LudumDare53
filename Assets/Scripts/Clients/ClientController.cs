using System.Linq;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    private Client client;
    private QuestManager questManager;
    private GameObject prompt;
    private PlayerInteraction playerInteraction;
    private PlayerControllerScript playerControllerScript;

    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        playerInteraction.player = questManager.player;
        playerControllerScript = questManager.player.GetComponent<PlayerControllerScript>();

        /*client = questManager.getActiveQuests().Last().getClient();*/
        client = questManager.getActiveQuest().getClient();
        prompt = transform.GetChild(0).gameObject;

        this.transform.position = client.getLocation();
        prompt.transform.position = client.getLocation();
    }

    void Update()
    {
        if (playerInteraction.getColliding() && Input.GetKeyDown(KeyCode.E))
        {
            if (client.checkCowInventory(playerControllerScript.getCows()))
            {
                client.completeQuest(questManager);
            }
        }
    }

    public void setQuestManager(QuestManager questManager)
    {
        this.questManager = questManager;
    }
}
