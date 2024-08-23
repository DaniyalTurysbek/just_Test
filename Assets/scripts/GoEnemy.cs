using UnityEngine;

public class GoEnemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("play").transform; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * moveSpeed);
        } 
    }
}
