using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //quem é o personagem a ser perseguido?
    public GameObject personagem;

    //velocidade do andar
    public float velocidade;

    //Andar
    private bool andandoEsquerda;
    private bool andandoDireita;

    //Posição inicial
    private Vector3 initialPosition;

    void Start()
    {
        this.andandoEsquerda = false;
        this.andandoDireita = false;
        this.initialPosition = this.transform.position;
        this.personagem = GameObject.Find("Character");
    }

    void Update()
    {
        VerificaAndar();
        VerificaMorte();
    }

    public void VerificaAndar()
    {
        if(personagem.transform.position.x > this.transform.position.x)
        {
            this.andandoDireita = true;
            this.andandoEsquerda = false;
        }
        else
        {
            this.andandoDireita = false;
            this.andandoEsquerda = true;
        }

        if (andandoDireita) AndarDireita();
        else if (andandoEsquerda) AndarEsquerda();
    }

    public void AndarDireita()
    {
        Vector3 posicao = this.transform.position;
        posicao.x += velocidade;
        this.transform.position = posicao;

        this.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void AndarEsquerda()
    {
        Vector3 posicao = this.transform.position;
        posicao.x -= velocidade;
        this.transform.position = posicao;

        this.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void VerificaMorte()
    {
        Vector3 actualPosition = this.transform.position;
        if (actualPosition.y < -15f)
        {
            this.transform.position = this.initialPosition;
        }
    }

}