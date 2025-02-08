using UnityEngine;
using UnityEngine.SceneManagement; // ✅ เพิ่ม SceneManager

public class SceneManage : MonoBehaviour
{
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // เปลี่ยน Scene ตามชื่อที่กำหนด
    }
}
