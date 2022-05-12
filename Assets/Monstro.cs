using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Monstro : MonoBehaviour
{
    public int Health = 0;
    PhotonView view;
    Rigidbody2D rb2D;
    [SerializeField] private AudioSource PassoSFX;
    [SerializeField] private AudioSource MorteSFX;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ArrowPrefab;
    public Texture Sangue, Contorno;

    [PunRPC]
    void playPasso()
    {
        PassoSFX.Play();
    }

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

    [PunRPC]
    public void OnGUI(){
        GUI.DrawTexture (new Rect (Screen.width / 40, Screen.height / 40, Screen.width / 5.5f/5*Health, Screen.height / 25), Sangue);
        GUI.DrawTexture (new Rect (Screen.width/40, Screen.height/40, Screen.width/5, Screen.height/8), Contorno);
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        rb2D.velocity = new Vector2(0, 0);
        if (other.gameObject.tag == "Flecha"){
            
            Health -= 1;
            if (Health <= 0){
                    
                //PhotonNetwork.Destroy(gameObject);
                PhotonNetwork.LoadLevel("PlayersWin");
            }
            
            if (view.IsMine)
            {
                
                //Debug.Log("Destruindo flexa");
                //PhotonNetwork.Destroy(other.gameObject);
                Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                Vector2 pos = new Vector2(0,0);
                Arrow Arrow = PhotonNetwork.Instantiate(ArrowPrefab.name, pos, rot).GetComponent<Arrow>();
                //view.RPC(nameof(Arrow.destroySelf), RpcTarget.All);
                //Arrow.ArrowVelocity = 0f;
            }
            Debug.Log("ARROW PREGAB --");
        }        
    }

    void MovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log("Horizontal: " + horizontal);
        Debug.Log("Vertical: " + vertical);

        //Debug.Log(horizontal, vertical);
        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        if (velocity != Vector2.zero)
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
