using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    public GameObject[] targetObjects; // Mảng chứa nhiều GameObject

    void Start()
    {
        // Ẩn tất cả GameObject lúc bắt đầu
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hiện tất cả GameObject khi Player vào trigger
            foreach (GameObject obj in targetObjects)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
