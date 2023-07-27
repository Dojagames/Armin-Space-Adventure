using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public float maxHp = 400f;
    //[HideInInspector]
    public float currentHp;

    [Header("Shooting")]
    public float fireRepeat = 1f;
    public float fireReDelay = 0.1f;
    public float bulletDamage = 10f;
    public Vector2 bulletForce = new Vector2(20f, 20f);
    public float bulletAmount = 1f;
    public float fireRate = 0.2f;
    public float spreadAmount = 0;
    public bool backFire = false;
    public Transform[] bulletSpawnPos;
    public GameObject bulletPref;

    public bool autoshoot = false;

    [Header("Movement")]
    public float moveSpeed = 3f;

    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePos;
    private float currentTime = 0f;
    private GameObject reBtn;


    void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        reBtn = GameObject.FindGameObjectWithTag("ReBtn");
        mainCam = Camera.main;
    }

    void Update()
    {
        if(autoshoot){
            Fire();
        } else {
            if(Input.GetButton("Fire1")) {
                Fire();
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if(currentTime > 0f) {
            currentTime -= Time.deltaTime;
        }
        if(currentTime <= 0f) {
            currentTime = 0f;
        }

        if(Input.GetKeyDown("k")) {
            TakeDmg(50f);
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Fire() {
        if(currentTime != 0) return;
        if(spreadAmount == 0f) {
            StartCoroutine(FireCo());
        } else if(backFire) {
            StartCoroutine(BackFireCo(1f));
            StartCoroutine(BackFireCo(-1f));
        } else {
            StartCoroutine(SpreadFireCo());
        }
        currentTime = fireRate;
    }

    IEnumerator FireCo () {
        for(int n = 0; n < fireRepeat; n++) {
            for(int i = 0; i < bulletAmount; i++) {
                GameObject temp = Instantiate(bulletPref, bulletSpawnPos[i].position, bulletSpawnPos[i].rotation);
                Rigidbody2D rbB = temp.GetComponent<Rigidbody2D>();
                float force_ = Random.Range(bulletForce.x, bulletForce.y);
                rbB.AddForce(bulletSpawnPos[i].up * force_, ForceMode2D.Impulse);
                temp.GetComponent<Bullet>().force = force_;
                temp.GetComponent<Bullet>().dmg = bulletDamage;
            }
            yield return new WaitForSeconds(fireReDelay);
        }
    }

    IEnumerator SpreadFireCo () {
        int randNbr = Random.Range(0, (int)spreadAmount);
        GameObject temp = Instantiate(bulletPref, bulletSpawnPos[randNbr].position, bulletSpawnPos[randNbr].rotation);
        Rigidbody2D rbB = temp.GetComponent<Rigidbody2D>();
        float force_ = Random.Range(bulletForce.x, bulletForce.y);
        rbB.AddForce(bulletSpawnPos[randNbr].up * force_, ForceMode2D.Impulse);
        temp.GetComponent<Bullet>().force = force_;
        temp.GetComponent<Bullet>().dmg = bulletDamage;
        yield return null;
    }

    IEnumerator BackFireCo (float value) {
        int randNbr = Random.Range(0, (int)spreadAmount);
        GameObject temp = Instantiate(bulletPref, bulletSpawnPos[randNbr].position, bulletSpawnPos[randNbr].rotation);
        Rigidbody2D rbB = temp.GetComponent<Rigidbody2D>();
        float force_ = Random.Range(bulletForce.x, bulletForce.y) * value;
        rbB.AddForce(bulletSpawnPos[randNbr].up * force_, ForceMode2D.Impulse);
        temp.GetComponent<Bullet>().force = force_;
        temp.GetComponent<Bullet>().dmg = bulletDamage;
        yield return null;
    }

    public void TakeDmg (float amount) {
        if((currentHp - amount) <= 0f) {
            currentHp = 0;
            Dead();
        } else {
            currentHp -= amount;
        }
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Game>().UpdateBelowBar(currentHp/maxHp);
    }

    void Dead() {
        //reBtn.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Game>().BaToSt();
        //new highscore?
        if(PlayerPrefs.GetInt("highscore") == 0 || PlayerPrefs.GetInt("highscore") < scorescript.ScoreVal) {
            PlayerPrefs.SetInt("highscore", scorescript.ScoreVal);
        }
        Destroy(this.gameObject);
    }
}
