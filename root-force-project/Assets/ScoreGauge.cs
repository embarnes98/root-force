using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGauge : MonoBehaviour
{
    public void SetAngle(float period)
    {
        transform.eulerAngles = new Vector3(0, 0, 90 - 6.75f / period);
    }
}
