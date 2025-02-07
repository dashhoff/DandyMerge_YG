using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ADTimerRenderer : MonoBehaviour
{
    public Text txt;
    public GameObject buttonIMG;
    public float delayAD;

    private float timer;
    private bool stopTimer;
    private void Update()
    {
        if (stopTimer == false)
        {
            timer += Time.deltaTime;
        }
        if (timer >= delayAD&&stopTimer==false)
        {
            stopTimer = true;
            timer = 0f;
            StartTimer();
        }
    }


    public void StartTimer()
    {
        buttonIMG.SetActive(true);
        txt.text = "2";
        buttonIMG.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).OnComplete(()=> buttonIMG.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear).OnComplete(Time1));
    }

    public void Time1()
    {
        txt.text = "1";
        buttonIMG.transform.DOScale(1.1f, 0.5f).SetEase(Ease.Linear).OnComplete(() => buttonIMG.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear).OnComplete(CheckAD));
    }

    public void CheckAD()
    {
        buttonIMG.SetActive(false);
        stopTimer = true;
        YG.YandexGame.FullscreenShow();
    }
}
