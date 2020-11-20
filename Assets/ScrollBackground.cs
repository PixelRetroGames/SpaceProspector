using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    private SpriteRenderer spriteRenderer;
    private float w;
    private float screenWidth;

    void Start () {
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        w = spriteRenderer.bounds.extents.x;
        print(w);
        //w = Camera.main.ScreenToWorldPoint(new Vector3(w, 0, 0)).x;
        transform.position = new Vector3(w / 2, transform.position.y, 0);
    }

    void Update () {
        transform.position = new Vector3(transform.position.x - scrollSpeed * Time.deltaTime, transform.position.y, 0);
        //w /= 2;
        //print(w);
        //print(transform.position.x);
        if (transform.position.x <= -w / 2) {
            transform.position = new Vector3(w / 2, transform.position.y, 0);
        }

    }
}
