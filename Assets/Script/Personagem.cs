using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personagem : MonoBehaviour
{
    public float velocidade;

    public float forcaPulo;
    const int MAX_PULOS = 1;
    private int qtdPulos;

    public GameObject textBones;
    private int qtdBones;

    public AudioClip getBoneSound;

    private Vector3 initialPosition;

    private bool isDead;

    public GameObject shootBone;
    private GameObject actualShoot;

    void Start()
    {
        this.qtdPulos = MAX_PULOS;
        this.qtdBones = 0;
        this.initialPosition = this.transform.position;
        this.isDead = false;
        AtualizarHud();
    }

    void Update()
    {
        VerifyWalk();
        VerifyJump();
        VerifyDeath();
        VerifyShoot();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("chao"))
        {
            this.qtdPulos = MAX_PULOS;

            this.GetComponent<Animator>().SetBool("isJumping", false);
        }
        if(col.collider.CompareTag("zombie")) this.isDead = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("bone"))
        {
            Destroy(col.gameObject);
            this.qtdBones++;
            AtualizarHud();

            this.GetComponent<AudioSource>().PlayOneShot(getBoneSound);
        }
    }

    public void AtualizarHud()
    {
        textBones.GetComponent<Text>().text = this.qtdBones.ToString();
    }

    public void VerifyWalk()
    {
        if (Input.GetKey(KeyCode.D)) WalkRight();
        else if (Input.GetKey(KeyCode.A)) WalkLeft();
        else this.GetComponent<Animator>().SetBool("isRunning", false);
    }

    public void VerifyJump()
    {
        if (Input.GetKey(KeyCode.W)) Jump();
    }

    public void WalkRight()
    {
        Vector3 posicao = this.transform.position;
        posicao.x += velocidade;
        this.transform.position = posicao;

        this.GetComponent<Animator>().SetBool("isRunning", true);
        this.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void WalkLeft()
    {
        Vector3 posicao = this.transform.position;
        posicao.x -= velocidade;
        this.transform.position = posicao;

        this.GetComponent<Animator>().SetBool("isRunning", true);
        this.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Jump()
    {
        if (this.qtdPulos > 0)
        {
            this.qtdPulos--;
            Vector2 force = new Vector2(0f, this.forcaPulo);
            this.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

            this.GetComponent<Animator>().SetBool("isJumping", true);
        }
    }

    public void VerifyDeath()
    {
        Vector3 actualPosition = this.transform.position;
        if (actualPosition.y < -15f) this.isDead = true;

        if(this.isDead)
        {
            this.transform.position = this.initialPosition;
            this.isDead = false;
        }
    }

    public void VerifyShoot()
    {
        if (Input.GetKey(KeyCode.Space) && this.actualShoot == null)
        {
            
            
            Vector3 shotPosition = this.transform.position;
            if (this.GetComponent<SpriteRenderer>().flipX) shotPosition.x -= 1f;
            else shotPosition.x += 1f;
            this.actualShoot = Instantiate(this.shootBone, shotPosition, this.transform.rotation);
            if (this.GetComponent<SpriteRenderer>().flipX) 
                this.actualShoot.GetComponent<Shoot>().forceX *= -1;
        }
    }
}
