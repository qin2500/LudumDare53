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
        playerControllerScript = playerInteraction.GetComponent<PlayerControllerScript>();

        client = questManager.getActiveQuests().Last().getClient();
        prompt = transform.GetChild(0).gameObject;

        this.transform.position = client.getLocation();
        prompt.transform.position = client.getLocation();
        playerInteraction.player = questManager.player;
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
