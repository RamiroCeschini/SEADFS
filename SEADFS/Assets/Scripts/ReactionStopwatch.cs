using UnityEngine;
using System.Diagnostics;

public class ReactionStopwatch : MonoBehaviour
{
    private Stopwatch stopwatch;

    void Awake()
    {
        stopwatch = new Stopwatch();
    }

    public void StartTimer()
    {
        stopwatch.Reset();
        stopwatch.Start();
    }

    public float StopTimer()
    {
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}
