using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

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
        StartCoroutine("SpawnWall");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.Dead)
        {
            gameOverScreen.SetActive(true);
            scoreText.gameObject.SetActive(false);
            finalScoreText.text = "Game Over\nYour score was " + score.ToString();
            if (score > PlayerPrefs.GetInt("Highest Score"))
            {
                PlayerPrefs.SetInt("Highest Score", score);
            }
        }
    }

    private IEnumerator SpawnWall()
    {
        Vector3 position;
        float randomMovement;
        while (!PlayerController.Instance.Dead)
        {
            position = spawnPosition.transform.position;
            randomMovement = Random.Range(-2.5f, 2.5f);
            Debug.Log("Random movement: " + randomMovement);
            position += (Vector3.up * randomMovement);
            GameObject newWall = Instantiate(obstaclePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(timeToSpawn * multiplier);
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
