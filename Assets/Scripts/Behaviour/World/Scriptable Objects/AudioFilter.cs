using UnityEngine;

[CreateAssetMenu(fileName = "Audio Filter", menuName = "Audio/Audio Filter", order = 0)]
public class AudioFilter : ScriptableObject
{
    public bool chorus;
    public ChorusFilter chorusFilter;
    public bool distortion;
    public float distortionLevel;
    public bool echo;
    public EchoFilter echoFilter;
    public bool highPass;
    public HighPassFilter highPassFilter;
    public bool lowPass;
    public LowPassFilter lowPassFilter;
    public bool reverb;
    public ReverbFilter reverbFilter;
}

[System.Serializable]
public struct ChorusFilter
{
    public float dryMix;
    public float wetMix1;
    public float wetMix2;
    public float wetMix3;
    public float delay;
    public float rate;
    public float depth;
}

[System.Serializable]
public struct EchoFilter
{
    public float delay;
    public float decayRatio;
    public float wetMix;
    public float dryMix;
}

[System.Serializable]
public struct HighPassFilter
{
    public float cutOffFrequency;
    public float highpassResonanceQ;
}

[System.Serializable]
public struct LowPassFilter
{
    public float cutOffFrequency;
    public float lowpassResonanceQ;
}

[System.Serializable]
public struct ReverbFilter
{
    public float dryLevel;
    public float room;
    public float roomHF;
    public float roomLF;
    public float decayTime;
    public float decayHFRatio;
    public float reflectionsLevel;
    public float reflectionsDelay;
    public float reverbLevel;
    public float reverbDelay;
    public float hfReference;
    public float lfReference;
    public float diffusion;
    public float density;
}