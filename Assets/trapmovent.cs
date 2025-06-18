using UnityEngine;

public class MoveAndDestroy : MonoBehaviour
{
    public GameObject targetObject;   // GameObject cần di chuyển
    public float moveSpeed = 4.5f;    // ✅ Tốc độ đã được cập nhật
    private bool shouldMove = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && targetObject != null)
        {
            shouldMove = true;
        }
    }

    void Update()
    {
        if (shouldMove && targetObject != null)
        {
            Vector3 currentPos = targetObject.transform.position;
            Vector3 targetPos = new Vector3(-7f, currentPos.y, currentPos.z);

            // Di chuyển đến vị trí -7 theo trục X
            targetObject.transform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);

            // Khi đến gần vị trí -7 thì xóa đối tượng
            if (Vector3.Distance(targetObject.transform.position, targetPos) < 0.01f)
            {
                Destroy(targetObject);
                shouldMove = false;
            }
        }
    }
}
