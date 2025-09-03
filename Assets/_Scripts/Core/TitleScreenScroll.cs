using UnityEngine;

/// <summary>
/// Script handling the title screen background scrolling.
/// </summary>
public class TitleScreenScroll : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0.1f);

    private void Update()
    {
        transform.position += (Vector3)(scrollSpeed * Time.deltaTime);
    }
}
