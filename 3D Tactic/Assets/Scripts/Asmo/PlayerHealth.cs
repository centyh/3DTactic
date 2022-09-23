using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public static float health;

    void Start()
    {

        healthBar = GetComponent<Image>();
        health = maxHealth;
    }


    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        currentHealth = health;
    }
}
