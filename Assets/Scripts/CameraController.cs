using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room Camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Player Camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    [SerializeField] private float minY;  // Minimum Y value for the camera

    private void Update()
    {
        // Player Camera X follow
        float targetX = player.position.x + lookAhead;
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        // Player Camera Y follow, but clamp to minY
        float targetY = Mathf.Max(player.position.y, minY);

        // Smoothly move camera to target position (optional: can adjust smoothing)
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
