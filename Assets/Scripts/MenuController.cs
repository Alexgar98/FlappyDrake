using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text highestFlight;
    [SerializeField] private TMP_Text highestFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highestFlight.text = "Highest score: " + PlayerPrefs.GetInt("Highest Score");
        highestFire.text = "Highest score: " + PlayerPrefs.GetInt("Highest Fire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
