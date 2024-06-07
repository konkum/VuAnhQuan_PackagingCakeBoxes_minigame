using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static float GRIDSIZE = 1.94f;
    [SerializeField] private Player _present;
    [SerializeField] private Player _donut;
    [SerializeField] private int[] moveToStars;

    private int _moves = 0;
    private Vector3 _direction;

    public Action<int> OnWin;
    public Action OnLose;

    public bool IsWin = false;

    private void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void OnDisable()
    {
        OnLose = null;
        OnWin = null;
    }

    private void Start()
    {
        InputManager.Instance.OnMovePerformed += OnMovedPerformed;
    }

    public void OnMovedPerformed(Vector2 direction)
    {
        if (IsWin)
        {
            return;
        }
        if (_donut.IsMoving)
        {
            return;
        }
        if (_present.IsMoving)
        {
            return;
        }
        _direction = direction;
        _moves++;
        StartCoroutine(_present.Move(direction));
        StartCoroutine(_donut.Move(direction));
    }

    private void Update()
    {
        if (IsWin)
        {
            return;
        }
        CheckPosition();
    }

    public void CheckPosition()
    {
        if (_donut.TargetPosition == _present.TargetPosition)
        {
            if (_direction.x < 0)
            {
                if (_donut.PreviousPosition.x < _present.PreviousPosition.x)
                {
                    _present.TargetPosition -= _direction * GRIDSIZE;
                }
                else if (_donut.PreviousPosition.x > _present.PreviousPosition.x)
                {
                    _donut.TargetPosition -= _direction * GRIDSIZE;
                }
            }
            else if (_direction.x > 0)
            {
                if (_donut.PreviousPosition.x < _present.PreviousPosition.x)
                {
                    _donut.TargetPosition -= _direction * GRIDSIZE;
                }
                else if (_donut.PreviousPosition.x > _present.PreviousPosition.x)
                {
                    _present.TargetPosition -= _direction * GRIDSIZE;
                }
            }
            if (_direction.y < 0)
            {
                if (_donut.PreviousPosition.y < _present.PreviousPosition.y)
                {
                    _present.TargetPosition -= _direction * GRIDSIZE;
                }
                else if (_donut.PreviousPosition.y > _present.PreviousPosition.y)
                {
                    StartCoroutine(EndGame());
                    IsWin = true;
                }
            }
            else if (_direction.y > 0)
            {
                if (_donut.PreviousPosition.y < _present.PreviousPosition.y)
                {
                    _donut.TargetPosition -= _direction * GRIDSIZE;
                }
                else if (_donut.PreviousPosition.y > _present.PreviousPosition.y)
                {
                    StartCoroutine(EndGame());
                    IsWin = true;
                }
            }
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.5f);
        _donut.gameObject.SetActive(false);
        OnWin?.Invoke(GetStar());
    }

    private int GetStar()
    {
        if (_moves < moveToStars[0])
        {
            return 3;
        }
        else if (_moves < moveToStars[1])
        {
            return 2;
        }

        return 1;
    }
}