using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [Header("Point System")]
    public GameObject[] pointButton;

    [Header("Stamina")]
    public TMP_Text staminaTMP_Text;

    [Header("Health")]
    public TMP_Text healthTMP_Text;

    [Header("Shield")]
    public TMP_Text shieldTMP_Text;

    [Header("Mana")]
    public TMP_Text manaTMP_Text;

    [Header("Level")]
    public TMP_Text levelTMP_Text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextSkil();
        LevelSkil();
    }

    public void TextSkil()
    {
        // Stamina
        staminaTMP_Text.text = PlayerManger.stamina.ToString() + " / " + PlayerManger.maxStamina.ToString();

        // Health
        healthTMP_Text.text = PlayerManger.Health.ToString() + " / " + PlayerManger.maxHealth.ToString();

        // Shield
        shieldTMP_Text.text = PlayerManger.shield.ToString() + " / " + PlayerManger.maxShield.ToString();

        // Mana
        manaTMP_Text.text = PlayerManger.mana.ToString() + " / " + PlayerManger.maxMana.ToString();

        // Level
        levelTMP_Text.text = "Level " + PlayerManger.levelPlayer.ToString();
    }
    public void LevelSkil()
    {
        if(PlayerManger.playerManger.point >= 1)
        {
            pointButton[0].SetActive(true);
            pointButton[1].SetActive(true);
            pointButton[2].SetActive(true);
            pointButton[3].SetActive(true);
            pointButton[4].SetActive(true);
        }
        else
        {
            pointButton[0].SetActive(false);
            pointButton[1].SetActive(false);
            pointButton[2].SetActive(false);
            pointButton[3].SetActive(false);
            pointButton[4].SetActive(false);
        }
    }

    public void ExitSkil()
    {
        PlayerManger.playerManger.skilUI.SetActive(false);
    }
}
