using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public GameObject sombrero;
    public Transform firepoint;

    private SpriteRenderer spr;
    private bool jump;
    private bool doubleJump;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool movement= true;
    private GameObject healthbar;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        healthbar = GameObject.Find("HealthBar");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            transform.parent = collision.transform;
            grounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        { 
        grounded = true;
        }
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.transform;
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
            transform.parent = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        if (grounded)
        {
            doubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            }else if(doubleJump){
                jump = true;
                doubleJump = false;
            }
            
        }
        
    }

    void FixedUpdate()
    {
       /*Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.075f;

        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }
        */

        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;
        rb2d.AddForce(Vector2.right * speed * h);
        float limitedspeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedspeed, rb2d.velocity.y);

        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;        
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
                anim.SetBool("Hat", true);
                
                    Instantiate(sombrero,firepoint.position,firepoint.rotation);
                
               
                Invoke("Hat", 0.35f);
            


        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        Debug.Log(rb2d.velocity.x);
    }
    private void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 0, 0);
        healthbar.SendMessage("TakeDamage", 15);
    }
    public void EnemyJump()
    {
        jump = true;
    }
    public void EnemyKnockBack(float enemyPosX) {
        healthbar.SendMessage("TakeDamage", 15);
        jump = true;
        float side = Mathf.Sign(enemyPosX-transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
        movement = false;
        Invoke("EnableMovement", 0.7f);
        spr.color = Color.red;

    }
    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }
    void Hat()
    {
        anim.SetBool("Hat", false);
    }
}
