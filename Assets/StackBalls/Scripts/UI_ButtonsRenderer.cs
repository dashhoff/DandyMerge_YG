using UnityEngine;
using DG.Tweening;
public class UI_ButtonsRenderer : MonoBehaviour
{
    public void OnEnter()
    {
        GetComponent<RectTransform>().DOSizeDelta(new Vector2(85f,85f), 0.5f);
    }

    public void OnExit()
    {
        GetComponent<RectTransform>().DOSizeDelta(new Vector2(80f, 80f), 0.5f);
    }

    public void OnScaler()
    {
        GetComponent<RectTransform>().DOScale(new Vector2(1.1f, 1.1f), 0.5f);
    }

    public void OnUnScaler()
    {
        GetComponent<RectTransform>().DOScale(new Vector2(1f, 1f), 0.5f);
    }
}