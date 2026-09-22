using UnityEngine;
using UnityEngine.Tilemaps;

public class eventManager : MonoBehaviour
{
    [Header("이벤트 타일맵")]
    [SerializeField] private Tilemap eventTilemap;

    [Header("이벤트 타일")]
    [SerializeField] private TileBase eventTile1;
    [SerializeField] private TileBase eventTile2;
    [SerializeField] private TileBase eventTile3;

    public void StartEvent(Vector3Int cellPosition)
    {
        if (eventTilemap == null)
        {
            return;
        }

        TileBase currentTile = eventTilemap.GetTile(cellPosition);

        if (currentTile == null)
        {
            return;
        }

        if (currentTile == eventTile1)
        {
            Event1(cellPosition);
        }
        else if (currentTile == eventTile2)
        {
            Event2(cellPosition);
        }
        else if (currentTile == eventTile3)
        {
            Event3(cellPosition);
        }
    }

    private void Event1(Vector3Int cellPosition)
    {
        Debug.Log("1번");
    }

    private void Event2(Vector3Int cellPosition)
    {
        Debug.Log("2번");
    }

    private void Event3(Vector3Int cellPosition)
    {
        Debug.Log("3번");
    }
}

