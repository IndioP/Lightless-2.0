using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RubyController : MonoBehaviour
{
    [SerializeField] Transform hand;

    [SerializeField] SpriteRenderer PlayerGFX;

    [SerializeField] private AudioSource PassoSFX;

    [SerializeField] Animator animator;

    PhotonView view;
    
    Rigidbody2D rb2D;
    // Start is called before the first frame update

    [PunRPC]
    void playPasso()
    {
        PassoSFX.Play();
    }

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
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
        if (view.IsMine && PlayerGFX.enabled)
        {
            MovementInput();
            RotateHand();
        }
        else
        {
            rb2D.velocity = new Vector2(0,0);
        }
        
    }

    void RotateHand()
    {
        float angle = Utility.AngleTowardsMouse(hand.position);
        hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    void MovementInput() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log(horizontal, vertical);
        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();

        if(velocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("horizontal", velocity.x);
            animator.SetFloat("vertical", velocity.y);
            view.RPC(nameof(playPasso), RpcTarget.All);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        rb2D.velocity = velocity;
    }
}
