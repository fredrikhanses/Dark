using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public AudioFilter audioFilter;

    [Tooltip("Extends time before sound clip is destroyed")]
    public float timeMultipier;
}
