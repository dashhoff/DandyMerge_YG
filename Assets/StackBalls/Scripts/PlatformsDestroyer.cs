using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsDestroyer : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 15f);
    }
}
