using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Objects/WanderingEntity")]
public class WanderingEntity : ScriptableObject
{
    public float wanderAreaRadius;
    public float wanderFrequency;
    public float wanderProbability;
    public float wanderRangeMax;
    public float wanderRangeMin;
    public float wanderSpeed;
}
