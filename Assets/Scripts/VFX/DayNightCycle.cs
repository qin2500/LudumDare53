using UnityEngine.Rendering;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Volume volume;
    public ParticleSystem sandstorm;
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

        float contextualTime = Mathf.Abs(cycleDuration - time) / cycleDuration;
        var main = sandstorm.main;

        volume.weight = contextualTime*0.65f;
        main.startSpeed = 16 + contextualTime*(25-16);

        Color temp = main.startColor.color;
        temp.a = contextualTime;
        main.startColor = temp;

        if (time >= 2*cycleDuration)
        {
            time = 0;
        }
    }
}
