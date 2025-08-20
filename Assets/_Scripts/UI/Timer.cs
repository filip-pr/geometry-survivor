using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    private string TimeText
    {
        get
        {
            float time = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = TimeText;
    }
}
