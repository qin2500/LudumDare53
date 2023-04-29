using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public new Camera camera;
    public float scalingFactor;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var position1 = player.transform.position;
        Vector2 position = position1;
        float camDis = camera.transform.position.z - position1.z;
        Vector2 mouseScreenPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camDis));

        float xPos = (mouseScreenPosition.x - position.x) * scalingFactor;
        float yPos = (mouseScreenPosition.y - position.y) * scalingFactor;

        transform.position = new Vector3(position.x + xPos, position.y + yPos, -10);
    }
}