using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text enemyScoreText;

    private int playerScore;
    private int enemyScore;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else Destroy(gameObject);


    }

    public void AddPlayerScore(bool hitHuman)
    {
        if (hitHuman)
            playerScore--;
        else
            playerScore++;
        playerScoreText.text = "You: " + playerScore;
    }

    public void AddEnemyScore(bool hitHuman)
    {
        if (hitHuman)
            enemyScore--;
        else
            enemyScore++;
        enemyScoreText.text = "AI: " + enemyScore;
    }
}
