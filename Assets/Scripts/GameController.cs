using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    private int collectedItems = 0;
    public int requiredItems = 3; // จำนวนไอเท็มที่ต้องเก็บก่อนผ่านด่าน
    public TMP_Text itemText; // UI แสดงจำนวนไอเท็ม
    public TMP_Text finishText; // UI แสดง "Finish" และเวลาที่ใช้

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
        UpdateItemUI();
        finishText.gameObject.SetActive(false); // ซ่อนข้อความ Finish ไว้ก่อน
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (collision.CompareTag("Item"))
        {
            collectedItems++;
            Destroy(collision.gameObject);
            UpdateItemUI();
        }
        else if (collision.CompareTag("Finish"))
        {
            if (collectedItems >= requiredItems)
            {
                FinishLevel();
            }
            else
            {
                Debug.Log("You need to collect all items first!");
            }
        }
    }

    void FinishLevel()
    {
        GameTimer.instance.StopTimer(); // หยุดจับเวลา
        float finalTime = GameTimer.instance.GetElapsedTime();

        // แสดงข้อความ Finish พร้อมระยะเวลาที่ใช้
        finishText.text = "🎉 FINISH! 🎉\nTime: " + finalTime.ToString("F2") + "s";
        finishText.gameObject.SetActive(true);

        Time.timeScale = 0; // หยุดเกม
    }

    public void Updatecheckpoint(Vector2 Pos)
    {
        checkpointPos = Pos;
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = Vector3.one;
        playerRb.simulated = true;
    }

    void UpdateItemUI()
    {
        itemText.text = "Items: " + collectedItems + " / " + requiredItems;
    }
}
