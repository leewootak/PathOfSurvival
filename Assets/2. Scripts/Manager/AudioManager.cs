using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static  AudioManager instance;
    public static AudioManager Instance {  get { return instance; } set { instance = value; } }

    [SerializeField] private List<AudioClip> audioSources = new List<AudioClip>();
    [SerializeField] private AudioClip BGM;
    [SerializeField] Scrollbar BGMscrollbar;
    [SerializeField] Scrollbar FXscrollbar;

    AudioSource BGMaudio;
    AudioSource FXaudio;

    public AudioMixerGroup MasterAudio;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        gameObject.AddComponent<AudioSource>();
        BGMaudio = GetComponent<AudioSource>();

        gameObject.AddComponent<AudioSource>();
        FXaudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BGMaudio.clip = BGM;
        BGMaudio.loop = true;
        BGMaudio.outputAudioMixerGroup = MasterAudio;
        BGMaudio.Play();
      

        FXaudio.loop = false;
        FXaudio.playOnAwake = false;
    }

    public void BGMVolumeSet()
    {
        BGMaudio.volume = BGMscrollbar.value;
    }
    public void FXVolumeSet() 
    {
        FXaudio.volume = FXscrollbar.value;
    }


    public void FXOn(int index)
    {
        FXaudio.PlayOneShot(audioSources[index]);
    }



}
