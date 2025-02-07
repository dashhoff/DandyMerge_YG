using UnityEngine;
using TMPro;
using YG;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Input : MonoBehaviour
{
    [Header("Game Attributes")]
    public int score;
    public int _scoreOnCloud;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _defeatText;
    [SerializeField] private TextMeshProUGUI _recordScore;

    [SerializeField] private AudioSource _defeatSound;
    [SerializeField] private CanvasGroup _mainCanvas;
    [SerializeField] private CanvasGroup _defeatCanvas;

    [Header("Game Options")]
    [SerializeField] private Coin[] _coins;
    [SerializeField] private GameObject _spawnSound;
    [SerializeField] private float clickCooldown;
    [SerializeField] private float _bombExplosionRadius;
    [SerializeField] private ParticleSystem _bombExplosionPS;

    [Header("Points")]
    [SerializeField] private Transform _leftCorner;
    [SerializeField] private Transform _rightCorner;
    [SerializeField] private Transform _upperSpawnPoint;
    public Transform _defeatPos;

    [Header("Hammers")]
    public int hammers;
    [SerializeField] private TextMeshProUGUI _hText;
    private bool _hammerMode = false;
    [SerializeField] private Image _imgButton;

    [Header("Others")]
    public GameObject _clickCanves;
    public GameObject _unableCircle;
    public static Player_Input Instance;
    private float _nextTimeToClick;
    public bool _canPlay = true;
    public GameObject _Button1;
    public GameObject _Button2;
    public Transform uiLayer;

    private int _record;

    [Header("Record Panel")]
    public TextMeshProUGUI _recordText;
    public GameObject _recordPanel;

    private Coin _currentCoin;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        Instance = this;
        Debug.Log(_defeatPos.position);
    }
    private void Start()
    {
        if (YandexGame.EnvironmentData.isDesktop)
            _upperSpawnPoint.gameObject.SetActive(true);
        GetRecord(); 
        AddHammers(0);
        SpawnCurrentCoin();
    }
    private void Update()
    {
        if (!_canPlay)
        {
            _mainCanvas.alpha -=Time.deltaTime*2;
            _defeatCanvas.alpha +=Time.deltaTime;
        }
    }

    public void ClickToSpaace()
    {
        if (_nextTimeToClick < Time.time && _canPlay&& !_hammerMode)
        {
            _currentCoin.EnablePhysics();
            _nextTimeToClick = Time.time + clickCooldown;
            GameObject sSound = Instantiate(_spawnSound);
            Destroy(sSound, 1f);
            SpawnCurrentCoin();
        }
        if (_hammerMode)
        {
            Vector3 clickPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitCollider = Physics2D.OverlapCircleAll(clickPos, _bombExplosionRadius);
            for (int i = 0; i < hitCollider.Length; i++)
            {
                if (hitCollider != null && hitCollider[i].gameObject.GetComponent<Coin>() != null)
                    hitCollider[i].gameObject.GetComponent<Coin>().DestroyMe();
            }
            _hammerMode = false;
            _unableCircle.SetActive(false);
            _Button1.SetActive(true);
            _Button2.SetActive(false);
            AddHammers(-1);
            Instantiate(_bombExplosionPS, clickPos, Quaternion.identity);
        }
    }

    public void HammerModeActivator(bool value)
    {
        if(hammers>0)
            _hammerMode = value;
    }

    private void SpawnCurrentCoin()
    {
        _currentCoin = Instantiate(_coins[Random.Range(0, _coins.Length)], _upperSpawnPoint.position, _upperSpawnPoint.rotation);
        _currentCoin.Spawn();
        ShowAd.Instance.ShowAdv();
    }

    private void FixedUpdate()
    {
        if(_canPlay)
            _upperSpawnPoint.position = new Vector2(Mathf.Clamp(_camera.ScreenToWorldPoint(Input.mousePosition).x, _leftCorner.position.x, _rightCorner.position.x),_upperSpawnPoint.position.y);

        if (hammers < 1)
            _imgButton.raycastTarget = false;
        _currentCoin.transform.position = _upperSpawnPoint.position;
    }

    public void AddScore(int amount)
    {
        score += amount;
        _scoreText.text = score.ToString();
    }
    public void GetRecord()
    {
        YandexGame.LoadProgress();
        _record = YandexGame.savesData._score;
        _recordScore.text = _record.ToString();
        _scoreOnCloud = _record;
        _recordText.text = _scoreOnCloud.ToString();
    }
    public void SetRecord()
    {
        if (score > _record)
        {
            YandexGame.savesData._score = score;
            YandexGame.SaveProgress();
            YandexGame.NewLeaderboardScores("LiderBordCandy", score);
        }
    }
    public void DefeatGame()
    {
        _defeatText.text = score.ToString();
        _upperSpawnPoint.gameObject.SetActive(false);
        _canPlay = false;
        _defeatSound.Play();
        if (score > _record)
        {
            SetRecord();
            GetRecord();
        }
        _defeatCanvas.interactable = true;
        _defeatCanvas.blocksRaycasts = true;
        _clickCanves.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddHammers(int amount)
    {
        hammers = YandexGame.savesData._hammers;
        hammers += amount;
        _hText.text = hammers.ToString();
        _imgButton.raycastTarget = true;
        YandexGame.savesData._hammers = hammers;
        YandexGame.SaveProgress();
    }
}
