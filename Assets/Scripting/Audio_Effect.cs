using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Effect : MonoBehaviour
{
    public static Audio_Effect instance;
    public AudioSource source;
    public AudioClip[] Sfx_clip;
    public AudioClip[] Sfx_player_clip;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlayingSFX(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
