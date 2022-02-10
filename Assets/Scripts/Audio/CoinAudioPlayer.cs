using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable() => OverlapManager.OnAnyCoinCollected += PlayCoinAudio;

    private void OnDisable() => OverlapManager.OnAnyCoinCollected -= PlayCoinAudio;

    private void PlayCoinAudio() => _audioSource.Play();
}
