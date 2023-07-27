using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg = 10f;
    public float lifeTime = 5f;
    public float force = 0f;
    public GameObject vfx;

    void Start () {
        Destroy(this.gameObject, lifeTime);
    }

    void Update () {
        if(GameObject.FindGameObjectWithTag("Player") != null && GetComponent<Rigidbody2D>().velocity.magnitude < force) {
            Destroy(this.gameObject);
        }
        Physics2D.IgnoreLayerCollision(11, 10, true);
        Physics2D.IgnoreLayerCollision(10, 10, true);
    }

    void OnCollisionEnter2D (Collision2D n) {
        if(n.transform.tag == "Ignore" || n.transform.tag == "Player") {
            Physics2D.IgnoreCollision(n.transform.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            return;
        }
        if(n.transform.tag == "Meteor") {
            n.transform.GetComponent<Meteor>().Damage(dmg);
        }
        if(n.transform.tag == "Ship") {
            n.transform.GetComponent<Shipp>().Damage(dmg, true);
        }
        GameObject temp = Instantiate(vfx, transform.position, transform.rotation);
        Destroy(temp, 2f);
        Destroy(this.gameObject);
    }
}
