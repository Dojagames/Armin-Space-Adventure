using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if(transform.position.x < -screenBounds.x) {
            transform.position = new Vector2(screenBounds.x, transform.position.y);
        } else if (transform.position.x > screenBounds.x) {
            transform.position = new Vector2(-screenBounds.x, transform.position.y);
        } else if (transform.position.y < -screenBounds.y) {
            transform.position = new Vector2(transform.position.x, screenBounds.y);
        } else if (transform.position.y > screenBounds.y) {
            transform.position = new Vector2(transform.position.x, -screenBounds.y);
        }
    }
}
