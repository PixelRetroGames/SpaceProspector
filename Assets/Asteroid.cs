using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MovableObject
{   
    public int damage;
    public int score;
    private GameObject game;

    void Start() {
        game = GameObject.FindGameObjectWithTag("GameController");
    }
    void Update() {
        Vector3 screenRightBorders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 screenLeftBorders = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        transform.position += new Vector3(velocity, 0, 0) * Time.deltaTime;
        if (transform.position.x >= screenRightBorders.x || transform.position.x <= screenLeftBorders.x) {
            game.GetComponent<Game>().TakeDamage(damage);
            print("au" + game.GetComponent<Game>().hp);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag.Equals("Bullet")) {
            game.GetComponent<Game>().AddScore(score);
        }
        Destroy(this.gameObject);
    }
}
