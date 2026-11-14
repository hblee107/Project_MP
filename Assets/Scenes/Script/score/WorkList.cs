using UnityEngine;
using TMPro;

public class WorkList : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private eventManager eventMgr; // 기존 eventManager 연결
    [SerializeField] private TextMeshProUGUI questText; // 우측 상단 업무 내용 텍스트 UI

    void Update()
    {
        if (eventMgr == null || questText == null)
            return;

        UpdateQuestDisplay();
    }

    void UpdateQuestDisplay()
    {
        // 1. 서류 얻기 (전체 3개 중 몇 개를 모았는지 계산)
        int totalDocuments = 3;
        int remainingDocs = eventMgr.CountTiles();
        int currentDocs = Mathf.Clamp(totalDocuments - remainingDocs, 0, totalDocuments);

        string docStatus = (currentDocs >= totalDocuments) ? "(완료)" : $"({currentDocs}/{totalDocuments})";

        // 2. 보고서 작성 여부
        string reportStatus = eventMgr.IsReportCompleted ? "(완료)" : "(0/1)";

        // 3. 인쇄 상태
        string printStatus = eventMgr.IsPrintFinished ? "(완료)" : "(0/1)";

        // 4. 서류 제출
        string submitStatus = eventMgr.IsCollectDocument ? "(완료)" : "(0/1)";

        // 텍스트 조합
        questText.text =
            $"서류 얻기{docStatus}\n" +
            $"보고서 작성{reportStatus}\n" +
            $"인쇄{printStatus}\n" +
            $"서류 제출{submitStatus}";
    }
}