using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void OnEnable() => OverlapManager.OnAnyOrbCollected += PlayOrbAudio;

    private void OnDisable() => OverlapManager.OnAnyOrbCollected -= PlayOrbAudio;

    private void PlayOrbAudio() => audioSource.Play();
}
