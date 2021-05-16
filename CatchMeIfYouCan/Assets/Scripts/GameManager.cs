 using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [Header("For Game Designer")]
    public float PlayerNormalSpeed;
    public float PlayerSlowedSpeed;
    public float PlayerHorizontalSpeed;
    public float GunFireRate;
    public float EnemySpeed;
    public int EnemyCountPerLevel;

    [Header("Player's Data")]
    public int Level;
    public float Gold;
    public int GunLevel;

    [Header("For Developer")]
    public float PlayerSpeed;
    public GameObject EnemyPrefab;
    public Transform EnemyParent;
    public GameObject Player;
    public GameObject GameOverScene;
    public GameObject MenuButton;
    public Text CountDownText;

    RoadManager _rm;
    int _enemyCounter;
    IEnumerator _coroutine;

    public void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        _rm = RoadManager.Instance;

        PlayerData pData = SaveSystem.LoadPlayer();
        Level = pData.Level;
        Gold = pData.Gold;
        SpawnTrigger.OnPlayerEntry += _rm.MoveRoad;
        Bullet.OnEnemyDestroy += IncrementGold;
        ObstacleTrigger.OnObstacleIn += DecrementSpeed;
        ObstacleTrigger.OnObstacleOut += IncrementSpeed;
        EnemyTrigger.OnHitPlayer += GameOver;
    }
    void IncrementGold() => Gold += 1;
    void DecrementSpeed() => PlayerSpeed = PlayerSlowedSpeed;
    void IncrementSpeed() => PlayerSpeed = PlayerNormalSpeed;

    public void LoadNextLevel()
    {

        _coroutine = Spawner(.2f);
        StartCoroutine(Countdown(3));
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        CountDownText.gameObject.SetActive(true);
        while (count > 0)
        {

            CountDownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        StartCoroutine(_coroutine);
        CountDownText.gameObject.SetActive(false);
        PlayerSpeed = PlayerNormalSpeed;
    }

    void SpawnEnemy()
    {
        GameObject a = Instantiate(EnemyPrefab, EnemyParent) as GameObject;
        a.transform.position = new Vector3(Player.transform.position.x + 10f, 0.6f, Player.transform.position.z);
        StartCoroutine(_coroutine);
    }
    IEnumerator Spawner(float respawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
            _enemyCounter++;
            if (_enemyCounter == EnemyCountPerLevel * (Level + 1))
                break;
        }
    }
    void GameOver()
    {
        MenuButton.SetActive(false);
        GameOverScene.SetActive(true);
    }

    void OnDestroy()
    {
        SpawnTrigger.OnPlayerEntry -= _rm.MoveRoad;
        Bullet.OnEnemyDestroy -= IncrementGold;
        ObstacleTrigger.OnObstacleIn -= DecrementSpeed;
        ObstacleTrigger.OnObstacleOut -= IncrementSpeed;
        EnemyTrigger.OnHitPlayer -= GameOver;
    }
}