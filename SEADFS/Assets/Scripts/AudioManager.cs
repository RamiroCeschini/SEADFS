using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource sfxAudioSource, normalWeldAudioSource, failureWeldAudioSource;

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
        audioSource.Play();

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();

    }

    public void FadeTrack(bool trueIn, AudioSource audioSource, AudioClip newClip)
    {
        
        if(trueIn == true)
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


}

