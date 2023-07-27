using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject obj;
    public Vector2 timeInt = new Vector2(1f, 3f);
    public Vector2 rocketAm = new Vector2(1f, 2f);
    int amount;
    float time;

    void Start()
    {
        amount = Random.Range((int)rocketAm.x, (int)rocketAm.y);
        time = Random.Range(timeInt.x, timeInt.y);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn () {
        for(int i = 0; i < amount; i++) {
            yield return new WaitForSeconds(time);
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }
}
