using UnityEngine;

public class TitleScreenScroll : MonoBehaviour
{
    [SerializeField] private Vector3 scrollSpeed = new Vector3(1f, 0f, 0f);

    private Transform scrollTarget;
    private Transform mapTilesParent;

    public void OnEnable()
    {
        MapManager mapManager = GetComponent<MapManager>();
        scrollTarget = mapManager.GenerationCenter;
        mapTilesParent = mapManager.MapTilesParent;
    }

    private void LateUpdate()
    {
        mapTilesParent.position += scrollSpeed * Time.deltaTime;
        scrollTarget.position -= scrollSpeed * Time.deltaTime;
    }
}
