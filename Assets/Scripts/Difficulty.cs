using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public int missedShipsAm = 0;
    public int adjustmentValue = 3;
    public float timeAdjustment = 0.1f;
    public float diffOverTime = 2f;
    public float diffOverTimeValue = 0.1f;

    void Start () {
        InvokeRepeating("DifficultyOverTime", 0f, diffOverTime);
    }

    public void ShipMissed () {
        if(missedShipsAm < adjustmentValue) {
            GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime -= GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime * timeAdjustment;
            missedShipsAm++;
        } else {
            missedShipsAm = 0;
        }
    }
    public void DifficultyOverTime () {
        GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime -= GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime * diffOverTimeValue;
    }
}
