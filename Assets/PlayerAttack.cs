using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject ArrowPrefab;

    [SerializeField] SpriteRenderer ArrowGFX;
    [SerializeField] SpriteRenderer PlayerGFX;
    [SerializeField] SpriteRenderer EsqueletoGFX;

    [SerializeField] Slider BowPowerSlider;

    [SerializeField] Transform Bow;
    [Range(0, 10)]

    [SerializeField] float BowPower;

    [Range(0, 3)]

    [SerializeField] float MaxBowCharge;

    float BowCharge;
    bool CanFire = true;
    PhotonView view;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
        gameObject.tag = "Jogador";
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine && PlayerGFX.enabled)
        {
            if (Input.GetMouseButton(0) && CanFire)
            {
                ChargeBow();
            }
            else if (Input.GetMouseButtonUp(0) && CanFire)
            {
                FireBow();
            }
            else
            {
                //if(BowCharge > 0f)
                //{
                //    BowCharge = 0.1f * Time.deltaTime;
                //}else
                //{
                BowCharge = 0f;
                //CanFire = true;

                //}
                BowPowerSlider.value = BowCharge;
            }
        }
    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;
        BowCharge += Time.deltaTime;
        BowPowerSlider.value = BowCharge;
        if(BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if(BowCharge > MaxBowCharge)
        {
            BowCharge = MaxBowCharge;
        }
        float ArrowSpeed = BowCharge + BowPower;
        float angle = Utility.AngleTowardsMouse(Bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        //Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
        Arrow Arrow = PhotonNetwork.Instantiate(ArrowPrefab.name, Bow.position, rot).GetComponent<Arrow>();
        Arrow.ArrowVelocity = ArrowSpeed;
        CanFire = false;
        ArrowGFX.enabled = false;
    }
    [PunRPC]
    void VivoMorto(int Id)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
        foreach (var player in players)
        {
            PhotonView playerView = player.GetComponent<PhotonView>();
            if(playerView.ViewID == Id)
            {
                SpriteRenderer[] sprites = player.GetComponentsInChildren<SpriteRenderer>();
                foreach(var sprite in sprites)
                {
                    sprite.enabled = !sprite.enabled;
                }
                //player.PlayerGFX.enabled = !player.PlayerGFX.enabled;
                //player.EsqueletoGFX.enabled = !player.EsqueletoGFX.enabled;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //rb2D.velocity = new Vector2(0, 0);
        if (other.gameObject.tag == "Flecha")
        {
            CanFire = true;
        }
        else if (other.gameObject.tag == "Monstro" && PlayerGFX.enabled)
        {
            //PlayerGFX.enabled = false;
            //EsqueletoGFX.enabled = true;
            //Destroy(gameObject);
            //if (view.IsMine)
            //{
            //    PhotonNetwork.Destroy(gameObject);
            //}
            view.RPC(nameof(VivoMorto), RpcTarget.All, view.ViewID);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
            foreach (var player in players)
            {
                Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), false);
            }
            GameObject monstro = GameObject.FindGameObjectWithTag("Monstro");
            Physics2D.IgnoreCollision(monstro.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), true);
        }
        else if (other.gameObject.tag == "Jogador" && EsqueletoGFX.enabled)
        {
            //PlayerGFX.enabled = true;
            //EsqueletoGFX.enabled = false;
            view.RPC(nameof(VivoMorto), RpcTarget.All, view.ViewID);

            GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
            foreach (var player in players)
            {
                Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), true);
            }
            GameObject monstro = GameObject.FindGameObjectWithTag("Monstro");
            Physics2D.IgnoreCollision(monstro.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>(), false);

        }
    }
}
