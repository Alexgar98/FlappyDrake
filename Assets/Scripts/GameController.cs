using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private int score;
    [SerializeField] private float timeToSpawn = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject destroyPosition;

    public int Score { get => score; set => score = value; }
    public float Speed { get => speed; set => speed = value; }
    public float TimeToSpawn { get => timeToSpawn; set => timeToSpawn = value; }
    public GameObject DestroyPosition { get => destroyPosition; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.Dead)
        {
            gameOverScreen.SetActive(true);
            scoreText.SetActive(false);
        }
        else
        {
            //Debug.Log(speed);
            StartCoroutine("SpawnWall");
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    private IEnumerator SpawnWall()
    {
        GameObject newWall = Instantiate(obstaclePrefab, spawnPosition.transform.position, Quaternion.identity);
        //newWall.transform.position += Vector3.left * 2 * Time.deltaTime;
        /*if (newWall.transform.position.x - destroyPosition.transform.position.x < 0.1f)
            Destroy(newWall);*/
        yield return new WaitForSeconds(timeToSpawn);
    }
}
