using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class Player : MonoBehaviour
{
    public float fireDelay;
    public GameObject bullet;

    public SoundGenerator moveUpSound;
    public SoundGenerator moveDownSound;
    public SoundGenerator shootSound;
    private int lane;
    private int numberOfLanes;

    private float[] lanePositions;

    private float shootCooldown;
    private GameObject game;

    private Vector3 targetPosition;
    private Vector3 dampPosition;
    private System.Random rng;
    // Start is called before the first frame update
    void OnEnable() {
        rng = new System.Random();
        game = GameObject.FindGameObjectWithTag("GameController");
        lane = 1;
        numberOfLanes = transform.GetComponentInParent<Game>().numberOfLanes;
        lanePositions = transform.GetComponentInParent<Game>().lanePositions;
        UpdatePosition();
    }

    // Update is called once per frame
    void Update() {
        if (shootCooldown <= 0) {
            Shoot();
        }
    }

    void FixedUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + new Vector3(0, 0.03f, 0) * rng.Next(-1, 1), ref dampPosition, fireDelay / 2, 100);
        shootCooldown -= Time.fixedDeltaTime;
    }

    private void Shoot() {
        shootSound.Play();
        Instantiate(bullet, transform.position + new Vector3(0.23f, 0, 0), Quaternion.identity);
        shootCooldown = fireDelay;
    }

    private void UpdatePosition() {
        targetPosition = new Vector3(transform.position.x, lanePositions[lane], 0);
    }

    public void MoveUp() {
        moveUpSound.Play();
        lane = Mathf.Max(0, lane - 1);
        UpdatePosition();
    }

    public void MoveDown() {
        moveDownSound.Play();
        lane = Mathf.Min(numberOfLanes - 1, lane + 1);
        UpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Asteroid")) {
            game.GetComponent<Game>().TakeDamage(game.GetComponent<Game>().maxHp);
        }
    }

}
