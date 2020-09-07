using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public int score;
    private PlayerController playerController;
    public int comboCount = 1;
    public bool comboFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
