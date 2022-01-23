using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int healthCount = 3;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI loseGame;
    private float score;
    private bool isGameOver;
    private PlayerController playerController;
    
    public UnityAction gameover;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.wasHitted += OnHit;
    }

    private void OnDisable()
    {
        playerController.wasHitted -= OnHit;
    }

    private void OnHit()
    {
        PlayerStats.Instance.TakeDamage(1);
        healthCount -= 1;
        if (healthCount < 1)
            GameOver();
    }

    private void GameOver()
    {
        gameover.Invoke();
        isGameOver = true;
        scoreText.text = "Score: " + Mathf.RoundToInt(score * 25);
        loseGame.gameObject.SetActive(true);
        loseGame.text = "Game over!";
        playerController.enabled = false;
        PlayerAnimController.Die();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayLevel()
    {
        Time.timeScale = 1;
    }

    public void PauseLevel()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Restart");
            RestartLevel();
        }

        if (!isGameOver)
        {
            score += Time.deltaTime;
            scoreText.text = Mathf.RoundToInt(score * 25).ToString();
        }
    }
}