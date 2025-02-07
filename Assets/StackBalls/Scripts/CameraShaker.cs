using UnityEngine;
public class CameraShaker : MonoBehaviour
{
    public float backMultiplier;
    public static CameraShaker Instance;
    private void Start()
    {
        Instance = this;
    }
    public void Shake()
    {
        transform.localRotation *= Quaternion.Euler(new Vector3(0,0,Random.Range(-2.5f,2.5f)));
    }

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero),Time.deltaTime*backMultiplier);
    }
}
