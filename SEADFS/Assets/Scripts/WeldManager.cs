using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldManager : MonoBehaviour
{
    [SerializeField] private List<WeldType> weldTypes = new List<WeldType>();
    [SerializeField] private int minNormalTime, maxNormalTime, maxDetectionTime;
    [SerializeField] private ReactionStopwatch stopwatch;
    [SerializeField] private GameObject startButton, stopButton;
    private bool failure = false;
    private bool noDetection = false;

    private WeldType currentFailure;
    
    public void StartWeld()
    {
        StartCoroutine(Weld());
        startButton.SetActive(false);
        stopButton.SetActive(true);
    }

    private IEnumerator Weld()
    {
        AudioManager.Instance.FadeTrack(true, AudioManager.Instance.normalWeldAudioSource, null);
        yield return new WaitForSeconds(Random.Range(minNormalTime, maxNormalTime));
        AudioManager.Instance.FadeTrack(false, AudioManager.Instance.normalWeldAudioSource, null);

        currentFailure = weldTypes[Random.Range(0, weldTypes.Count)];
        currentFailure.isFailure = true;
        Debug.Log("Caso de falla: " + currentFailure.typeName);
        AudioManager.Instance.FadeTrack(true, AudioManager.Instance.failureWeldAudioSource, currentFailure.typeClip);
        failure = true;
        stopwatch.StartTimer();
        yield return new WaitForSeconds(maxDetectionTime);
        noDetection = true;
        StopWeld();
        yield return null;
    }

    public void StopWeld()
    {
        stopButton.SetActive(false);

        StopAllCoroutines();
        if (noDetection)
        {
            Debug.Log("No se detectó la falla");
            AudioManager.Instance.FadeTrack(false, AudioManager.Instance.failureWeldAudioSource, null);
            return;
        }

        if (failure)
        {
            Debug.Log("Falla detectada en " + stopwatch.StopTimer() + "ms");
            AudioManager.Instance.FadeTrack(false, AudioManager.Instance.failureWeldAudioSource, null);
        }
        else
        {
            AudioManager.Instance.FadeTrack(false, AudioManager.Instance.normalWeldAudioSource, null);
            Debug.Log("Falsa detección");
        }
    }

    public void CheckFailureType(string failureName)
    {
        if (currentFailure.typeName == failureName)
        {
            Debug.Log("Identificación correcta");

        }
        else
        {
            Debug.Log("Falsa identificación");
            Debug.Log("Falla: " + currentFailure.typeName);
            Debug.Log("Identificación: " + failureName);
        }
    }
}
