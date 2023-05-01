using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public QuestManager questManager;
    private List<Quest> quests;
    private List<GameObject> questPanelList;
    public GameObject questPanelPrefab;
    public Transform contentPanel;
    public GameObject questPanel;
    public bool open = false;



    public void Display()

    {
        open = true;
        questPanel.SetActive(open);
        
        quests = questManager.getActiveQuests();
        questPanelList = new List<GameObject>();
        foreach (Quest quest in quests)
        {
            GameObject questPanel = Instantiate(questPanelPrefab) as GameObject;
            questPanelList.Add(questPanel);
            questPanel.transform.SetParent(contentPanel);


            // Set panel's title and description to quest's properties 
            questPanel.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = quest.name;
            questPanel.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = quest.text;

            // Add onClick listener to accept button
            Button acceptButton = questPanel.transform.Find("AcceptButton").GetComponent<Button>();
            acceptButton.onClick.AddListener(delegate { AcceptQuest(quest, questPanel); });
            
        }
        
    }

    public void close_panel() {
        open = false;
        questPanel.SetActive(open);
        
        foreach (GameObject q in questPanelList) {

            Destroy(q);
        }
        questPanelList.Clear();
    }
    public void AcceptQuest(Quest quest, GameObject questPanel)
    {
        questManager.acceptQuest(quest);
        quests.Remove(quest);
        Destroy(questPanel);
    }
}