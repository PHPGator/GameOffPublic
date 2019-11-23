using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{

    public float currentHealth = 100.0f;
    public float maxHealth = 100.0f;
    public bool isAlive = true;
    public bool debugging = false;

    private TextMeshProUGUI currentHealthText;
    // Start is called before the first frame update
    void Start()
    {
        //currentHealthText = GameObject.FindWithTag("Debugging - Player Health").GetComponent<TextMeshProUGUI>();
        checkHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public method for decreasing health, may be needed in other scripts
    public void decreaseHealth(float decAmount)
    {
        currentHealth -= decAmount;
        checkHealth();
    }

    // public method for increasing health, may be needed in other scripts
    public void increaseHealth(float incAmount)
    {
        currentHealth += incAmount;
        checkHealth();
    }

    // Check health to make sure it does not go below 0 or above the maximum health.
    public void checkHealth()
    {
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            isAlive = false;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Enemy Health: " + currentHealth);
        // if debugging, go ahead and update UI
        if (debugging)
        {
            currentHealthText.text = "Player Health: " + currentHealth.ToString();
        }
    }
}
