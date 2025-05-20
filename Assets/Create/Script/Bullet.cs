using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f;          // 사거리
    public int damage = 1;                   // 공격력
    public LayerMask destroyOnLayers;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float traveled = Vector3.Distance(startPosition, transform.position);

        if (traveled >= maxDistance)
        {
            Destroy(gameObject); // 사거리 초과 시 삭제
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & destroyOnLayers) != 0)
        {
            ObjectManager target = other.GetComponent<ObjectManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // 총알 삭제
        }
    }
}
