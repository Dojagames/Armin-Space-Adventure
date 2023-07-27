using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private GameObject playerRef;
    public float hpBarSmoothness = 7.5f;
    public float belowBarDelay = 0.75f;
    public Image hpBar, belowBar;

    [Header("Cursor")]
    public Texture2D cursorarrow;

    [Space]
    public int targetFPS = 144;

    private GameObject reBtn;
    private float belowBarValue = 1f;
    private float tempAmount = 0f;
    private float spawnTime;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        Cursor.SetCursor(cursorarrow, new Vector2(cursorarrow.width / 2 , cursorarrow.height / 2), CursorMode.ForceSoftware);
        reBtn =  GameObject.FindGameObjectWithTag("ReBtn");
        reBtn.transform.GetChild(0).gameObject.SetActive(false);
        playerRef = Instantiate(player, transform.position, transform.rotation);
        spawnTime = GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime;
        scorescript.ScoreVal = 0;
    }

    void Update()
    {
        if(Application.targetFrameRate != targetFPS) Application.targetFrameRate = targetFPS;

        //Hpbar
        if(playerRef != null) {
            hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerRef.GetComponent<Player>().currentHp/playerRef.GetComponent<Player>().maxHp, hpBarSmoothness * Time.deltaTime);
        } else {
            hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, 0f, hpBarSmoothness * Time.deltaTime);
        }
        belowBar.fillAmount = Mathf.Lerp(belowBar.fillAmount, belowBarValue, hpBarSmoothness * Time.deltaTime);

        //escape
        if(Input.GetButtonDown("Cancel")) {
            GameObject.FindGameObjectWithTag("ExBtn").transform.GetChild(0).gameObject.SetActive(!GameObject.FindGameObjectWithTag("ExBtn").transform.GetChild(0).gameObject.activeSelf);
        }
    }

    public void UpdateBelowBar (float amount) {
        tempAmount = amount;
        Invoke("RealUpdate", belowBarDelay);
    }

    void RealUpdate () {
        belowBarValue = tempAmount;
    }

    public void RespawnBtn () {
        belowBarValue = 1f;
        belowBar.fillAmount = 1f;
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (GameObject meteor in meteors) {
            Destroy(meteor);
        }
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        foreach (GameObject ship in ships) {
            Destroy(ship);
        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items) {
            Destroy(item);
        }
        GameObject.Find("Spawner").GetComponent<Spawner>().spawnTime = spawnTime;
        playerRef = Instantiate(player, transform.position, transform.rotation);
        reBtn.transform.GetChild(0).gameObject.SetActive(false);

    }

    public void ExitBtn () {
        Application.Quit();
    }

    public void BaToSt () {
        StartCoroutine(BackToStart());
    }

    IEnumerator BackToStart() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Start");
    }
}
