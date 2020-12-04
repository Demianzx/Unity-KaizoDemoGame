using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowsercontroller : MonoBehaviour {
    
    public GameObject Fuego;
    public Transform firepoint;
    public Transform player;
    public float maxSpeed = 3f;
    public float speed = 0f;
    public float safeDist = 15f;
    public float currentDist;
    public AudioClip bang;

    private float vida=5;
    private bool shooting=false;
    private float shootDelay=5f;
    private float maximoTiempo=5f;
    private SpriteRenderer spr;
    private Rigidbody2D rb2d;
    private Animator anim;
    private Rigidbody bulletshoot;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        currentDist = Vector3.Distance(transform.position, player.position);

        if (currentDist < safeDist)
        {
            transform.LookAt(player);
            

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float YOffset = 0.8f;
            if (transform.position.y + YOffset < collision.transform.position.y)
            {
                collision.SendMessage("EnemyJump");
                speed = 0;
                vida--;
            }
            else
            {
                collision.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
