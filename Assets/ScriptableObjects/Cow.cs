using UnityEngine;

[CreateAssetMenu(fileName = "New Cow", menuName = "Objects/Cow")] 
public class Cow : ScriptableObject
{
    public WanderingEntity wanderingEntity;
    public int id;

    public bool lassoed = false;
    public bool scared = false;

    public Sprite sprite;
}
