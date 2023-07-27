using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public bool weaponPowerupActive = false;
    private Player pl;

    void Start () {
        pl = GetComponent<Player>();
    }

    public void Apply (string item, float value) {
        //if(weaponPowerupActive) return;
        if(item == "Shotgun") {
            StartCoroutine(Shotgun(value));
        } else if (item == "Sniper") {
            StartCoroutine(Sniper(value));
        } else if (item == "MG") {
            StartCoroutine(MG(value));
        } else if (item == "BackFire") {
            StartCoroutine(BackFire(value));
        } else if (item == "HP") {
            if((pl.currentHp + value) <= pl.maxHp) {
                pl.currentHp += value;
            } else {
                pl.currentHp = pl.maxHp;
            }
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Game>().UpdateBelowBar(pl.currentHp/pl.maxHp);
        }
    }

    IEnumerator Shotgun (float duration) {
        weaponPowerupActive = true;
        pl.bulletDamage = 15f;
        pl.bulletForce = new Vector2(18f, 32f);
        pl.bulletAmount = 7f;
        pl.fireRate = 0.5f;
        pl.fireRepeat = 2f;
        pl.fireReDelay = 0.03f;
        yield return new WaitForSeconds(duration);
        DefaultSettings();
        weaponPowerupActive = false;
    }

    IEnumerator Sniper (float duration) {
        weaponPowerupActive = true;
        pl.bulletDamage = 50f;
        pl.bulletForce = new Vector2(70f, 70f);
        pl.bulletAmount = 1f;
        pl.fireRate = 0.5f;
        pl.fireRepeat = 1f;
        pl.fireReDelay = 0f;
        yield return new WaitForSeconds(duration);
        DefaultSettings();
        weaponPowerupActive = false;
    }

    IEnumerator MG (float duration) {
        weaponPowerupActive = true;
        pl.bulletDamage = 5f;
        pl.bulletForce = new Vector2(30f, 40f);
        pl.fireRate = 0.0035f;
        pl.spreadAmount = 3f;
        yield return new WaitForSeconds(duration);
        DefaultSettings();
        weaponPowerupActive = false;
    }

    IEnumerator BackFire (float duration) {
        weaponPowerupActive = true;
        pl.bulletDamage = 5f;
        pl.bulletForce = new Vector2(35f, 35f);
        pl.spreadAmount = 3f;
        pl.backFire = true;
        yield return new WaitForSeconds(duration);
        DefaultSettings();
        weaponPowerupActive = false;
    }

    void DefaultSettings () {
        pl.bulletDamage = 10f;
        pl.bulletForce = new Vector2(35f, 35f);
        pl.bulletAmount = 1f;
        pl.fireRate = 0.06944445f;
        pl.fireRepeat = 1f;
        pl.fireReDelay = 0.1f;
        pl.spreadAmount = 0f;
        pl.backFire = false;
    }
}
