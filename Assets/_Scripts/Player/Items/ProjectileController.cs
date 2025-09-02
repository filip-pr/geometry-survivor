using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifeSpan = 15f;

    private float lifeTime = 0f;
    private Vector2 direction = Vector2.zero;

    public void Setup(Vector2 origin, Vector2 direction)
    {
        transform.position = origin;
        this.direction = direction;
        Rotate();
    }

    private void Rotate()
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= lifeSpan)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);
    }
}
