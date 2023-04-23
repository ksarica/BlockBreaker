using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // size 6 yaptık bu heightin yarısı => 12 yapar 4:3 oran 3x = 12 ise 4x = 16 yapar.
    [SerializeField] private float _screenSizeInUnits = 16f;
    [SerializeField] private float _minPosInX = 1f;
    [SerializeField] private float _maxPosInX = 15f;


    private GameController _gameController;
    private BallController _ballController;
    private Transform _cachedTransform;
    private Vector2 _paddlePos;
    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _ballController = FindObjectOfType<BallController>();

        _cachedTransform = this.gameObject.transform;
        _paddlePos = new Vector2();
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition.x / Screen.width * _screenSizeInUnits);
        ManagePaddle();
    }

    private void ManagePaddle()
    {
        //_paddlePos.x = _cachedTransform.position.x;
        _paddlePos.y = _cachedTransform.position.y;
        _paddlePos.x = Mathf.Clamp(GetXPos(), _minPosInX, _maxPosInX);
        _cachedTransform.position = _paddlePos;
    }

    private float GetXPos()
    {
        if (_gameController.IsAutoPlayEnabled())
        {
            return _ballController.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * _screenSizeInUnits;
        }
    }
}
