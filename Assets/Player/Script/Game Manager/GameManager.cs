using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject Player;

    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;

        Instantiate(Player, playerTransform.transform.position, playerTransform.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
