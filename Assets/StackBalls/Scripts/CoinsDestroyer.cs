using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsDestroyer : MonoBehaviour
{
    public GameObject _particle;

    public void DestroyCoin()
    {
        GameObject part = Instantiate(_particle, transform.position, transform.rotation);
        Destroy(part, 1.5f);
        Destroy(gameObject);
    }
}
