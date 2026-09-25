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
            Event3(cellPosition);
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
                Debug.Log("프린터 작동 시작! (n분 대기 필요)");
                currentPrinterState = PrinterState.Printing;
                break;

            case PrinterState.Printing:
                // n분이 경과하기 전 상호작용 시도
                Debug.Log("프린터가 출력 중입니다. 대기 시간이 지나야 합니다.");
                break;

            case PrinterState.ReadyToCollect:
                //  업무 완료 및 비활성화
                Debug.Log("출력물 수령 완료! (프린터 업무 완수)");
                currentPrinterState = PrinterState.Finished;

                // 상호작용 타일 삭제
                eventTilemap.SetTile(cellPosition, null);
                break;

            case PrinterState.Finished:
                break;
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


    private void Event3(Vector3Int cellPosition)
    {
        Debug.Log("3번");
    }
}

