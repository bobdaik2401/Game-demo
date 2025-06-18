using UnityEngine;

public class MovementGrounds : MonoBehaviour
{
    public Transform[] targets;
    public float[] speeds;
    public float[] pointAs;
    public float[] pointBs;

    // Mảng hướng di chuyển cho từng đối tượng (true = phải, false = trái)
    private bool[] movingRights;

    void Start()
    {
        // Khởi tạo hướng ban đầu cho từng đối tượng
        movingRights = new bool[targets.Length];
        for (int i = 0; i < movingRights.Length; i++)
        {
            movingRights[i] = true;
        }
    }

    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null) continue;

            Vector3 pos = targets[i].position;

            if (movingRights[i])
            {
                pos.x += speeds[i] * Time.deltaTime;
                if (pos.x >= pointBs[i])
                {
                    pos.x = pointBs[i];
                    movingRights[i] = false;
                }
            }
            else
            {
                pos.x -= speeds[i] * Time.deltaTime;
                if (pos.x <= pointAs[i])
                {
                    pos.x = pointAs[i];
                    movingRights[i] = true;
                }
            }

            targets[i].position = pos;
        }
    }
}
