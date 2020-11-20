using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void MoveUp() {
        player.MoveUp();
    }

    public void MoveDown() {
        player.MoveDown();
    }
}
