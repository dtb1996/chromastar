using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable() => OverlapManager.OnAnyOrbCollected += PlayOrbAudio;

    private void OnDisable() => OverlapManager.OnAnyOrbCollected -= PlayOrbAudio;

    private void PlayOrbAudio() => _audioSource.Play();
}
