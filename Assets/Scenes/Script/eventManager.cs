using UnityEngine;
using UnityEngine.Tilemaps;

public class eventManager : MonoBehaviour
{
    [Header("플레이어 스크립트")]
    [SerializeField] private playerMove player;

    [Header("이벤트 타일맵")]
    [SerializeField] private Tilemap eventTilemap;

    [Header("이벤트 타일")]
    [SerializeField] private TileBase eventTile1;
    [SerializeField] private TileBase eventTile2;
    [SerializeField] private TileBase eventTile3;
    [SerializeField] private TileBase eventTile4;
    [SerializeField] private TileBase eventTile5;
    [SerializeField] private TileBase startTile;

    private readonly int requiredMinutes = 120; // 프린트 출력 필요 시간
    private readonly int minutesPerMove = 15;   // 1칸 이동당 15분
    private int printStartMoveCount = 0;       // 프린터 작동을 시작한 시점의 moveCount


    [SerializeField] int document = 0;

    private bool isCollectDocument = false;

    private bool isReportCompleted = false; // 보고서 작성 완료 여부

    // 프린터 작동 상태
    private enum PrinterState { ReadyToStart, Printing, ReadyToCollect, Finished }
    private PrinterState currentPrinterState = PrinterState.ReadyToStart;

  
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
            GetDocument(cellPosition);
        }
        else if (currentTile == eventTile4)
        {
            Event4();
        }
        else if (currentTile == eventTile5)
        {
            Clear();
        }
    }

    private void Event1(Vector3Int cellPosition)
    {
        Debug.Log("보고서 작성 완료!");


        // 비활성화
        isReportCompleted = true;
        eventTilemap.SetTile(cellPosition, null);
    }

    private void Event2(Vector3Int cellPosition)
    {
        // 보고서를 안 쓰고 상호작용했을 떄
        if (!isReportCompleted)
        {
            Debug.Log("보고서를 먼저 작성해야 합니다.");
            return;
        }

 
        switch (currentPrinterState)
        {
            case PrinterState.ReadyToStart:
                // 프린터 작동 시작
                printStartMoveCount = player.moveCount;
                currentPrinterState = PrinterState.Printing;
                Debug.Log("프린터 작동 시작! (2시간 대기 필요)");
                break;

            case PrinterState.Printing:
                // 프린터 작동 이후 경과한 이동횟수계산
                int elapsedMoves = player.moveCount - printStartMoveCount;
                int elapsedMinutes = elapsedMoves * minutesPerMove;

                // 2시간 이상 지났는지
                if (elapsedMinutes >= requiredMinutes)
                {
                    Debug.Log($"🎉 출력물 수령 완료!  프린터 업무 완수");
                    currentPrinterState = PrinterState.Finished;

                    // 타일 비활성화
                    eventTilemap.SetTile(cellPosition, null);
                }
                else
                {
                    Debug.Log($"⏳ 아직 출력 중입니다. ({elapsedMinutes}분 / {requiredMinutes}분 경과 - {requiredMinutes - elapsedMinutes}분 더 필요)");
                }
                break;

            case PrinterState.Finished:
                break;
        }
    }

    private void GetDocument(Vector3Int cellPosition)
    {
        Debug.Log("서류 획득");
        document++;
        eventTilemap.SetTile(cellPosition, null);
    }

    private void Event4()
    {
        if (CountTiles() <= 0)
        {
            Debug.Log("서류를 모두 모았습니다.");
            isCollectDocument = true;
        }
        else
        {
            Debug.Log($"아직 서류가 {CountTiles()}개 남았습니다.");
        }
    }

    private void Clear()
    {
        if (isCollectDocument)
        {
            Debug.Log("게임 클리어!");
        }
        else
        {
            Debug.Log("아직 해야 할 일이 남았습니다.");
        }
    }

    public void CompletePrinterProcess()
    {
        if (currentPrinterState == PrinterState.Printing)
        {
            currentPrinterState = PrinterState.ReadyToCollect;
            Debug.Log("프린트 출력이 완료되어 수령 가능한 상태가 되었습니다.");
        }
    }

    public int CountTiles()
    {
        if (eventTilemap == null || eventTile3 == null)
            return 0;

        int count = 0;
        BoundsInt bounds = eventTilemap.cellBounds;

        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            TileBase currentTile = eventTilemap.GetTile(cellPosition);

            if (currentTile == eventTile3)
            {
                count++;
            }
        }

        return count;
    }

    public Vector3Int GetStartCell()
    {
        if (startTile == null)
        {
            Debug.LogError("플레이어 시작 위치 타일이 연결되지 않았습니다.");
            return Vector3Int.zero;
        }

        int startTileCount = 0;
        Vector3Int startCell = Vector3Int.zero;
        BoundsInt bounds = eventTilemap.cellBounds;

        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            TileBase currentTile = eventTilemap.GetTile(cellPosition);

            if (currentTile == startTile)
            {
                startCell = cellPosition;
                startTileCount++;
            }
        }

        if (startTileCount == 0)
        {
            Debug.LogError("플레이어 시작 위치 타일을 찾지 못했습니다.");
        }
        else if (startTileCount > 1)
        {
            Debug.LogWarning($"시작 위치 타일이 {startTileCount}개 이므로 확인이 필요합니다.");
        }

        return startCell;
    }

}

