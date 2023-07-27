using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployit : MonoBehaviour
{
    public float respawntime = 5.0f;
    public bool playerReady = true;
    public bool devMode = false;
    public GameObject[] Meteor;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Spawnwave());
    }

    private void spawnMets() {
        GameObject a = Instantiate(Meteor[Random.Range(0, Meteor.Length)]);
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        a.transform.Rotate(0, 0, Random.Range(0, 360));
        if(devMode) a.transform.position = new Vector2(0.5f, 6f);
    }

    IEnumerator Spawnwave() {
        while(playerReady){
            yield return new WaitForSeconds(respawntime);
            spawnMets();
        }
    }

    void Update()
    {
        
    }
}
