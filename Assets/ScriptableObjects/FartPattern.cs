using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fart", menuName = "Fart Pattern")]
public class FartPattern : ScriptableObject
{
    public Color fartColor;
    public float fartSize;
    public float fartFrequency;

    public float bulletSpeed;
    public float spread;

    public bool followsPlayer;

    //shotgun
    public bool isShotgun;
    public int bulletCount;

}
