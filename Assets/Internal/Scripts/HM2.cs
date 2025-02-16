using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HM2 : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public bool isDead = false;
    public GameObject gameOverScreen;
    public GameObject selectedGameOverButton;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDied();
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;

        if (currentHealth < maxHealth) 
        {
            currentHealth = maxHealth;
        }

    }

    public void PlayerDied()
    {
        if(currentHealth <= 0)
        {
            EventSystem.current.SetSelectedGameObject(selectedGameOverButton);
            isDead = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            

        }
    }
}
