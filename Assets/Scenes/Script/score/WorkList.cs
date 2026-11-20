using UnityEngine;
using TMPro;

public class WorkList : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private eventManager eventMgr; // 기존 eventManager 연결
    [SerializeField] private TextMeshProUGUI questText; // 우측 상단 업무 내용 텍스트 UI

    [SerializeField] private string color;
    [SerializeField] private int size;

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

        string docStatus = (currentDocs >= totalDocuments) ? $"<size={size}><color={color}><s>서류 얻기(완료)</s></color></size>" : $"서류 얻기({currentDocs}/{totalDocuments})";
        
        // 2. 보고서 작성 여부
        string reportStatus = eventMgr.IsReportCompleted ? $"<size={size}><color={color}><s>보고서 작성(완료)</s></color></size>" : "보고서 작성(0/1)";

        // 3. 인쇄 상태
        string printStatus = eventMgr.IsPrintFinished ? $"<size={size}><color={color}><s>인쇄(완료)</s></color></size>" : "인쇄(0/1)";

        // 4. 서류 제출
        string submitStatus = eventMgr.IsCollectDocument ? $"<size={size}><color={color}><s>서류 제출(완료)</s></color></size>" : "서류 제출(0/1)";

        // 텍스트 조합
        questText.text =
            $"{docStatus}\n" +
            $"{reportStatus}\n" +
            $"{printStatus}\n" +
            $"{submitStatus}";
    }
}