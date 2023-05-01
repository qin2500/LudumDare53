using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class QuestDisplay : MonoBehaviour
{

     public GameObject questPanel;
     public GameObject questText;
     public QuestManager questManager;
     private Quest[] quests;

    void Start(){
        questPanel.SetActive(false);
    }

    public void Display(){
        int i = 0;
        quests = questManager.possibleQuests;
        questPanel.SetActive(true);
        foreach (Quest q in quests) {
            if (questText.GetComponent<UnityEngine.UI.Text>().text == null)
            {
                questText.GetComponent<UnityEngine.UI.Text>().text = quests[i].text + "\n";
                i++;
            }

        }
    }


 

}
