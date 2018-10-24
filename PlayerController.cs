using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private Animator anim;

    public float speed;
    public float jumpforce;
    public float delay;

    //ground check stuff
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    //sound variables
    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip endClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    //private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //for audio
    void Awake() {
        source = GetComponent<AudioSource>();
    }

    private void Update() {

    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        //Debug.Log(isOnGround);

        

        //running animation
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        //jumping animation
        /*if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) {
            anim.SetTrigger("jump");
        }*/

        //replace with animation
        if (facingRight == false && moveHorizontal > 0) {
            Flip();
        } else if(facingRight == true && moveHorizontal < 0){
            Flip();
        }

        //pause speed for death
        if (anim.GetBool("isPDead")) {
            speed = 0;
        }

        if(transform.position.x > 187) {
            source.PlayOneShot(endClip);
        }
    }

    //replace with animation
    void Flip() {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision) {
       if(collision.collider.tag == "Ground" && isOnGround) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                rb2d.velocity = Vector2.up * jumpforce;
                anim.SetBool("jump", true);
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(jumpClip);
            } else{
                anim.SetBool("jump", false);
            }
        }
        /*if (collision.collider.tag == "Enemy" && transform.position.y < 1) {
            anim.SetBool("isPDead", true);
            StartCoroutine(PerformPlayerDeath());
        }*/

        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        /*if (collision.collider.tag == "Enemy") {
            anim.SetBool("isPDead", true);
            StartCoroutine(PerformPlayerDeath());
        }*/

        if (collision.collider.tag == "Pickup") {
            collision.gameObject.SetActive(false);
            if (collision.gameObject.activeInHierarchy == false) {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(coinClip);
            }
        }
        
    }

    /*IEnumerator PerformPlayerDeath() {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }*/

}
