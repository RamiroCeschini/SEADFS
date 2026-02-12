using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeldManager : MonoBehaviour
{
    [SerializeField] private List<WeldType> weldTypes = new List<WeldType>();
    [SerializeField] private int minNormalTime, maxNormalTime, maxDetectionTime;
    [SerializeField] private ReactionStopwatch stopwatch;
    [SerializeField] private TMP_Text detectionResult, reactionResult, originResult;
    [SerializeField] private CanvasSetting canvas;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private float ambienceValue;
    private bool failure;

    private bool detection = true;
    private bool origin = false;
    private float reactionTime;

    private WeldType currentFailure;
    private string weldFailureName = "Sin falla";
    
    public void StartWeld()
    {
        StartCoroutine(Weld());

    }

    private IEnumerator Weld()
    {
        ambienceValue = ambienceSlider.value;
        ambienceSlider.value = 0;
        AudioManager.Instance.FadeTrack(true, AudioManager.Instance.normalWeldAudioSource, null);
        yield return new WaitForSeconds(Random.Range(minNormalTime, maxNormalTime));
        AudioManager.Instance.FadeTrack(false, AudioManager.Instance.normalWeldAudioSource, null);
        failure = true;
        currentFailure = weldTypes[Random.Range(0, weldTypes.Count)];
        currentFailure.isFailure = true;
        Debug.Log("Caso de falla: " + currentFailure.typeName);
        weldFailureName = currentFailure.typeName;
        AudioManager.Instance.ChangeBool(false);
        AudioManager.Instance.FadeTrack(true, AudioManager.Instance.failureWeldAudioSource, currentFailure.typeClip);

        stopwatch.StartTimer();
        yield return new WaitForSeconds(maxDetectionTime);
        detection = false;
        StopWeld();
        yield return null;
    }

    public void StopWeld()
    {
        StopAllCoroutines();
        ambienceSlider.value = ambienceValue;

        if (!detection)
        {
            Debug.Log("No se detectó la falla");
            reactionTime = maxDetectionTime * 1000;
            origin = false;
            AudioManager.Instance.FadeTrack(false, AudioManager.Instance.failureWeldAudioSource, null);
            canvas.ChangePanel(3);
            DisplayResults();
            return;
        }

        else if (!failure)
        {
            AudioManager.Instance.FadeTrack(false, AudioManager.Instance.normalWeldAudioSource, null);
            detection = false;
            reactionTime = 0;
            origin = false;
            Debug.Log("Falsa detección");
            canvas.ChangePanel(3);
            DisplayResults();
            return;
        }

        Debug.Log("Falla detectada en " + stopwatch.StopTimer() + "ms");
        reactionTime = stopwatch.StopTimer();
        detection = true;
        AudioManager.Instance.FadeTrack(false, AudioManager.Instance.failureWeldAudioSource, null);
        canvas.ChangePanel(2);

    }
    public void CheckFailureType(string failureName)
    {
        if (currentFailure.typeName == failureName)
        {
            Debug.Log("Identificación correcta");
            origin = true;
        }
        else
        {
            Debug.Log("Falsa identificación");
            Debug.Log("Falla: " + currentFailure.typeName);
            Debug.Log("Identificación: " + failureName);
            origin = false;
        }
    }

    public void SaveAttempt()
    {
        UserDataManager.Instance.AddAttempt(detection, reactionTime, origin);
    }

    public void DisplayResults()
    {
        if (!detection)
        {
            detectionResult.text = "No detectada";
        }
        else
        {
            detectionResult.text = "Detectada";
        }
        if (!origin)
        {
            originResult.text = "No reconocido (" + weldFailureName + ")";
        }
        else
        {
            originResult.text = "Reconocido (" + weldFailureName + ")";
        }

        reactionResult.text = reactionTime.ToString() + " ms";
    }
}
