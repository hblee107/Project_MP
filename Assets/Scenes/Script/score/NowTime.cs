using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NowTime : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private timeManager timeMgr; // 기존 timeManager 연결
    [SerializeField] private TextMeshProUGUI timeText; // 왼쪽 아래 텍스트 UI
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Image sliderImage;

    [Header("설정")]
    [SerializeField] private string color = "#DC143C";
    [SerializeField] private float sliderDuration = 0.5f;

    Color sliderColor;

    int endhour;
    int starthour;
    float lastTargetValue = -1f; // 이전 목표값 저장용
    Coroutine fillCoroutine;

    void Start()
    {
        endhour = (timeMgr.endHour * 60) + timeMgr.endMinute;
        starthour = (timeMgr.startHour * 60) + timeMgr.startMinute;

        timeSlider.minValue = 0f;
        timeSlider.maxValue = endhour - starthour;
        timeSlider.value = timeSlider.maxValue;

        ColorUtility.TryParseHtmlString(color, out sliderColor);
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
        }

        if (!Mathf.Approximately(lastTargetValue, remainingMinutes))
        {
            lastTargetValue = remainingMinutes;
            SetSlider(remainingMinutes);
        }

        timeText.text = $"{hour:D2} : {minute:D2}";
    }

    public void SetSlider(float targetSlider)
    {
        StopAllCoroutines();
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
}
