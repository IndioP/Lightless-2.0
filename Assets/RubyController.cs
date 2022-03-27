using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RubyController : MonoBehaviour
{
    [SerializeField] Transform hand;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        view = GetComponent<PhotonView>();
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
        Vector2 position = transform.position;
        position.x = position.x + 1f * horizontal * Time.deltaTime;
        position.y = position.y + 1f * vertical * Time.deltaTime;
        transform.position = position;
    }
}
