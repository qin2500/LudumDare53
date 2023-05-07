using UnityEngine.Rendering;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Volume volume;
    public float cycleDuration;

    private float time;

    void Start()
    {
        time = cycleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        volume.weight = Mathf.Abs(cycleDuration - time)/cycleDuration*0.65f;

        if (time >= 2*cycleDuration)
        {
            time = 0;
        }
    }
}
