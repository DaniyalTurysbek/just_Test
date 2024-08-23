using System.Collections; 
using UnityEngine; 

public class enemySRC : MonoBehaviour
{
    public bool invul = false;
    MeshRenderer meshRender; 
    public Material blinkMaterial;
    public Material normalMaterial;
    private int enemyHP = 3;
    Scr scr;
    GameObject player;

    void Start()
    { 
        meshRender = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("play");
        scr = player.GetComponent<Scr>();
    } 

    void Update()
    { 
        if(scr.GetCoins() == 41) 
        { 
            gameObject.SetActive(false);
        }
    } 

    public void startBlink()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("final");
        StartCoroutine(Invulabirity());
        enemyHP -= 1;
        Debug.Log("enemy hp: " + enemyHP);
        if(enemyHP == 0)
        {
            enemy.SetActive(false);
        }
    }
    IEnumerator Invulabirity()
    {
        invul = true;
        for (int i = 0; i < 7; i++)
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
        invul = false;
    }
}
