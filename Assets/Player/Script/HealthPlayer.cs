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
        PlayerManger.Health = maxHealth;

        PlayerManger.shield = PlayerManger.maxShield;
    }

    public void Damage(int damageAmont)
    {
        if(PlayerManger.shield >= 1)
        {
            PlayerManger.shield -= damageAmont;
        }
        else if (PlayerManger.shield <= 0)
        {
            PlayerManger.Health -= damageAmont;
            if (PlayerManger.Health <= 0)
            {
                PlayerManger.Health = 0;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackOne")
        {
            if (PlayerManger.shield >= 1)
            {
                Damage(1);
            }
            else if (PlayerManger.shield <= 0)
            {
                Damage(Random.Range(5, 7));
            }
        }
    }
}
