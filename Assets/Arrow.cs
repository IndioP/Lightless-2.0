using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [SerializeField] Rigidbody2D rb;
    PhotonView view;


    private void Start()
    {
        //Destroy(gameObject, 4f);
        view = GetComponent<PhotonView>();
        gameObject.tag = "Flecha";
        ArrowVelocity = 2f;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
        foreach(var player in players)
        {
            Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), true);
        }
        GameObject monstro = GameObject.FindGameObjectWithTag("Monstro");
        Physics2D.IgnoreCollision(monstro.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), false);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Jogador")
        {
            //Destroy(gameObject);
            if (view.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);

            }
        }
        else
        {
            ArrowVelocity = 0f;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
            foreach (var player in players)
            {
                Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), false);
            }
            GameObject monstro = GameObject.FindGameObjectWithTag("Monstro");
            Physics2D.IgnoreCollision(monstro.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), true);
        }
        
    }
}
