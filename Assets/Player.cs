using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int fireDelay;
    public GameObject bullet;
    private int lane;
    private int numberOfLanes;

    private float[] lanePositions;

    public float shootCooldown;
    // Start is called before the first frame update
    void Start() {
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
        shootCooldown -= Time.fixedDeltaTime;
    }

    private void Shoot() {
         Instantiate(bullet, transform.position + new Vector3(0.23f, 0, 0), Quaternion.identity);
         shootCooldown = fireDelay;
    }

    private void UpdatePosition() {
        transform.position = new Vector3(transform.position.x, lanePositions[lane], 0);
    }

    public void MoveUp() {
        lane = Mathf.Max(0, lane - 1);
        UpdatePosition();
    }

    public void MoveDown() {
        lane = Mathf.Min(numberOfLanes - 1, lane + 1);
        UpdatePosition();
    }

}
