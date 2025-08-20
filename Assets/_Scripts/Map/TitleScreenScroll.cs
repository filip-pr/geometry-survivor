using UnityEngine;

public class TitleScreenScroll : MonoBehaviour
{
    [SerializeField] private Vector3 scrollSpeed = new Vector3(1f, 0f, 0f);

    [SerializeField] private GameObject scrollTarget;

    private void LateUpdate()
    {
        transform.position += scrollSpeed * Time.deltaTime;
        scrollTarget.transform.position -= scrollSpeed * Time.deltaTime;
    }
}
