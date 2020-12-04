using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoController : MonoBehaviour {
    public float maxSpeed = 10f;
    public float speed = 10f;
    public bowsercontroller AI;
    private bool vivo = true;
    private Rigidbody2D rb2d;
    private Animator anim;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        rb2d.AddForce(Vector2.left * speed);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.SendMessage("EnemyKnockBack", transform.position.x);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    }
