using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    [Range(0.1f, 10)] [SerializeField] public float _gameSpeed = 1f;
    [SerializeField] public int scorePerBlockDestroyed = 50;

    //state variables
    [SerializeField] public int currentScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] private bool isAutoPlayEnabled;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameController>().Length;
        if (gameStatusCount > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void AddToScore(int scoreMult)
    {
        currentScore += scorePerBlockDestroyed * scoreMult;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(this.gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

}
