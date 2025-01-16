using UnityEngine;

public class WallMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.Instance.Dead)
        {
            transform.position += Vector3.left * GameController.Instance.Speed * (1 / GameController.Instance.Multiplier) * Time.deltaTime;
            if (transform.position.x - GameController.Instance.DestroyPosition.transform.position.x < -0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
