using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Monstro : MonoBehaviour
{
    int Health = 0;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
        view = GetComponent<PhotonView>();
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
        if (other.gameObject.tag == "Flecha")
        {
            Health -= 1;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    void MovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log(horizontal, vertical);
        Vector2 position = transform.position;
        position.x = position.x + 1f * horizontal * Time.deltaTime;
        position.y = position.y + 1f * vertical * Time.deltaTime;
        transform.position = position;
    }
}
