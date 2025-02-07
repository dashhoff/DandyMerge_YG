using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject nextLevelCandy;
    public GameObject[] popSound;
    public GameObject popParticle;
    public int _candyLevel;
    public bool IsSpawned { get; private set; }
    public bool ISpawned { get; private set; }
    private bool _isPhysicsEnable;

    public GameObject dNumber;
    private Rigidbody2D _rigidbody2D;
    private Collider2D[] _collider2Ds;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2Ds = GetComponents<Collider2D>();
    }

    public void Spawn()
    {
        ISpawned = true;
        DisablePhysics();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            if (coin._candyLevel == _candyLevel && !collision.isTrigger && !coin.IsSpawned)
            {
                IsSpawned = true;
                if (nextLevelCandy != null)
                {
                    GameObject newOb = Instantiate(nextLevelCandy, Vector2.Lerp(transform.position, collision.transform.position, 0.5f), Quaternion.identity);
                    newOb.GetComponent<Coin>().AddVelocity(_rigidbody2D.linearVelocity);
                }
                Destroy(collision.gameObject);
                DestroyMe();
            }
        }

        if (collision.CompareTag("Defeat")&&!ISpawned&& Player_Input.Instance._canPlay)
        {
            Player_Input.Instance.DefeatGame();
        }
    }

    public void DisablePhysics()
    {
        _isPhysicsEnable = false;
        _rigidbody2D.isKinematic = true;
        for (int i = 0; i < _collider2Ds.Length; i++)
            _collider2Ds[i].enabled = false;
    }
    public void EnablePhysics()
    {
        _isPhysicsEnable = true;
        _rigidbody2D.isKinematic = false;
        for (int i = 0; i < _collider2Ds.Length; i++)
            _collider2Ds[i].enabled = true;
    }

    public void DestroyMe()
    {
        SpawnText(transform);
        Player_Input.Instance.AddScore(_candyLevel * 2);
        Player_Input.Instance.SetRecord();
        CameraShaker.Instance.Shake();
        GameObject popSnd = Instantiate(popSound[Random.Range(0, popSound.Length)]);
        Destroy(popSnd, 1f);
        GameObject popPartic = Instantiate(popParticle, transform.position, Quaternion.identity);
        Destroy(popPartic, 1f);
        Destroy(gameObject);
    }
    private void SpawnText(Transform transform)
    {
        GameObject ob1 = Instantiate(dNumber, Vector2.Lerp(transform.position, transform.position, 0.5f), Quaternion.identity);
        ob1.transform.SetParent(Player_Input.Instance.uiLayer);
        ob1.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
        if(YG.YandexGame.EnvironmentData.isDesktop)
            ob1.GetComponent<RectTransform>().localScale = new Vector2(2.75f, 2.75f);
        else
            ob1.GetComponent<RectTransform>().localScale = new Vector2(3.25f, 3.25f);
        ob1.GetComponent<DamageNumbersPro.DamageNumberGUI>().number = nextLevelCandy.GetComponent<Coin>()._candyLevel;
    }
    public void AddVelocity(Vector2 vector2)
    {
        _rigidbody2D.linearVelocity = vector2;
    }
}
