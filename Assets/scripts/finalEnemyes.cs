using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class finalEnemyes : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    GameObject player;
    Scr playerScr;
    bool goes;
    MeshRenderer meshRederer; 

    public bool invul { get; private set; }

    void Start()
    {
        player = GameObject.FindWithTag("play");
        playerScr = player.GetComponent<Scr>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("play").transform; 
        goes = false;
        meshRederer = gameObject.GetComponent<MeshRenderer>();
        meshRederer.enabled = false;
    }
    private void Update()
    {
        if(playerScr.GetCoins() <= 24)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            meshRederer.enabled = false;
            goes = false; 
        }
        else if(playerScr.GetCoins() >24)
        {
            gameObject.GetComponent <Collider>().isTrigger = false;
            meshRederer.enabled = true;
            goes = true; 
        }
    }
    void FixedUpdate()
    {
        if (target != null && goes)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * moveSpeed);
        }
    } 
}
