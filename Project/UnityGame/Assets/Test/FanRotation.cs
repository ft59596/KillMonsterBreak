using UnityEngine;

public class FanRotation : MonoBehaviour
{
    // ��ת�ٶȣ���/�룩
    public float rotationSpeed = 100f;

    void Update()
    {
        // ÿ֡��Y����ת
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}