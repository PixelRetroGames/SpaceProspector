using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int numberOfLanes;
    public float[] lanePositions;
    public int maxHp;
    public int hp;
    private int score;

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
        backgroundMusic.Play();
        dead = false;
        score = 0;
        UpdateScoreText();
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

    public void AddScore(int val) {
        score += val;
        UpdateScoreText();
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
}
