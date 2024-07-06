using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Stamina")]
    public float stamina = 100;
    public float maxStamina = 100;

    [Header("Mana")]
    public float Mana = 100;
    public float maxMana = 100;

    [Header("Level")]
    public int levelPlayer = 0;
    public float PlayerXP;
    [SerializeField] public float maxPlayerXP;

    [Header("Health System")]
    public float Health = 100;
    public float maxHealth = 100;

    [Header("Shield System")]
    public float shield = 4;
    public float maxShield = 4;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;

        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
