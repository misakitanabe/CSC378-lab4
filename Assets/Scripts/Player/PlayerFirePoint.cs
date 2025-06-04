using UnityEngine;

public class PlayerFirePoint : MonoBehaviour
{
    [SerializeField] private float moveRange = 1f;      // Total range of up/down motion
    [SerializeField] private float moveSpeed = 1f;      // Speed of vertical movement

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
