using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject != null)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.down * GameManager.instance.fallSpeed;
        }
    }

    public bool isMove()
    {
        return rb != null ? rb.velocity.magnitude > 0: false;
    }

}
