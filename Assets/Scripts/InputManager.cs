using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public delegate void StartTouch(Vector2 position, float time);

    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);

    public event StartTouch OnEndTouch;

    public event Action<Vector2> OnMovePerformed;

    private PlayerMovement _playerMovement;
    private Camera _mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _playerMovement = new PlayerMovement();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerMovement.Enable();
    }

    private void OnDisable()
    {
        _playerMovement.Disable();
    }

    private void Start()
    {
        _playerMovement.Main.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerMovement.Main.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        _playerMovement.Main.Movement.performed += ctx => OnMove(ctx);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(ScreenToWorld(_mainCamera, _playerMovement.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(ScreenToWorld(_mainCamera, _playerMovement.Main.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(_mainCamera, _playerMovement.Main.PrimaryPosition.ReadValue<Vector2>());
    }

    private Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}