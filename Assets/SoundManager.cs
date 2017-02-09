using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{

    public AudioClip[] clips;
    [HideInInspector]
    public AudioSource source;
    private int songIndex = 0;
    public bool isPlayingWorkMusic = false;
    // Use this for initialization
    void Start()
    {
        songIndex = Random.Range(0, clips.Length - 1);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            songIndex = Random.Range(0, clips.Length - 1);
            source.clip = clips[songIndex];
            source.Play();
            isPlayingWorkMusic = false;
        }
    }
}
