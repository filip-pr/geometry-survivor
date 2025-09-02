using UnityEngine;

public class RepulsorProjectileController : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 1f;
    [SerializeField] private float sizeIncreasePerSecond = 1f;

    public Transform Target { get; set; }

    private float lifeTime = 0f;
    

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= lifeSpan)
        {
            Destroy(gameObject);
            return;
        }
        transform.localScale += Vector3.one * sizeIncreasePerSecond * Time.deltaTime;
        transform.position = Target.position;
    }


}
