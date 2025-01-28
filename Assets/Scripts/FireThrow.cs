using UnityEngine;

public class FireThrow : MonoBehaviour
{
    public GameObject fireballPrefab;  // Assign your fireball sprite prefab
    public float launchForceMultiplier = 1f; // Adjust to control launch force

    private GameObject currentFireball;
    private SpringJoint2D springJoint;
    private Camera mainCamera;
    private Vector2 startTouchPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleTouches();
    }

    void HandleTouches()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Handle the first touch; extend this for multi-touch

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    SpawnFireball(touch.position);
                    break;

                case TouchPhase.Moved:
                    UpdateSpringJoint(touch.position);
                    break;

                case TouchPhase.Ended:
                    LaunchFireball(touch.position);
                    break;
            }
        }
    }

    void SpawnFireball(Vector2 touchPosition)
    {
        if (currentFireball != null) return; // Prevent multiple spawns

        // Convert touch position to world coordinates
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
        currentFireball = Instantiate(fireballPrefab, worldPosition, Quaternion.identity);

        // Add and configure the spring joint
        springJoint = currentFireball.GetComponent<SpringJoint2D>();
        if (springJoint != null)
        {
            springJoint.connectedAnchor = worldPosition; // Set spring anchor point
            startTouchPosition = touchPosition;         // Store initial touch position
        }
    }

    void UpdateSpringJoint(Vector2 touchPosition)
    {
        if (currentFireball == null || springJoint == null) return;

        // Update the spring joint anchor to follow the finger
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
        springJoint.connectedAnchor = worldPosition;
    }

    void LaunchFireball(Vector2 touchPosition)
    {
        if (currentFireball == null || springJoint == null) return;

        // Calculate the swipe direction
        Vector2 swipeDirection = (startTouchPosition - touchPosition).normalized;

        // Apply a launch force based on the direction and distance
        Rigidbody2D rb = currentFireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float swipeDistance = Vector2.Distance(startTouchPosition, touchPosition);
            rb.AddForce(swipeDirection * swipeDistance * launchForceMultiplier, ForceMode2D.Impulse);
        }

        // Destroy the spring joint to release the fireball
        Destroy(springJoint);
        springJoint = null;
        currentFireball = null; // Allow new fireballs to be spawned
    }
}
