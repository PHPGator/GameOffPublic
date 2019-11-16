using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100.0f;
    public float playerMaxHealth = 100.0f;
    public bool isAlive = true;
    public bool debugging = true;

    private TextMeshProUGUI playerHealthText;

    void Start()
    {
        //playerHealthText = GameObject.FindWithTag("Debugging - Player Health").GetComponent<TextMeshProUGUI>();
        checkHealth();
        //decreaseHealth(200.0f);
    }

    // public method for decreasing health, may be needed in other scripts
    public void decreaseHealth(float decAmount)
    {
        playerHealth -= decAmount;
        checkHealth();
    }

    // public method for increasing health, may be needed in other scripts
    public void increaseHealth(float incAmount)
    {
        playerHealth += incAmount;
        checkHealth();
    }


    // Check health to make sure it does not go below 0 or above the maximum health.
    public void checkHealth()
    {
        if (playerHealth <= 0.0f)
        {
            playerHealth = 0.0f;
            isAlive = false;
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        // if debugging, go ahead and update UI
        if (debugging)
        {
            //playerHealthText.text = "Player Health: " + playerHealth.ToString();
        }
    }
}
