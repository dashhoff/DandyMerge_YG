using UnityEngine;
using YG;

public class ShowAd : MonoBehaviour
{
    public static ShowAd Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowAdv()
    {
        YandexGame.FullscreenShow();
    }
}
