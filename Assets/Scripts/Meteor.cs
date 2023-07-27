using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    public float hp = 60f;
    private float currentHp = 0f;

    public float spawnProtection = 1.15f;

    public int pointsAmount = 100;

    public Vector3 offset = new Vector3(10f, 10f, 0f);
    public GameObject pointsTxt;
    private Transform canvas;

    public GameObject[] GFX;
    public GameObject explosion;

    private Collider2D c2D;
    private Transform playerInTrigger;

    void Start() {
        currentHp = hp;
        c2D = GetComponent<Collider2D>();
        StartCoroutine(SpawnProtection());
        canvas = GameObject.FindGameObjectWithTag("GUI").transform;
    }


    public void Damage(float amount) {
        if((currentHp - amount) > 0f) {
            currentHp -= amount;
            if (currentHp < ((hp/100) * 25)) {
                HideAll();
                GFX[0].SetActive(true);
            } else if (currentHp < ((hp/100) * 50)) {
                HideAll();
                GFX[1].SetActive(true);
            } else if (currentHp < ((hp/100) * 75)) {
                HideAll();
                GFX[2].SetActive(true);
            }
        } else {
            currentHp = 0;
            scorescript.ScoreVal += pointsAmount;
            GameObject n = Instantiate(pointsTxt, Camera.main.WorldToScreenPoint(transform.position) + offset, Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f)), canvas);
            n.GetComponent<Text>().text = "+" + pointsAmount;
            GameObject temp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(n, 5f);
            Destroy(temp, 2f);
            Destroy(this.gameObject);

        }
    }

    void HideAll () {
        for(int i = 0; i < GFX.Length; i++) {
            GFX[i].SetActive(false);
        }
    }

    IEnumerator SpawnProtection() {
        yield return new WaitForSeconds(spawnProtection);
        KiPlayerUnMeteor();
    }

    void KiPlayerUnMeteor () {
        if(playerInTrigger != null) {
            playerInTrigger.GetComponent<Player>().TakeDmg(playerInTrigger.GetComponent<Player>().maxHp);
        }
        c2D.isTrigger = false;
    }

    void OnTriggerEnter2D (Collider2D n) {
        if(n.transform.tag != "Player") return;
        playerInTrigger = n.transform;
    }

    void OnTriggerExit2D (Collider2D n) {
        if(n.transform.tag != "Player") return;
        playerInTrigger = null;
    }
}
