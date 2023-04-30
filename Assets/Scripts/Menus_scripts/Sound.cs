using UnityEngine;

public class Sound
{
    //holds all values to change in a clip

    [HideInInspector] public AudioSource source;
    public string clipname;
    public AudioClip AudioClip;
    public bool isLoop;
    public bool playOnAwake;
    public float volume;
    
    public enum AudioTypes {soundEffect, music};
    public AudioTypes audioType;


}
