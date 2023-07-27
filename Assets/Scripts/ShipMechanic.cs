using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMechanic : MonoBehaviour
{
    public int spawnPos = 0;
    public Vector2 point;
	public float speed = 5f;
	public float rotateSpeed = 200f;

    private Rigidbody2D rb;
    private Vector2 screenBounds;

    Difficulty difficulty;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb = GetComponent<Rigidbody2D>();
        difficulty = GameObject.FindGameObjectWithTag("Manager").GetComponent<Difficulty>();
        GetDestination();
    }

    void Update()
    {
        FlyToDestiny();
        ReachDesty();
    }

    void GetDestination () {
        switch(spawnPos){
            case 1:
                point = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - 3);
            break;
            case 2:
                point = new Vector2(screenBounds.x + 3, Random.Range(-screenBounds.y, screenBounds.y));
            break;
            case 3:
                point = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 3);
            break;
            case 4: 
                point = new Vector2(-screenBounds.x - 3, Random.Range(-screenBounds.y, screenBounds.y));
            break;
        }
    }

    void ReachDesty(){
        if(spawnPos == 1 && transform.position.y < -screenBounds.y - 1) {
            Destroy(this.gameObject);
            difficulty.ShipMissed();
        } else if (spawnPos == 3 && transform.position.y > screenBounds.y + 1) {
            Destroy(this.gameObject);
            difficulty.ShipMissed();
        } else if (spawnPos == 2 && transform.position.x > screenBounds.x + 1) {
            Destroy(this.gameObject);
            difficulty.ShipMissed();
        } else if (spawnPos == 4 && transform.position.x < -screenBounds.x - 1) {
            Destroy(this.gameObject);
            difficulty.ShipMissed();
        }
    }

    void FlyToDestiny () {
        Vector2 direction = point - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
    }
}
