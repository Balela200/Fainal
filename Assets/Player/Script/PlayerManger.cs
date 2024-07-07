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
    public static float stamina = 100;
    public static float maxStamina = 100;

    public GameObject staminaGOj;
    Image staminaBar;
    GameObject staminaImage;

    GameObject animatorStaminaGOJ;
    public Animator animatorStamina;

    [Header("Mana")]
    public static float Mana = 100;
    public static float maxMana = 100;

    [Header("Level")]
    public static int levelPlayer = 0;
    public static float PlayerXP;
    [SerializeField] public static float maxPlayerXP;

    // Bar
    Image levelBar;
    GameObject levelImage;

    // Text
    TMP_Text levelText;
    GameObject levelTextTMP_Text;

    [Header("Health Player")]
    public static float Health = 100;
    public static float maxHealth = 100;

    // Bar
    Image HealthBar;
    GameObject healthBarImage;

    [Header("Shield")]
    public static float shield = 4;
    public static float maxShield = 4;

    // Bar
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
        staminaBar.fillAmount = stamina / maxStamina;

        // Bar Health
        HealthBar.fillAmount = Health / maxHealth;

        // Bar Shield
        ShieldBar.fillAmount = shield / maxShield;

        // Bar Level
        levelText.text = levelPlayer.ToString();
        levelBar.fillAmount = PlayerXP / maxPlayerXP;
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

            stamina -= 2;
        }
    }

    public void LevelSystem()
    {
        if(PlayerXP >= 500 && levelPlayer == 0)
        {
            PlayerXP = 0;
            levelPlayer = 2;
        }
        else if (PlayerXP >= 1000 && levelPlayer == 1)
        {
            PlayerXP = 0;
            levelPlayer = 2;
        }
        else if(PlayerXP >= 2500 && levelPlayer == 2)
        {
            PlayerXP = 0;
            levelPlayer = 3;
        }
    }

    void Level()
    {
        if(levelPlayer == 0)
        {
            maxPlayerXP = 500;
        }
        else if(levelPlayer == 1)
        {
            maxPlayerXP = 1500;
        }
        else if(levelPlayer == 2)
        {
            maxPlayerXP = 2500;
        }
        else if(levelPlayer == 3)
        {
            maxPlayerXP = 3500;
        }
    }
}
