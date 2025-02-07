using UnityEngine;
using UnityEngine.Events;
using YG;
public class CheckReward : MonoBehaviour
{
    [SerializeField] int AdID;
    public UnityEvent m_event;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void Rewarded(int id)
    {
        if (id == AdID)
        {
            m_event.Invoke();
        }

    }
}
