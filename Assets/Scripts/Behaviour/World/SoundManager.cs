using System;
using UnityEngine;
public class SoundManager : MonoBehaviour, IEvent
{
    public string m_EventSoundName;
    [SerializeField] Sound[] m_Sounds;

    /// <summary>
    /// Play sound globally
    /// </summary>
    public GameObject PlaySound(string name)
    {
        Sound playSound = Array.Find(m_Sounds, sound => sound.name == name);
        GameObject oneShotClip = new GameObject(name);
        var comp = (OneShotSound)oneShotClip.AddComponent(typeof(OneShotSound));
        comp.Play(playSound);
        return oneShotClip;
    }

    /// <summary>
    /// Play sound globally
    /// </summary>
    public void PlaySound(string name, float volumeMultiplier, float pitchMultiplier)
    {
        Sound playSound = Array.Find(m_Sounds, sound => sound.name == name);
        float originalVolume = playSound.volume;
        float originalPitch = playSound.pitch;
        playSound.volume *= volumeMultiplier;
        playSound.pitch *= pitchMultiplier;
        GameObject oneShotClip = new GameObject(name);
        var comp = (OneShotSound)oneShotClip.AddComponent(typeof(OneShotSound));
        comp.Play(playSound);
        playSound.volume = originalVolume;
        playSound.pitch = originalPitch;
    }

    /// <summary>
    /// Play sound at location
    /// </summary>
    public void PlaySound(string name, Vector3 location)
    {
        Sound playSound = Array.Find(m_Sounds, sound => sound.name == name);
        GameObject oneShotClip = new GameObject(name);
        var comp = (OneShotSound)oneShotClip.AddComponent(typeof(OneShotSound));
        comp.PlayLocation(playSound, location);
    }

    /// <summary>
    /// Play sound at location
    /// </summary>
    public void PlaySound(string name, Vector3 location, float volumeMultiplier, float pitchMultiplier)
    {
        Sound playSound = Array.Find(m_Sounds, sound => sound.name == name);
        float originalVolume = playSound.volume;
        float originalPitch = playSound.pitch;
        playSound.volume *= volumeMultiplier;
        playSound.pitch *= pitchMultiplier;
        GameObject oneShotClip = new GameObject(name);
        var comp = (OneShotSound)oneShotClip.AddComponent(typeof(OneShotSound));
        comp.PlayLocation(playSound, location);
        playSound.volume = originalVolume;
        playSound.pitch = originalPitch;
    }

    public void PlayMusic(string name, bool loop)
    {
        Sound playMusic = Array.Find(m_Sounds, sound => sound.name == name);
        GameObject music = new GameObject(name);
        AudioSource audioSource = (AudioSource)music.AddComponent(typeof(AudioSource));
        audioSource.clip = playMusic.clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void Event()
    {
        PlaySound(m_EventSoundName);
    }
}
