using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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

    }

    private void LateUpdate()
    {
        transform.position = followPlayer.position;
        transform.rotation = followPlayer.rotation;
    }
}
