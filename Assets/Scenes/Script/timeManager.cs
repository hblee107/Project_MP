using UnityEngine;

public class timeManager : MonoBehaviour
{
    public playerMove player;

    [Header("시간 설정")]
    [SerializeField] int startHour = 0;
    [SerializeField] int startMinute = 0;
    [SerializeField] int endHour = 0;
    [SerializeField] int endMinute = 0;

    int currentHour = 0;
    int currentMinute = 0;


    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCalulation(player.moveCount);
    }

    void timeCalulation(int moveNum)
    {
        
        int currentTime = (startHour * 60) + startMinute + moveNum * 15;

        if (currentTime >= (endHour * 60) + endMinute)
        {
            Debug.Log($"<게임오버> 현재 {endHour:D2}:{endMinute:D2}로 퇴근시간입니다.");
        }
        else 
        {
            //Debug.Log($"현재 시간은 {currentHour:D2}:{currentMinute:D2}입니다.");
        }

        currentHour = currentTime / 60;
        currentMinute = currentTime % 60;
    }

}
