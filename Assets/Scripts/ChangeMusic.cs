using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ChangeMusic : MonoBehaviour
{
    public AudioClip clip;
    private Toggle toggle;
    // Use this for initialization
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((on) => ChangeMusicWhenOn(on));
    }

    // Update is called once per frame
    void ChangeMusicWhenOn(bool on)
    {
        if (on)
        {
            if (!SoundManager.Instance.isPlayingWorkMusic)
            {
                SoundManager.Instance.source.clip = clip;
                SoundManager.Instance.source.Play();
                SoundManager.Instance.isPlayingWorkMusic = true;

            }
        }
    }
}
