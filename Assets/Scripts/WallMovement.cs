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
        transform.position += Vector3.left * GameController.Instance.Speed * 10 * Time.deltaTime;
        if (transform.position.x - GameController.Instance.DestroyPosition.transform.position.x < 0.1f)
            Destroy(this);
    }
}
