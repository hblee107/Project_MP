using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NowTime : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private timeManager timeMgr; // 기존 timeManager 연결
    [SerializeField] private TextMeshProUGUI timeText; // 왼쪽 아래 텍스트 UI
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Image sliderImage;

    [Header("설정")]
    [SerializeField] private string sliderColorHex = "#DC143C";
    [SerializeField] private float sliderDuration = 0.5f;

    [Header("텍스트 변경 3색")]
    [SerializeField]
    private string[] textColor = new string[]
    {
        "#FF4D4D", // 1번째 색상 (빨강)
        "#FFD700", // 2번째 색상 (노랑)
        "#4DFF88"  // 3번째 색상 (초록)
    };
    [SerializeField] private float colorChangeTime = 0.5f; // 변경 간격(초)

    Color sliderColor;
    List <Color> parsedTextColors = new List<Color>();

    int endhour;
    int starthour;

    float lastTargetValue = -1f;
    Coroutine fillCoroutine;
    
    Coroutine colorLoopCoroutine;


    void Start()
    {
        endhour = (timeMgr.endHour * 60) + timeMgr.endMinute;
        starthour = (timeMgr.startHour * 60) + timeMgr.startMinute;

        timeSlider.minValue = 0f;
        timeSlider.maxValue = endhour - starthour;
        timeSlider.value = timeSlider.maxValue;

        ColorUtility.TryParseHtmlString(sliderColorHex, out sliderColor);

        parsedTextColors.Clear();
        foreach (string hex in textColor)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color c))
            {
                parsedTextColors.Add(c);
            }
        }
    }

    void Update()
    {
        if (timeMgr == null || timeText == null)
            return;

        int hour = timeMgr.currentHour;
        int minute = timeMgr.currentMinute;

        int currenthour = (timeMgr.currentHour * 60) + timeMgr.currentMinute;
        int remainingMinutes = endhour - currenthour;

        if ((endhour - currenthour) <= 60)
        {
            sliderImage.color = sliderColor;

            if (colorLoopCoroutine == null && parsedTextColors.Count > 0)
            {
                colorLoopCoroutine = StartCoroutine(CycleTextColors());
            }
        }

        if (!Mathf.Approximately(lastTargetValue, remainingMinutes))
        {
            lastTargetValue = remainingMinutes;
            SetSlider(remainingMinutes);
        }

        timeText.text = $"{hour:D2} : {minute:D2}";
    }

    // 슬라이더(진행 바) 관련
    public void SetSlider(float targetSlider)
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        float clampedTarget = Mathf.Clamp(targetSlider, timeSlider.minValue, timeSlider.maxValue);
        fillCoroutine = StartCoroutine(FillAnime(clampedTarget));
    }

    private IEnumerator FillAnime(float target)
    {
        float start = timeSlider.value;
        float elapsed = 0f;

        while (elapsed < sliderDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / sliderDuration);
            timeSlider.value = Mathf.Lerp(start, target, t);
            yield return null;
        }

        timeSlider.value = target;
    }

    // 시간 색상 변경
    private IEnumerator CycleTextColors()
    {
        int index = 0;

        while (true)
        {
            timeText.color = parsedTextColors[index];
            index = (index + 1) % parsedTextColors.Count;
            yield return new WaitForSeconds(colorChangeTime); // 변수명 일치
        }
    }
}
