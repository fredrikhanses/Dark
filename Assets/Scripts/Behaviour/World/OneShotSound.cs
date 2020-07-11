using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSound : MonoBehaviour
{
    AudioSource audio;
    List<Behaviour> filters = new List<Behaviour>();
    void Awake()
    {
        audio = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
    }

    IEnumerator OneShot(float extendTime)
    {
        AudioClip clip = AudioClip.Create(audio.clip.name, Mathf.FloorToInt(audio.clip.samples * Mathf.Clamp(extendTime, 1, audio.clip.samples)), audio.clip.channels, audio.clip.frequency, false);
        float[] samples = new float[audio.clip.samples];
        audio.clip.GetData(samples, 0);
        clip.SetData(samples, 0);

        audio.clip = clip;
        audio.Play();

        yield return new WaitWhile(GetIsPlaying);

        Destroy(gameObject);
    }

    bool GetIsPlaying()
    {
        return audio.isPlaying;
    }

    internal void Play(Sound sound)
    {
        if (sound.audioFilter != null)
        {
            AddFilterComponents(sound.audioFilter);
        }

        audio.clip = sound.clip;
        audio.volume = sound.volume;
        audio.pitch = sound.pitch;
        StartCoroutine(OneShot(sound.timeMultipier));
    }

    internal void PlayLocation(Sound sound, Vector3 location)
    {
        if (sound.audioFilter != null)
        {
            AddFilterComponents(sound.audioFilter);
        }

        audio.clip = sound.clip;
        audio.volume = sound.volume;
        audio.pitch = sound.pitch;
        audio.spatialBlend = 1;
        transform.position = location;
        StartCoroutine(OneShot(sound.timeMultipier));
    }

    private void AddFilterComponents(AudioFilter audioFilter)
    {
        if (audioFilter.chorus)
        {
            AudioChorusFilter filter = (AudioChorusFilter)gameObject.AddComponent(typeof(AudioChorusFilter));

            filter.dryMix = audioFilter.chorusFilter.dryMix;
            filter.wetMix1 = audioFilter.chorusFilter.wetMix1;
            filter.wetMix2 = audioFilter.chorusFilter.wetMix2;
            filter.wetMix3 = audioFilter.chorusFilter.wetMix3;
            filter.delay = audioFilter.chorusFilter.delay;
            filter.rate = audioFilter.chorusFilter.rate;
            filter.depth = audioFilter.chorusFilter.depth;

            filters.Add(filter);
        }

        if (audioFilter.distortion)
        {
            AudioDistortionFilter filter = (AudioDistortionFilter)gameObject.AddComponent(typeof(AudioDistortionFilter));

            filter.distortionLevel = audioFilter.distortionLevel;

            filters.Add(filter);
        }

        if (audioFilter.echo)
        {
            AudioEchoFilter filter = (AudioEchoFilter)gameObject.AddComponent(typeof(AudioEchoFilter));

            filter.delay = audioFilter.echoFilter.delay;
            filter.decayRatio = audioFilter.echoFilter.decayRatio;
            filter.wetMix = audioFilter.echoFilter.wetMix;
            filter.dryMix = audioFilter.echoFilter.dryMix;

            filters.Add(filter);
        }

        if (audioFilter.highPass)
        {
            AudioHighPassFilter filter = (AudioHighPassFilter)gameObject.AddComponent(typeof(AudioHighPassFilter));

            filter.cutoffFrequency = audioFilter.highPassFilter.cutOffFrequency;
            filter.highpassResonanceQ = audioFilter.highPassFilter.highpassResonanceQ;

            filters.Add(filter);
        }

        if (audioFilter.lowPass)
        {
            AudioLowPassFilter filter = (AudioLowPassFilter)gameObject.AddComponent(typeof(AudioLowPassFilter));

            filter.cutoffFrequency = audioFilter.lowPassFilter.cutOffFrequency;
            filter.lowpassResonanceQ = audioFilter.lowPassFilter.lowpassResonanceQ;

            filters.Add(filter);
        }

        if (audioFilter.reverb)
        {
            AudioReverbFilter filter = (AudioReverbFilter)gameObject.AddComponent(typeof(AudioReverbFilter));

            filter.dryLevel = audioFilter.reverbFilter.dryLevel;
            filter.room = audioFilter.reverbFilter.room;
            filter.roomHF = audioFilter.reverbFilter.roomHF;
            filter.roomLF = audioFilter.reverbFilter.roomLF;
            filter.decayTime = audioFilter.reverbFilter.decayTime;
            filter.decayHFRatio = audioFilter.reverbFilter.decayHFRatio;
            filter.reflectionsLevel = audioFilter.reverbFilter.reflectionsLevel;
            filter.reflectionsDelay = audioFilter.reverbFilter.reflectionsDelay;
            filter.reverbLevel = audioFilter.reverbFilter.reverbLevel;
            filter.reverbDelay = audioFilter.reverbFilter.reverbDelay;
            filter.hfReference = audioFilter.reverbFilter.hfReference;
            filter.lfReference = audioFilter.reverbFilter.lfReference;
            filter.diffusion = audioFilter.reverbFilter.diffusion;
            filter.density = audioFilter.reverbFilter.density;

            filters.Add(filter);
        }
    }
}
