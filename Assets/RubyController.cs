using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RubyController : MonoBehaviour
{
    [SerializeField] Transform hand;
    PhotonView view;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
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
        if (view.IsMine)
        {
            MovementInput();
            RotateHand();
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
        rb2D.velocity = velocity;
    }
}
