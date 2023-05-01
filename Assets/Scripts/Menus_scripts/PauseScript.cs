using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject PausePanel;
    public static bool GameIsPause = false;
    // Update is called once per frame
    void Start(){
        PausePanel.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPause){
                Continue();
            }
            else{
                Pause();
            }
        }
        
    }


    public void Pause(){
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPause = true;
    }
    public void Continue(){
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        GameIsPause = false;
    }


}
