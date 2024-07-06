using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManger : MonoBehaviour
{
    public static PlayerManger playerManger;

    [Header("Stamina")]
    [SerializeField] private float timerStamina;

    public GameObject staminaGOj;
    Image staminaBar;
    GameObject staminaImage;

    GameObject animatorStaminaGOJ;
    public Animator animatorStamina;

    [Header("Mana")]
    public float Mana = 100;
    public float maxMana = 100;

    [Header("Level")]
    // Bar
    Image levelBar;
    GameObject levelImage;

    // Text
    TMP_Text levelText;
    GameObject levelTextTMP_Text;

    [Header("Health Player")]
    Image HealthBar;
    GameObject healthBarImage;

    [Header("Shield")]
    Image ShieldBar;
    GameObject ShieldBarImage;




    // Start is called before the first frame update
    void Start()
    {
        playerManger = this;

        // Animator Stamina
        animatorStaminaGOJ = GameObject.FindGameObjectWithTag("AnimatorStamina");
        animatorStamina = animatorStaminaGOJ.GetComponent<Animator>();

        // Find Stamina
        staminaImage = GameObject.FindGameObjectWithTag("StaminaBar");
        staminaBar = staminaImage.GetComponent<Image>();

        // Find Health
        healthBarImage = GameObject.FindGameObjectWithTag("HealthBar");
        HealthBar = healthBarImage.GetComponent<Image>();

        // Find Shield
        ShieldBarImage = GameObject.FindGameObjectWithTag("ShieldBar");
        ShieldBar = ShieldBarImage.GetComponent<Image>();

        // Find Level
        levelImage = GameObject.FindGameObjectWithTag("LevelBar");
        levelBar = levelImage.GetComponent<Image>();

        // Find level Text TMP_Text
        levelTextTMP_Text = GameObject.FindGameObjectWithTag("levelTextTMP_Text");
        levelText = levelTextTMP_Text.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        LevelSystem();
        Level();

        
        // Bar Stamina
        staminaBar.fillAmount = GameManager.gameManager.stamina / GameManager.gameManager.maxStamina;

        // Bar Health
        HealthBar.fillAmount = GameManager.gameManager.Health / GameManager.gameManager.maxHealth;

        // Bar Shield
        ShieldBar.fillAmount = GameManager.gameManager.shield / GameManager.gameManager.maxShield;

        // Bar Level
        levelText.text = GameManager.gameManager.levelPlayer.ToString();
        levelBar.fillAmount = GameManager.gameManager.PlayerXP / GameManager.gameManager.maxPlayerXP;
    }

    public void GameManagerOutput()
    {

    }

    public void Stamina()
    {
        timerStamina += Time.deltaTime;
        if(timerStamina >= 0.2f)
        {
            timerStamina = 0;

            GameManager.gameManager.stamina -= 2;
        }
    }

    public void LevelSystem()
    {
        if(GameManager.gameManager.PlayerXP >= 500 && GameManager.gameManager.levelPlayer == 0)
        {
            GameManager.gameManager.PlayerXP = 0;
            GameManager.gameManager.levelPlayer = 2;
        }
        else if (GameManager.gameManager.PlayerXP >= 1000 && GameManager.gameManager.levelPlayer == 1)
        {
            GameManager.gameManager.PlayerXP = 0;
            GameManager.gameManager.levelPlayer = 2;
        }
        else if(GameManager.gameManager.PlayerXP >= 2500 && GameManager.gameManager.levelPlayer == 2)
        {
            GameManager.gameManager.PlayerXP = 0;
            GameManager.gameManager.levelPlayer = 3;
        }
    }

    void Level()
    {
        if(GameManager.gameManager.levelPlayer == 0)
        {
            GameManager.gameManager.maxPlayerXP = 500;
        }
        else if(GameManager.gameManager.levelPlayer == 1)
        {
            GameManager.gameManager.maxPlayerXP = 1500;
        }
        else if(GameManager.gameManager.levelPlayer == 2)
        {
            GameManager.gameManager.maxPlayerXP = 2500;
        }
        else if(GameManager.gameManager.levelPlayer == 3)
        {
            GameManager.gameManager.maxPlayerXP = 3500;
        }
    }
}
