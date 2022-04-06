using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Monstro : MonoBehaviour
{
    int Health = 0;
    PhotonView view;
    Rigidbody2D rb2D;


    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
        view = GetComponent<PhotonView>();
        rb2D = GetComponent<Rigidbody2D>();
        if (view.IsMine)
        {
            Camera.main.GetComponent<CameraScript>().player = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            MovementInput();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        rb2D.velocity = new Vector2(0, 0);
        if (other.gameObject.tag == "Flecha")
        {

            Health -= 1;
            if (Health <= 0)
            {
                    
                PhotonNetwork.Destroy(gameObject);
            }
        }
        
        
        
    }

    void MovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log(horizontal, vertical);
        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        rb2D.velocity = velocity;
    }
}
