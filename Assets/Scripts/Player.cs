using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private static float GRIDSIZE = 1.94f;
    [SerializeField] private Tilemap groundTile;
    [SerializeField] private Tilemap wallTile;
    [SerializeField] private float moveSpeed = 10;
    public Vector3 PreviousPosition { get; private set; }

    private bool _isMoving = false;
    public Vector3 TargetPosition = Vector3.zero;

    public bool IsMoving => _isMoving;

    private void Start()
    {
        PreviousPosition = transform.position;
    }

    private void Update()
    {
        if (_isMoving)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, TargetPosition) < 0.1f)
            {
                transform.position = TargetPosition;
                _isMoving = false;
            }
        }
    }

    public IEnumerator Move(Vector2 direction)
    {
        Vector3 moveDirection = direction * GRIDSIZE;
        while (CanMove(moveDirection))
        {
            yield return null;
            moveDirection += (Vector3)direction * GRIDSIZE;
        }
        moveDirection -= (Vector3)direction * GRIDSIZE;
        PreviousPosition = this.transform.position;
        TargetPosition = this.transform.position + moveDirection;
        _isMoving = true;

        yield return new WaitUntil(() => !_isMoving);
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPosition = groundTile.WorldToCell(this.transform.position + direction);

        if (!groundTile.HasTile(gridPosition) || wallTile.HasTile(gridPosition))
        {
            return false;
        }

        return true;
    }

    public void MoveBackWard(Vector3 direction)
    {
        this.transform.position -= direction * GRIDSIZE;
    }
}