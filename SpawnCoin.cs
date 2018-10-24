using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour {

    private bool boxHit;
    public Transform boxCheck;
    public float checkHeight;
    public float checkWidth;
    public LayerMask isPlayer;

    public float delay;
    public int numberOfCoins;
    private int i;


    // Use this for initialization
    void Start () {
        //gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        boxHit = Physics2D.OverlapBox(boxCheck.position, new Vector2(checkWidth, checkHeight), 0, isPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player" && boxHit) {
            Debug.Log("it worked");
            while (numberOfCoins > 0) {
                if(gameObject.activeInHierarchy == true) {
                    //gameObject.SetActive(true);
                    
                    StartCoroutine(PerformDespawn());

                }
            }
        }
    }

    IEnumerator PerformDespawn() {
        yield return new WaitForSeconds(delay);
        //gameObject.SetActive(false);
        
    }
}
