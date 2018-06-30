using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBuilding : MonoBehaviour {

    Vector3 scale;
    float firstTime = -1;
    public void UpdateScale(float remainingTime)
    {
        if (firstTime < 0) { firstTime = remainingTime; }
        scale = transform.localScale;
        scale.y = 1-remainingTime/firstTime;
        transform.localScale = scale;
    }
}
