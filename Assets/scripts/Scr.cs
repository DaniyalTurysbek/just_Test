using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Scr : MonoBehaviour
{
    public float jumpForce = 10f;
    public int maxjumps = 2;
    private int remainingJumps;
    private float coins =0;
    public int bossHP = 6;

    private Rigidbody rb;
    private bool isGrounded = true;
    public float speed = 15f;
    MeshRenderer meshRender;
    public Material blinkMaterial;
    public Material normalMaterial;
    private bool invulnerable = false;
    public float HP = 3f;
    public float enemyHP = 3f;
    GameObject bo; 
    public BossBehaviour bossBeh;
    GameObject goE;
    public GoEnemy GoEnemy;
    bool touched;
    public GameObject checkPoint;
    public GameObject damage;
    public GameObject teleport;
    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody>(); 
        meshRender = rb.GetComponent<MeshRenderer>();
        bo = GameObject.FindGameObjectWithTag("bbb"); 
        bossBeh = bo.GetComponent<BossBehaviour>();
        goE = GameObject.FindGameObjectWithTag("goEnemy");
        GoEnemy = goE.GetComponent<GoEnemy>();
        goE.SetActive(false);
        touched = false;
        checkPoint = GameObject.FindWithTag("checkPoint");
        damage = GameObject.FindGameObjectWithTag("destroyMe");
        damage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHP == 0)
        {
            bo.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D) )
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S) )
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (isGrounded)
        {
            remainingJumps = maxjumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            Jump();
        }
        if((HP == 0 && !touched))
        {
            RestartGame();
        }
        if (gameObject.transform.localScale.x < 1 && touched)
        {
            HP = 3;
            gameObject.transform.position = checkPoint.transform.position;
        }
    }
    public int GetCoins()
    {
        return (int)coins;
    }
    void RestartGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    private void Jump()
    {
        Vector3 jumpVolocity = new Vector3(rb.velocity.x * 2, jumpForce, rb.velocity.z * 2);
        rb.velocity = jumpVolocity;
        remainingJumps--;
    } 
    private void OnCollisionEnter(Collision collision )
    { 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("checkPoint"))
        {
            touched = true;
            Debug.Log("Touched the checkpoint");
        }
        if ((collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("goEnemy")) && !invulnerable)
        {
            HP -= 1;
            gameObject.transform.localScale = transform.localScale * 0.5f;
            StartCoroutine(Invulabirity()); 
        }
        if (collision.gameObject.CompareTag("finalEnemyes") && !invulnerable)
        {
            gameObject.transform.localScale = transform.localScale * 0.5f;
            StartCoroutine(Invulabirity());
        }
        if (collision.gameObject.CompareTag("GGGG"))
        { 
            collision.transform.parent.GetComponent<enemySRC>().startBlink();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("checkPoint"))
        {
            while(gameObject.transform.localScale.x <= 3f)
            { 
                gameObject.transform.localScale *= 1.09f;
            }
        }
    }
    IEnumerator Invulabirity()
    {
        invulnerable = true;
        for(int i = 0; i < 7; i++)
        {
            if (i % 2 == 0)
            {
                meshRender.material = blinkMaterial;
            }
            else
                meshRender.material = normalMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        meshRender.material = normalMaterial;
        invulnerable = false;
    }
    private void OnTriggerEnter(Collider collider )
    {
        if (collider.gameObject.CompareTag("coin"))
        {
            GameObject door = GameObject.FindGameObjectWithTag("door");
            
            if(coins < 22)
            {
                coins += 1;
                Debug.Log(coins);
                Destroy(collider.gameObject);
                if (coins == 6)
                {
                    DestroyDoor();
                }
                if (coins == 7)
                {
                    goE.SetActive(true);
                }
                if (coins == 14)
                {
                    DestroyDoor2();
                    goE.SetActive(false);
                }
                if (coins == 21)
                {
                    DestroyDoor3();
                }
            }
            else
            { 
                if(damage.activeSelf == false)
                {
                    coins += 1;
                    Debug.Log(coins);
                    Destroy(collider.gameObject);
                    
                    if (coins > 24)
                    {
                        bossBeh._laserSpeed = 8f;
                    }
                    if (coins % 2 == 0)
                    {
                        damage.SetActive(true);
                    } 
                } 
            }
            if (coins == 22)
            {
                bossBeh.StartHaha();
            } 
        }
        if (collider.gameObject.CompareTag("destroyMe"))
        {
            bossHP -= 1;
            damage.SetActive(false);
            Debug.Log(bossHP);
            if(bossHP == 0)
            {
                Debug.Log("You WIN!");
                bossBeh.win();
            }
        }
        if (collider.gameObject.CompareTag("out1"))
        {
            if(coins < 6)
            {
                Transform spherTransform = gameObject.transform;
                spherTransform.position = new Vector3(-37, 39, 0); 
             }
        }
        if (collider.gameObject.CompareTag("out2"))
        {
            if (coins < 14)
            {
                Transform spherTransform = gameObject.transform;
                spherTransform.position = new Vector3(-22, 19, 5);
            }
        }
        if (collider.gameObject.CompareTag("out3"))
        {  
            if(!touched)
            {
                Transform spherTransform = gameObject.transform;
                spherTransform.position = new Vector3(-18, 1, 35);
            } 
            else
            {
                gameObject.transform.position = new Vector3(5, 0, -214);
            }
        }
        if (collider.gameObject.CompareTag("apteka") && HP < 3)
        {
            gameObject.transform.localScale = transform.localScale * 2f;
            HP += 1;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BigApteka") && gameObject.transform.localScale.x < 3)
        {
            gameObject.transform.localScale *= 1.009f;
        }
    }

    private void DestroyDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("door"); 
        if (door != null)
        {
            door.SetActive(false);
            Debug.Log("Door destroyed!");
            door.transform.localScale = transform.localScale * 0.5f;
        } 
    }

    private void DestroyDoor2()
    {
        GameObject door2 = GameObject.FindGameObjectWithTag("door2");
        if(door2!= null)
        {
            door2.SetActive(false);
        }
    }
    private void DestroyDoor3()
    {
        GameObject door2 = GameObject.FindGameObjectWithTag("door3");
        if (door2 != null)
        {
            door2.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
