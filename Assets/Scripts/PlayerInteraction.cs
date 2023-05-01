using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject interactionPrompt;
    public GameObject Panel;
    private bool playerCollision = false;
    private int[] arr;


    void Start(){
        Panel.SetActive(false);
    }
    void Update(){
        if (playerCollision && Input.GetKeyDown(KeyCode.E)){
            Panel.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerCollision = true;
            interactionPrompt.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            playerCollision = false;
            interactionPrompt.SetActive(false);
        }   
    }

    public bool getColliding()
    {
        return playerCollision;
    }
}
