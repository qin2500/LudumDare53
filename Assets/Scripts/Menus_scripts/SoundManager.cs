using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;

    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup SFXMixerGroup;
    
    void Awake()
    {
        Instance = this;
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.AudioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            if (s.audioType == Sound.AudioTypes.soundEffect){
                s.source.outputAudioMixerGroup = SFXMixerGroup;
            }
            else{
                s.source.outputAudioMixerGroup = musicMixerGroup;
            }

            if (s.playOnAwake){
                s.source.Play();
            }
        }
        
    }
    public void Play(string clipname){
        Sound s = Array.Find(sounds, dummySound => dummySound.clipname == clipname);
        if (s == null){
            Debug.LogError("Sound: "+ clipname + "Does not Exist!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string clipname){
        Sound s = Array.Find(sounds, dummySound => dummySound.clipname == clipname);
        if (s == null){
            Debug.LogError("Sound: " + clipname + "Does not Exist!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateMixerVolume()
    {
       musicMixerGroup.audioMixer.SetFloat("Music vol", Mathf.Log10(VolumeSlider.musicVolume)*20);
       SFXMixerGroup.audioMixer.SetFloat("SFX vol", Mathf.Log10(VolumeSlider.SFXVolume)*20);
        
    }

}
