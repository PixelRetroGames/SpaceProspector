using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int numberOfLanes;
    public float[] lanePositions;
    public int maxHp;
    public int hp;
    private int score;
    private GameObject scoreObj;
    private GameObject hpObj;
    // Start is called before the first frame update
    void Start() {
        scoreObj = GameObject.FindGameObjectWithTag("Score");
        hpObj = GameObject.FindGameObjectWithTag("Hp");
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
        if (hp == 0) {
            print("You lost!");
            Application.Quit();
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
}
