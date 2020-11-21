using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float velocity;
    // Start is called before the first frame update
    void OnEnable() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 screenRightBorders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 screenLeftBorders = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        transform.position += new Vector3(velocity, 0, 0) * Time.deltaTime;
        if (transform.position.x >= screenRightBorders.x || transform.position.x <= screenLeftBorders.x) {
            Destroy(this.gameObject);
        }
    }

    void  OnCollisionEnter2D() {
        Destroy(this.gameObject);
    }
}
