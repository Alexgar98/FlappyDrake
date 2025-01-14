using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    bool dead = false;

    public bool Dead { get => dead; set => dead = value; }

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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crashed")
        {
            dead = true;
        }
        if (collision.gameObject.tag == "Success")
        {
            GameController.Instance.AddScore(1);
            GameController.Instance.Speed /= 1.01f;
        }
    }
}
