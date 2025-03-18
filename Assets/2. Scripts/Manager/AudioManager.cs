using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static  AudioManager instance;
    public static AudioManager Instance {  get { return instance; } set { instance = value; } }

    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] private AudioSource BGM;
    [SerializeField] Scrollbar BGMscrollbar;
    [SerializeField] Scrollbar FXscrollbar;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        foreach(AudioSource source in audioSources) 
        {
            source.enabled= false;
            source.loop=false;
            source.playOnAwake = false;
        }
    }


    private void Update()
    {
        BGM.Play();
    }


    public void BGMVolumeSet()
    {
        BGM.volume = BGMscrollbar.value;
    }
    public void FXVolumeSet() 
    {
        FXscrollbar.value = FXscrollbar.value;
    }


    public void FXOn(int index)
    {
        audioSources[index].enabled = true;
        audioSources[index].Play();
    }



}
