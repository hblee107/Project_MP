using UnityEngine;
using TMPro;
public class NowTime : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private timeManager timeMgr; // 기존 timeManager 연결
    [SerializeField] private TextMeshProUGUI timeText; // 왼쪽 아래 텍스트 UI

    void Update()
    {
        if (timeMgr == null || timeText == null)
            return;

        int hour = timeMgr.currentHour;
        int minute = timeMgr.currentMinute;

        
        timeText.text = $"{hour:D2} : {minute:D2}";
    }
}
