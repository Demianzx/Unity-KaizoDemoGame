using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float maxSpeed = 1f;
    public float speed = 1f;

    private bool vivo = true;
    private Rigidbody2D rb2d;
    private Animator anim;
    private PolygonCollider2D pc2d,pchijo;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc2d = GetComponent<PolygonCollider2D>();
        pchijo = GetComponentInChildren<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (vivo)
        {
            rb2d.AddForce(Vector2.right * speed);
            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        
            if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
            {
                speed = -speed;
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            }

            if (speed < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (speed > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float YOffset = 0.4f;
            if (transform.position.y + YOffset < collision.transform.position.y)
            {
                collision.SendMessage("EnemyJump");
                anim.SetBool("dead", true);
                Invoke("Destroy", 0.5f);
                
                vivo = false;
                speed = 0;
            }else
            {
                collision.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
        if (collision.gameObject.tag == "Atack")
        {
            anim.SetBool("dead", true);
            Invoke("Destroy", 0.5f);
            vivo = false;
            speed = 0;
        }

        }
    void Destroy()
    {
        Destroy(gameObject);
    }
}