using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SombreroAtack : MonoBehaviour {
    public float maxSpeed = 5f;
    public float speed = 2f;
    public PlayerController player;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();

        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
        }
    } 
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        rb2d.AddForce(Vector2.right * speed );

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Destruir", 0.05f);
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
}
