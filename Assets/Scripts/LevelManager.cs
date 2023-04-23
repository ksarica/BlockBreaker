using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _breakableBlocks;
    
    private SceneLoader _sceneLoader;
    private GameController _gameController;

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void CountBlocks()
    {
        _breakableBlocks++;
    }

    public void BlockDestroyed(int maxHitsCount)
    {
        _breakableBlocks--;
        _gameController.AddToScore(maxHitsCount);

        if (_breakableBlocks <= 0)
        {
            _sceneLoader.LoadNextScene();
        }
    }

}
