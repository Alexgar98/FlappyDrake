using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    bool dead = false;
    private Rigidbody2D rb;
    private int jumped = 0;
    [SerializeField] private int jumpForce = 10;

    public bool Dead { get => dead; set => dead = value; }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            jumped++;
            Jump();
        }
        else
        {
            jumped = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crashed")
        {
            dead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (collision.gameObject.tag == "Success")
        {
            GameController.Instance.Score++;
            GameController.Instance.Multiplier /= 1.1f;
            Debug.Log("Multiplier: " + GameController.Instance.Multiplier);
        }
    }

    private void Jump()
    {
        if (jumped == 1)
        {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }
}
