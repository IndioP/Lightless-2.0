using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        //Destroy(gameObject, 4f);
        gameObject.tag = "Flecha";
        ArrowVelocity = 2f;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Jogador")
        {
            Destroy(gameObject);
        }
        else
        {
            ArrowVelocity = 0f;
        }
        
    }
}
