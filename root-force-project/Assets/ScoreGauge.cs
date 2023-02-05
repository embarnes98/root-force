using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGauge : MonoBehaviour
{
    public void SetAngle(float currentPeriod, float minPeriod, float maxPeriod)
    {
        transform.eulerAngles = new Vector3(
            0, 0, 90 - 180 * Mathf.Lerp(maxPeriod, minPeriod, currentPeriod)
        );
    }
}
