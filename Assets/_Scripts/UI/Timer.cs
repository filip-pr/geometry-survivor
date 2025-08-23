using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    private float timer = 0f;
    private bool stopped = false;

    private string TimeText
    {
        get
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            return "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public float TimeElapsed => timer;

    public void StopTimer()
    {
        stopped = true;
    }

    public void StartTimer()
    {
        stopped = false;
    }

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!stopped)
        {
            timer += Time.deltaTime;
        }
        textMesh.text = TimeText;
    }
}
