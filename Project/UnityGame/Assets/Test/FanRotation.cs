using UnityEngine;

public class FanRotation : MonoBehaviour
{
    // 旋转速度（度/秒）
    public float rotationSpeed = 100f;

    void Update()
    {
        // 每帧绕Y轴旋转
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}