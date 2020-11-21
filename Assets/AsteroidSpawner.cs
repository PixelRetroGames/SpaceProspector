using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public float initialSpeed;
    public float initialSpawnChance;
    public float spawnDelay;
    public GameObject asteroid;
    public float spawnCooldownTime;
    public float spawnChance;
    public float speed;

    private System.Random rng;
    private int numberOfLanes;
    private float[] lanePositions;

    public float[] laneCooldown;

    public float spawnCooldown;

    private float positionX;
    // Start is called before the first frame update
    public void OnEnable() {
        speed = initialSpeed;
        spawnChance = initialSpawnChance;
        positionX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + 2;
        rng = new System.Random();
        numberOfLanes = transform.GetComponentInParent<Game>().numberOfLanes;
        lanePositions = transform.GetComponentInParent<Game>().lanePositions;
        laneCooldown = new float[numberOfLanes];
        spawnCooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {
        spawnCooldown = Mathf.Max(0, spawnCooldown - Time.fixedDeltaTime);
        for (int i = 0; i < numberOfLanes; i++) {
            laneCooldown[i] = Mathf.Max(0, laneCooldown[i] - Time.fixedDeltaTime);
        }
    }
    void Update() {
        if (spawnCooldown > 0) {
            return;
        }

        spawnCooldown = spawnCooldownTime;
        if (rng.Next(100) < spawnChance) {
            List<int> freeLanes = new List<int>();
            for (int i = 0; i < numberOfLanes; i++) {
                if (laneCooldown[i] <= 0) {
                    freeLanes.Add(i);
                }
            }

            if (freeLanes.Count > 0) {
                int chosenLane = rng.Next(freeLanes.Count);
                int lane = freeLanes[chosenLane];
                laneCooldown[lane] = spawnDelay;
                asteroid.GetComponent<Asteroid>().velocity = speed;
                Instantiate(asteroid, new Vector3(positionX, lanePositions[lane], 0), Quaternion.identity);
            }
        }
    }
}
