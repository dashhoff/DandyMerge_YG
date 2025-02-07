using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public SpriteRenderer _sprite;
    public Color color1;
    public Color color2;

    public float changeMultiplier;

    public Color baseDifferentColor;

    private bool _change = true;

    private float _clampTimer;
    void Update()
    {
        if (_change)
        {
            if (baseDifferentColor!=color2)
            {
                _clampTimer += Time.deltaTime * changeMultiplier;
                baseDifferentColor = Color.Lerp(baseDifferentColor, color2, _clampTimer);
            }
            else
            {
                _change = false;
                _clampTimer = 0;
            }
        }
        else
        {
            if (baseDifferentColor!=color1)
            {
                _clampTimer += Time.deltaTime * changeMultiplier;
                baseDifferentColor = Color.Lerp(baseDifferentColor, color1, _clampTimer);
            }
            else
            {
                _change = true;
                _clampTimer = 0;
            }
        }
        _sprite.color = baseDifferentColor;
    }
}
