using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 100)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    public bool isOnAwake;
    public bool isLoop;

    [HideInInspector]
    public AudioSource source;
}
