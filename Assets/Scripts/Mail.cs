using UnityEngine;

public class Mail : MonoBehaviour
{
    private float startTime;
    private float deltaTime = 300;

/*    private ArrayList<>
*/
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time - startTime > deltaTime)
        {
            deltaTime = Random.Range(200, 400);
        }
    }
}
