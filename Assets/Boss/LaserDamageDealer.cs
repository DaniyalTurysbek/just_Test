using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamageDealer : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "play")
            other.transform.localScale = other.transform.localScale * 0.99f;
    }
}
