using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;

public class playerMove : MonoBehaviour
{
    [Header("타일맵")]
    [SerializeField] private Tilemap moveableTile;
    [SerializeField] private Tilemap wallTilemap;

    [Header("이벤트 시스템")]
    [SerializeField] private eventManager eventSystem;

    [Header("이동 설정")]
    [SerializeField] private float duration = 0.15f;
    [SerializeField] public int moveCount = 0;
    private Vector3Int currentCell;
    private bool isMoving;
    

    private void Awake()
    {
        currentCell = moveableTile.WorldToCell(transform.position);
        transform.position = moveableTile.GetCellCenterWorld(currentCell);
    }

    public void OnMove(InputValue inputValue)
    {
        if (isMoving)
            return;

        Vector2 input = inputValue.Get<Vector2>();

        if (input == Vector2.zero)
            return;

        Vector3Int direction = GetDirection(input);

        if (direction != Vector3Int.zero)
        {
            TryMove(direction);
        }
    }
    public void OnInteract(InputValue inputValue)
    {
        // 이동 중일 때는 상호작용 불가
        if (isMoving) return;

        if (inputValue.isPressed)
        {
            if (eventSystem != null)
            {
                eventSystem.StartEvent(currentCell);
            }
        }
    }


    

    private Vector3Int GetDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return input.x > 0 ? Vector3Int.right : Vector3Int.left;
        }
        else
        {
            return input.y > 0 ? Vector3Int.up : Vector3Int.down;
        }
    }

    private void TryMove(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;

        // 벽 타일맵에 타일이 있으면 이동하지 않음
        if (wallTilemap != null && wallTilemap.HasTile(targetCell))
            return;

            StartCoroutine(MoveToCell(targetCell));
    }

    private IEnumerator MoveToCell(Vector3Int targetCell)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = moveableTile.GetCellCenterWorld(targetCell);

        float moveTime = 0f;

        while (moveTime < duration)
        {
            moveTime += Time.deltaTime;

            float t = moveTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // 마지막 위치를 타일 중앙에 정확히 고정
        transform.position = targetPosition;
        currentCell = targetCell;

        moveCount++;
        isMoving = false;
    }
  
}
