using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void OnEnable() => OverlapManager.OnAnyCoinCollected += PlayCoinAudio;

    private void OnDisable() => OverlapManager.OnAnyCoinCollected -= PlayCoinAudio;

    private void PlayCoinAudio() => audioSource.Play();
}
