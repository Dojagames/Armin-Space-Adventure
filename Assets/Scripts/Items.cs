using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public string Item = "";
    public float value = 0f;
    public bool hpPack = false;
    
    void OnTriggerEnter2D (Collider2D n) {
        if(n.transform.tag != "Player") return;
        if(n.transform.tag == "Player" && n.transform.GetComponent<Powerups>().weaponPowerupActive && !hpPack) return;
        n.transform.GetComponent<Powerups>().Apply(Item, value);
        Destroy(this.gameObject);
    }
}
