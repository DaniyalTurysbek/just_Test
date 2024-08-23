using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;
    public Transform childCamera;
    public float smoothness = 5f;
    public bool rotate;
    public bool move;
    public Vector3 extraOffset;
    public float rotSpeed;
    public float moveSpeed;

    public float mouseRotSpeedX = 10f;
    void Start()
    {
        offset = transform.position - Player.transform.position;
        moveSpeed = Vector3.Distance(offset, extraOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + offset;
    } 
}
