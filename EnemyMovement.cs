using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public Transform wallHitBox;
    public float wallHitWidth;
    public float wallHitHeight;
    public LayerMask isGround;

    //Goomba Death
    public Transform headcheck;
    public float headWidth;
    public float headHeight;
    //public float checkRadius;
    public LayerMask isPlayer;
    private bool playerHit;
    private Animator anim;
    private Animator animP;

    public float delay;
    

    private bool wallHit;

    //Player Death
    private bool goombaHit;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate((speed * -1) * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if(wallHit == true) {
            speed = speed * -1;
        }

        playerHit = Physics2D.OverlapBox(headcheck.position, new Vector2(headWidth, headHeight), 0, isPlayer);
        //Debug.Log(playerHit);

        goombaHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isPlayer);
    }
        

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player" && playerHit) {
            anim.SetBool("isDead", true);
            speed = 0;
            
            StartCoroutine(PerformGoombaDeath());
            //Debug.Log("Goomba dead");
        } else if(collision.collider.tag == "Player" && goombaHit) {
            animP = collision.gameObject.GetComponent<Animator>();
            animP.SetBool("isPDead", true);
            
            StartCoroutine(PerformPlayerDeath(collision));
        }
        
        if(collision.collider.tag == "Pickup") {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }

    } 

    IEnumerator PerformGoombaDeath() {
        
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator PerformPlayerDeath(Collision2D collision) {
        yield return new WaitForSeconds(delay);
        Destroy(collision.gameObject);
    }
}
