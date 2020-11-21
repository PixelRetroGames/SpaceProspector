using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerAPI playerAPIObj;
    public int numberOfLanes;
    public float[] lanePositions;
    public int maxHp;
    public int hp;
    private int score;
    public GameObject backgroundObj;
    public GameObject asteroidSpawnerObj;
    private bool dead;
    public GameObject scoreObj;
    public GameObject hpObj;
    public GameObject deadObj;
    public GameObject[] deactivateOnDeath;
    public SoundGenerator deathSound;
    public SoundGenerator retrySound;
    public SoundGenerator impactSound;
    public AudioSource backgroundMusic;

    // Start is called before the first frame update
    void OnEnable() {
        asteroidSpawnerObj.GetComponent<AsteroidSpawner>().OnEnable();
        print("Called");
        backgroundMusic.Play();
        dead = false;
        score = 0;
        UpdateScoreText();
        UpdateParameters();
        hp = maxHp;
        UpdateHpText();
    }

    // Update is called once per frame
    void Update() {
        CheckLoseCondition();
    }

    private void CheckLoseCondition() {
        if (hp == 0 && !dead) {
            dead = true;
            playerAPIObj.AddSlotocoins(score);
            deadObj.GetComponentInChildren<Text>().text = "\nGame over!\n\nEarned " + score + " slotocoins\n\n" + "Total slotocoins " + playerAPIObj.GetSlotocoins() + "\n\n\n\n"; 
            deadObj.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
            deadObj.SetActive(true);
            backgroundMusic.Stop();
            deathSound.Play();
            for (int i = 0; i < deactivateOnDeath.Length; i++) {
                deactivateOnDeath[i].SetActive(false);
            }
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage) {
        hp = Mathf.Max(0, hp - damage);
        UpdateHpText();
    }

    private void UpdateHpText() {
        hpObj.GetComponent<Text>().text = "Hp: " + hp;
        // hpObj.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
    }

    public void UpdateParameters() {    
        float oldSpawnChance = asteroidSpawnerObj.GetComponent<AsteroidSpawner>().spawnChance;
        asteroidSpawnerObj.GetComponent<AsteroidSpawner>().spawnChance = Mathf.Min(100.0f , oldSpawnChance + 3 * Mathf.Log((oldSpawnChance + 1) / 50));
        asteroidSpawnerObj.GetComponent<AsteroidSpawner>().speed -= 0.02f;
        backgroundObj.GetComponent<ScrollBackground>().scrollSpeed = -asteroidSpawnerObj.GetComponent<AsteroidSpawner>().speed * 0.67f;
    }
    public void AddScore(int val) {
        val = val * (int) (100 * Mathf.Floor((Mathf.Sqrt(1.0f * score / 100 + 1))));
        score += val;
        UpdateScoreText();
        UpdateParameters();
    }

    private void UpdateScoreText() {
        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");
        scoreObj.GetComponent<Text>().text = "Score: " + score;
        scoreObj.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
    }

    public void ReloadGame() {
        Time.timeScale = 1;
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        // SceneManager.LoadScene("SwitchScene");
        // SceneManager.LoadScene(S
        deadObj.SetActive(false);
        foreach (GameObject obj in deactivateOnDeath) {
            obj.SetActive(true);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Asteroid")) {
            Destroy(obj);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet")) {
            Destroy(obj);
        }

        gameObject.SetActive(false);
        gameObject.SetActive(true);

        retrySound.Play();
    }

    public void Exit() {
        Application.Quit();
    }
}
