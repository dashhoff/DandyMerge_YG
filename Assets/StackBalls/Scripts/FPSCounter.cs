using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSCounter : MonoBehaviour
{
    public int FPSCount;
    public Text countText;

    private float nextTimeToViewFPS;
    private void Update()
    {
        FPSCount = (int)(1f / Time.unscaledDeltaTime);
        if (nextTimeToViewFPS < Time.time)
        {
            countText.text = FPSCount.ToString();
            nextTimeToViewFPS = Time.time + 0.2f;
        }
    }
}
