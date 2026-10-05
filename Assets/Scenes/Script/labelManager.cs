using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class labelManager : MonoBehaviour
{
    [Header("이벤트 타일맵")]
    [SerializeField] private Tilemap eventTilemap;

    [Serializable]
    private class TileLabelInfo
    {
        public TileBase targetTile;
        public string labelText;
    }

    [Header("TMP 라벨 프리팹")]
    [SerializeField] private TextMeshPro labelPrefab;

    [Header("라벨 부모 오브젝트")]
    [SerializeField] private Transform labelParent;

    [Header("타일별 라벨")]
    [SerializeField] private TileLabelInfo[] tileLabels;

    private readonly List<GameObject> createdLabels =
        new List<GameObject>();

    private void Start()
    {
        CreateLabels();
    }

    public void CreateLabels()
    {
        if (eventTilemap == null)
        {
            Debug.LogError("Event Tilemap이 연결되지 않았습니다.");
            return;
        }

        if (labelPrefab == null)
        {
            Debug.LogError("TMP Label Prefab이 연결되지 않았습니다.");
            return;
        }

        if (labelParent == null)
        {
            labelParent = transform;
        }

        ClearLabels();

        foreach (Vector3Int cellPosition
                 in eventTilemap.cellBounds.allPositionsWithin)
        {
            TileBase currentTile =
                eventTilemap.GetTile(cellPosition);

            if (currentTile == null)
                continue;

            string labelText = GetLabelText(currentTile);

            // 등록되지 않은 타일이면 라벨을 만들지 않음
            if (string.IsNullOrEmpty(labelText))
                continue;

            Vector3 labelPosition = eventTilemap.GetCellCenterWorld(cellPosition);

            TextMeshPro label = Instantiate(
                labelPrefab,
                labelPosition,
                Quaternion.identity,
                labelParent
            );

            label.text = labelText;

            createdLabels.Add(label.gameObject);
        }
    }

    private string GetLabelText(TileBase currentTile)
    {
        if (tileLabels == null)
            return string.Empty;

        foreach (TileLabelInfo info in tileLabels)
        {
            if (info.targetTile == currentTile)
            {
                return info.labelText;
            }
        }

        return string.Empty;
    }

    public void ClearLabels()
    {
        foreach (GameObject labelObject in createdLabels)
        {
            if (labelObject != null)
            {
                Destroy(labelObject);
            }
        }

        createdLabels.Clear();
    }

    public void RefreshLabels()
    {
        CreateLabels();
    }
}
