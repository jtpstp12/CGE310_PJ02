using UnityEngine;
using TMPro; // ✅ ใช้กับ TextMeshPro

public class TimerDisplay : MonoBehaviour
{
    public TMP_Text timerText; // ✅ แก้ให้ใช้ TMP_Text

    void Update()
    {
        if (GameTimer.instance != null)
        {
            float time = GameTimer.instance.GetElapsedTime();
            timerText.text = "Time: " + time.ToString("F2"); // แสดงเวลาแบบ 2 ตำแหน่ง
        }
    }
}
