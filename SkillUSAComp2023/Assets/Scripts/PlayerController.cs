using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseDamage = 8;
    [SerializeField] private float trueDamage;
    [SerializeField] private float baseHealth = 150;
    public float trueHealth;
    public float currentHealth = 150;
    private float numOfDinoMeat;
    private float numOfRosary;
    private float numOfVinyl;
    private float numOfBigMax;
    [SerializeField] private GameObject inventoryHUD;

    [SerializeField] private Scrollbar healthSlide;
    // Start is called before the first frame update
    void Start()
    {
        inventoryHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryHUD.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryHUD.SetActive(false);
        }
        baseDamage = baseDamage + 0.0005f;
        baseHealth = baseHealth + 0.0005f;
        trueHealth = baseHealth + (numOfDinoMeat * 20f) + (numOfRosary * 30f) + (numOfBigMax * 40f);
        trueDamage = baseDamage + (numOfVinyl * (.5f * baseDamage));
        healthSlide.size = currentHealth / trueHealth;
    }
}

