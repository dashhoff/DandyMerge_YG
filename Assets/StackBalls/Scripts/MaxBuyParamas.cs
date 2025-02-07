using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBuyParamas : MonoBehaviour
{
    public int maxBodySkins;
    public int maxEyeSkins;
    public int maxRotSkins;
    public int maxColorSkins;

    public GameObject[] bodyButtons;
    public GameObject[] eyeButtons;
    public GameObject[] rotButtons;
    public GameObject[] colorButtons;
    private void Start()
    {
        LoadMaxSkins();
    }
    public void LoadMaxSkins()
    {
        if (PlayerPrefs.HasKey("MaxBody"))
        {
            maxBodySkins = PlayerPrefs.GetInt("MaxBody");
        }
        else
        {
            maxBodySkins = 1;
        }

        if (PlayerPrefs.HasKey("MaxEye"))
        {
            maxEyeSkins = PlayerPrefs.GetInt("MaxEye");
        }
        else
        {
            maxEyeSkins = 1;
        }

        if (PlayerPrefs.HasKey("MaxRot"))
        {
            maxRotSkins = PlayerPrefs.GetInt("MaxRot");
        }
        else
        {
            maxRotSkins = 1;
        }

        if (PlayerPrefs.HasKey("MaxColor"))
        {
            maxColorSkins = PlayerPrefs.GetInt("MaxColor");
        }
        else
        {
            maxColorSkins = 1;
        }
    }
}
