using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource sfxAudioSource, normalWeldAudioSource, failureWeldAudioSource;
    private bool fading = false;

    [SerializeField] private float fadeDuration = 1.5f;
    public float CurrentFade { get; private set; }

    private void Awake()
    {
        Singleton();
    }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else Instance = this;
        DontDestroyOnLoad(this);
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        fading = true;
        audioSource.Play();

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
        fading = false;
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        fading = true;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        fading = false;
    }

    public void FadeTrack(bool trueIn, AudioSource audioSource, AudioClip newClip)
    {
        if (fading)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut(normalWeldAudioSource));
            StartCoroutine(FadeOut(failureWeldAudioSource));
            return;
        }
        if (trueIn == true)
        {
            if (newClip != null)
            {
                audioSource.clip = newClip;
            }
            StartCoroutine(FadeIn(audioSource));
        }

        else
        {
            if (newClip != null)
            {
                audioSource.clip = newClip;
            }
            StartCoroutine(FadeOut(audioSource));
        }
    }

    public void ChangeBool(bool parameter)
    {
        fading = parameter;
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxAudioSource.PlayOneShot(sfx);
    }
}


