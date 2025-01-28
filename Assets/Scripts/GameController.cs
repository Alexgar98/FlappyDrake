using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private int score = 0;
    private float multiplier = 1f;
    [SerializeField] private float timeToSpawn = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject destroyPosition;
    private int birds = 10;

    public float Multiplier { get => multiplier; set => multiplier = value; }
    public int Score { get => score; set => score = value; }
    public float Speed { get => speed; }
    public float TimeToSpawn { get => timeToSpawn; set => timeToSpawn = value; }
    public GameObject DestroyPosition { get => destroyPosition; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("FireMode"))
        {
            multiplier = 0.4f;
        }
        StartCoroutine("SpawnWall");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.Dead || birds == 0)
        {
            gameOverScreen.SetActive(true);
            scoreText.gameObject.SetActive(false);
            finalScoreText.text = "Game Over\nYour score was " + score.ToString();
            if (SceneManager.GetActiveScene().name.Equals("FlightMode"))
            {
                if (score > PlayerPrefs.GetInt("Highest Score"))
                {
                    PlayerPrefs.SetInt("Highest Score", score);
                }
            }
            else if (SceneManager.GetActiveScene().name.Equals("FireMode"))
            {
                if (score > PlayerPrefs.GetInt("Highest Fire"))
                {
                    PlayerPrefs.SetInt("Highest Fire", score);
                }
            }
        }
    }

    private IEnumerator SpawnWall()
    {
        Vector3 position;
        float randomMovement;
        while (!PlayerController.Instance.Dead && birds > 0)
        {
            position = spawnPosition.transform.position;
            randomMovement = Random.Range(-2.5f, 2.5f);
            Debug.Log("Random movement: " + randomMovement);
            position += (Vector3.up * randomMovement);
            GameObject newWall = Instantiate(obstaclePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(timeToSpawn * multiplier);
            if (SceneManager.GetActiveScene().name == "FireMode")
                birds--;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private void OnGUI()
    {
        scoreText.text = score.ToString("000");
    }
}
