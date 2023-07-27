using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5.0f;
    public bool playerReady = true;
    public GameObject[] Ships;

    private int rand_site, rand_pos;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Spawnwave());
    }

    private void spawnShips() {
        GameObject a = Instantiate(Ships[Random.Range(0, Ships.Length)]);

        rand_pos = Random.Range(1, 4);
        switch(rand_pos){
            case 1:
                a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 1);
            break;
            case 2:
                a.transform.position = new Vector2(-screenBounds.x - 1, Random.Range(-screenBounds.y, screenBounds.y));
            break;
            case 3:
                a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - 1);
            break;
            case 4: 
                a.transform.position = new Vector2(screenBounds.x + 1, Random.Range(-screenBounds.y, screenBounds.y));
            break;
        }
        a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y, 10f);
        a.transform.GetComponent<ShipMechanic>().spawnPos = rand_pos;
    }

    IEnumerator Spawnwave() {
        while(playerReady){
            yield return new WaitForSeconds(spawnTime);
            spawnShips();
        }
    }
}
