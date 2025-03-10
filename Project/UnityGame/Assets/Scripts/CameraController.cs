using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("最小距离")]
    public float minDistance = 2;
    [Header("最大距离")]
    public float maxDistance = 8;
    [Header("灵敏度")]
    public float sensitivity = 0.5f;
    public Transform followPlayer;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - followPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraNearAndFar();
    }

    public void CameraNearAndFar()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0 || scrollWheel < 0)
        {
            followPlayer.localPosition = followPlayer.localPosition + (scrollWheel > 0 ? -1 : 1) * followPlayer.localPosition.normalized * sensitivity;
            if (followPlayer.localPosition.magnitude > maxDistance)
            {
                followPlayer.localPosition = followPlayer.localPosition.normalized * maxDistance;
            }
            else if (followPlayer.localPosition.magnitude < minDistance)
            {
                followPlayer.localPosition = followPlayer.localPosition.normalized * minDistance;
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = followPlayer.position;
        transform.rotation = followPlayer.rotation;
    }
}
