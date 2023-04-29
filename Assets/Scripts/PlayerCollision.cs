using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject interactionPrompt;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player || collision.gameObject.name == "cowboy")
        {
            interactionPrompt.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player || collision.gameObject.name == "cowboy")
        {
            interactionPrompt.SetActive(false);
        }   
    }
}
