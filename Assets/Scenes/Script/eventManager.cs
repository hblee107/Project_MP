using UnityEngine;
using UnityEngine.Tilemaps;

public class eventManager : MonoBehaviour
{
    [Header("이벤트 타일맵")]
    [SerializeField] private Tilemap eventTilemap;
    [SerializeField] private playerMove player;

    [Header("이벤트 타일")]
    [SerializeField] private TileBase eventTile1;
    [SerializeField] private TileBase eventTile2;
    [SerializeField] private TileBase eventTile3;
    [SerializeField] private TileBase eventTile4;
    [SerializeField] private TileBase eventTile5;

    private readonly int requiredMinutes = 60; // 프린트 출력 필요 시간
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
            Event4(cellPosition);
        }
        else if (currentTile == eventTile5)
        {
            Clear(cellPosition);
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

                Debug.Log("프린터 작동 시작! (1시간 대기 필요)");
                break;

            case PrinterState.Printing:
                // 프린터 작동 이후 경과한 이동횟수계산
                int elapsedMoves = player.moveCount - printStartMoveCount;
                int elapsedMinutes = elapsedMoves * minutesPerMove;

                // 1시간 이상 지났는지
                if (elapsedMinutes >= requiredMinutes)
                {
                    currentPrinterState = PrinterState.ReadyToCollect;

               
                }
                else
                {
                    Debug.Log($"⏳ 아직 출력 중입니다. ({elapsedMinutes}분 / {requiredMinutes}분 경과 - {requiredMinutes - elapsedMinutes}분 더 필요)");
                }
                break;

            case PrinterState.ReadyToCollect:
                // 출력물 수령
                Debug.Log("🎉 출력물 수령 완료! 프린터 업무 완수");

                currentPrinterState = PrinterState.Finished;

                // 모든 보라색 프린터 타일 제거
                RemoveAllPrinterTiles();
                break;


            case PrinterState.Finished:
                // 이미 완료된 상태
                break;
        }
    }
       // 프린터지우기
    private void RemoveAllPrinterTiles()
    {
        if (eventTilemap == null || eventTile2 == null)
            return;

        BoundsInt bounds = eventTilemap.cellBounds;

        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            if (eventTilemap.GetTile(cellPosition) == eventTile2)
            {
                eventTilemap.SetTile(cellPosition, null);
            }
        }
    }

    private void GetDocument(Vector3Int cellPosition)
    {
        Debug.Log("4번");
        document++;
        eventTilemap.SetTile(cellPosition, null);
    }

    private void Event4(Vector3Int cellPosition)
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

    private void Clear(Vector3Int cellPosition)
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

}

