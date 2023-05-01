using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Objects/WanderingEntity")]
public class WanderingEntity : ScriptableObject
{
    public int wanderAreaRadius;
    public int wanderFrequency;
    public int wanderProbability;
    public int wanderRangeMax;
    public int wanderRangeMin;
    public int wanderRange;
    public int wanderSpeed;
}
