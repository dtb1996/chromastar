using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    //public AudioClip clip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = clip;
        //StartCoroutine("PlaySound");
    }

    public void SetClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        StartCoroutine("PlaySound");
    }

    private IEnumerator PlaySound()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(gameObject);
    }
}
