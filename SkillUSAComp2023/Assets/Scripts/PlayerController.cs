using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseDamage = 8;
    [SerializeField] private float trueDamage;
    [SerializeField] private float baseHealth = 150;
    [SerializeField] private float trueHealth;
    private float numOfDinoMeat;
    private float numOfRosary;
    private float numOfVinyl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        baseDamage = baseDamage + 0.0005f;
        baseHealth = baseHealth + (0.0005f * 30f);
        trueHealth = baseHealth + (numOfDinoMeat * 20f) + (numOfRosary * 30f);
        trueDamage = baseDamage + (numOfVinyl * (.5f * baseDamage));
    }
}
