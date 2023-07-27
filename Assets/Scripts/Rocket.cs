using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour {

	Transform target;
    public GameObject vfx;
    public GameObject txt;

    public float dmg = 10f;
    public Vector2 speedInt = new Vector2(14f, 18f);
    public Vector2 rotSpeedInt = new Vector2(225f, 275f);

    public int pointsAmount = 10;
    private Transform canvas;
    public Vector3 offset = new Vector3(10f, 10f, 0f);

	float speed;
	float rotateSpeed;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        if(GameObject.FindGameObjectWithTag("Player") != null) target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = Random.Range(speedInt.x, speedInt.y);
        rotateSpeed = Random.Range(rotSpeedInt.x, rotSpeedInt.y);
        Physics2D.IgnoreLayerCollision(8, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
        Physics2D.IgnoreLayerCollision(12, 12, true);
        canvas = GameObject.FindGameObjectWithTag("GUI").transform;
	}
	
	void FixedUpdate () {
        if(target == null) {
            Kaaboooom(0);
            return;
        }

		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

	void OnCollisionEnter2D (Collision2D n) 
	{
        if (n.transform.tag == "Ship") {
            return;
        } else if (n.transform.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDmg(dmg);
            Kaaboooom(-pointsAmount);
            return;
        }
        Kaaboooom(pointsAmount);
	}

    void Kaaboooom (int points) {
        if(points != 0) {
            scorescript.ScoreVal += points;
            GameObject m = Instantiate(txt, Camera.main.WorldToScreenPoint(transform.position) + offset, Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f)), canvas);
            if (points > 0) m.GetComponent<Text>().text = "+" + pointsAmount;
            if (points < 0) m.GetComponent<Text>().text = "-" + pointsAmount;
            GameObject temp = Instantiate(vfx, transform.position, transform.rotation);
            Destroy(temp, 3f);
            Destroy(m, 5f);
        }
        Destroy(gameObject);
    }
}
