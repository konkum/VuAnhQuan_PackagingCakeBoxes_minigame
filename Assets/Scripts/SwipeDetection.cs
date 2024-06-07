using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = 0.2f;
    [SerializeField] private float maximumTime = 0.5f;
    [SerializeField, Range(0, 1)] private float directionThreshold = 0.9f;

    private InputManager _inputManager;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= minimumDistance && (_endTime - _startTime) <= maximumTime)
        {
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
            Debug.Log(Direction);
            GameManager.Instance.OnMovedPerformed(Direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Direction = Vector2.up;
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Direction = Vector2.down;
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Direction = Vector2.left;
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Direction = Vector2.right;
        }
    }
}