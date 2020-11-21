using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MovableObject
{   
    public int damage;
    public int score;
    
    public Animator animator;
    private GameObject game;
    private float animationDuration;
    private float animationTimer; 

    public void OnEnable() {
        game = GameObject.FindGameObjectWithTag("GameController");
        animationDuration = animator.runtimeAnimatorController.animationClips[0].length;

        animator.enabled = false;
    }

    void Update() {
        if (animationTimer > 0) {
            animationTimer -= Time.deltaTime;
            //print(animationTimer);
            if (animationTimer <= 0) {
                Destroy(this.gameObject);
            }
        } 
        Vector3 screenRightBorders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 screenLeftBorders = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        transform.position += new Vector3(velocity, 0, 0) * Time.deltaTime;
        if (transform.position.x >= screenRightBorders.x || transform.position.x <= screenLeftBorders.x) {
            game.GetComponent<Game>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Bullet")) {
            game.GetComponent<Game>().AddScore(score);
        }
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        animator.enabled = true;
        animationTimer = animationDuration;
    }
}
