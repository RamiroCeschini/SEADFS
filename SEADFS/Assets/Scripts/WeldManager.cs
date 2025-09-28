using System.Collections.Generic;
using UnityEngine;

public class WeldManager : MonoBehaviour
{
    [SerializeField] private List<WeldType> weldTypes = new List<WeldType>();
    [SerializeField] private int minNormalTime, maxNormalTime, maxDetectionTime;
    private float detectionTime;
    
    public void StartWeld()
    {

    }
}
