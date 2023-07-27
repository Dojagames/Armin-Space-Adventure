using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shipp : MonoBehaviour
{

    public float hp = 40f;
    private float currentHp;
    
    public int pointsAmount = 250;

    private Collider2D c2D;
    private Transform canvas;
    public Vector3 offset = new Vector3(10f, 10f, 0f);
    public GameObject pointsTxt;
    public GameObject explosion;

    void Start()
    {
        currentHp = hp;
        c2D = GetComponent<Collider2D>();
        canvas = GameObject.FindGameObjectWithTag("GUI").transform;
    }

    void OnCollisionEnter2D (Collision2D n) {
        if(n.transform.tag == "Ignore") {
            Physics2D.IgnoreCollision(n.transform.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        } else if (n.transform.tag == "Ship") {
            Damage(hp, false);
        }
    }
    
    void Update () {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 11, true);
    }

    public void Damage(float amount, bool points) 
    {
        if((currentHp - amount) > 0f) {
            currentHp -= amount;
        } else {
            currentHp = 0;
            if(points) {
                scorescript.ScoreVal += pointsAmount;
                GameObject n = Instantiate(pointsTxt, Camera.main.WorldToScreenPoint(transform.position) + offset, Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f)), canvas);
                n.GetComponent<Text>().text = "+" + pointsAmount;
                Destroy(n, 5f);
            }
            GameObject temp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(temp, 3f);
            Destroy(this.gameObject);
        }
    }
}
