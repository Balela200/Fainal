using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class HealthPlayer : MonoBehaviour
{
    public static HealthPlayer healthPlayer;
    [Header("Health System")]
    public float Health = 100;
    public float maxHealth = 100;
    Animator anim;

    [Header("Shield System")]
    public float shield = 4;
    public float maxShield = 4;

    private Camera cam;

    void Start()
    {
        healthPlayer = this;

        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public HealthPlayer()
    {
        GameManager.gameManager.Health = GameManager.gameManager.maxHealth;

        GameManager.gameManager.shield = GameManager.gameManager.maxShield;
    }

    public void Damage(int damageAmont)
    {
        if(GameManager.gameManager.shield >= 1)
        {
            GameManager.gameManager.shield -= damageAmont;
        }
        else if (GameManager.gameManager.shield <= 0)
        {
            GameManager.gameManager.Health -= damageAmont;
            if (GameManager.gameManager.Health <= 0)
            {
                GameManager.gameManager.Health = 0;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackOne")
        {
            if (GameManager.gameManager.shield >= 1)
            {
                Damage(1);
            }
            else if (GameManager.gameManager.shield <= 0)
            {
                Damage(Random.Range(5, 7));
            }
        }
    }
}
