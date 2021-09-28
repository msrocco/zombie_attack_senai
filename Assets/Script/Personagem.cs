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

    // Start is called before the first frame update
    void Start()
    {
        this.qtdPulos = MAX_PULOS;
        this.qtdBones = 0;
        AtualizarHud();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)) 
        {
            Vector3 posicao = this.transform.position;
            posicao.x += velocidade;
            this.transform.position = posicao;
        }
        if(Input.GetKey(KeyCode.A)) 
        {
            Vector3 posicao = this.transform.position;
            posicao.x -= velocidade;
            this.transform.position = posicao;
        }
        if(Input.GetKey(KeyCode.W) && this.qtdPulos > 0) 
        {
            this.qtdPulos--;
            Vector2 force = new Vector2(0f, this.forcaPulo);
            this.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("chao"))
        {
            this.qtdPulos = MAX_PULOS;
        }
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

    void AtualizarHud()
    {
        textBones.GetComponent<Text>().text = this.qtdBones.ToString();
    }
}
