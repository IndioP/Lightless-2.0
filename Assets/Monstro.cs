using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    int Health = 0;
    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Flecha")
        {
            Health -= 1;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
