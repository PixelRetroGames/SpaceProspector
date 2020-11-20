using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;

    private Texture texture;
    private Vector2 savedOffset;

    void Start () {
    }

    void Update () {
        transform.position = new Vector3(transform.position.x - 0.005f, transform.position.y, 0);

        if (transform.position.x <= -3) {
            transform.position = new Vector3(3, transform.position.y, 0);
        }

    }
}
