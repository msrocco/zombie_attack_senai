using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float forceX;
    public float forceY;

    public float timeLife;

    void Start()
    {
        Vector2 forca = new Vector2(forceX, forceY);
        this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);
    }

    void Update()
    {
        timeLife -= Time.deltaTime;
        if (timeLife < 0f) Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("zombie"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
        else if (col.collider.CompareTag("chao")) Destroy(this.gameObject);
    }
}
