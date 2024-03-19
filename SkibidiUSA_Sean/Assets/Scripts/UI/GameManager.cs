using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float playerBaseDamage = 8;
    [SerializeField] private float playerTrueDamage;
    [SerializeField] private float playerBaseHealth = 150;
    public float playerTrueHealth;
    public float playerCurrentHealth = 150;
    private float numOfDinoMeat;
    private float numOfRosary;
    private float numOfVinyl;
    private float numOfBigMax;
    [SerializeField] private GameObject inventoryHUD;

    public bool playerDead;

    [SerializeField] private Scrollbar healthSlide;

    [SerializeField] private Text scoreText;
    [SerializeField] private float score;
    // Start is called before the first frame update
    void Start()
    {
        inventoryHUD.SetActive(false);
        playerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score " + score;
        if (playerCurrentHealth < 0)
        {
            playerDead = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryHUD.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryHUD.SetActive(false);
        }

        playerBaseDamage = playerBaseDamage + 0.0005f;
        playerBaseHealth = playerBaseHealth + 0.0005f;
        playerTrueHealth = playerBaseHealth + (numOfDinoMeat * 20f) + (numOfRosary * 30f) + (numOfBigMax * 40f);
        playerTrueDamage = playerBaseDamage + (numOfVinyl * (.5f * playerBaseDamage));
        healthSlide.size = playerCurrentHealth / playerTrueHealth;
    }

}

