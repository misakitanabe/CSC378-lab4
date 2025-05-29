using UnityEngine;

public class BossFirePointMover : MonoBehaviour
{
    [SerializeField] private float moveRange = 2f;      // Total range of up/down motion
    [SerializeField] private float moveSpeed = 2f;      // Speed of vertical movement

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition; // relative to the boss
    }

    private void Update()
    {
        float offsetY = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.localPosition = new Vector3(startPos.x, startPos.y + offsetY, startPos.z);
    }
}
