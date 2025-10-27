using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]

    // Player Transform to follow
    public Transform player;
    // How far ahead of the player the camera should look
    public float aheadDistance = 2f;
    // How quickly the camera follows the player
    public float smoothSpeed = 5f;

    private float fixedY;
    private float fixedZ;
    private float initialCameraX;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("CameraFollow: Player not assigned in Inspector!");
            enabled = false;
            return;
        }

        // Store initial Y/Z so the camera never moves vertically
        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        // Camera should never move left of this X
        initialCameraX = transform.position.x;
    }

    void LateUpdate()
    {
        // Get desired X (follow player horizontally)
        float targetX = player.position.x + aheadDistance;

        // Prevent camera from going backward past the initial position
        targetX = Mathf.Max(targetX, initialCameraX);

        // Create a smooth follow effect (keep Y/Z fixed)
        Vector3 targetPosition = new Vector3(targetX, fixedY, fixedZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
