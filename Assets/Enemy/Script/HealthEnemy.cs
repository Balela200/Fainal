using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public static HealthEnemy healthEnemy;
    [Header("Health System")]
    public float enemyHealth = 100;
    public float maxEnemyHealth = 100;

    Animator anim;

    [Header("Health Bar")]
    //[SerializeField] private Image sliderHealthBat;
    //[SerializeField] private GameObject HealthBar;
    //[SerializeField] private float reduceSpeed = 10;
    //private float target = 5;

    private Camera cam;

    void Start()
    {
        healthEnemy = this;

        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public HealthEnemy()
    {
        enemyHealth = maxEnemyHealth;
    }

    public void Damage(int damageAmont)
    {
        enemyHealth -= damageAmont;
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject, 0.5f);

            GameManager.gameManager.PlayerXP += Random.Range(10, 11);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Damage(Random.Range(10, 30));
        }
    }
}
