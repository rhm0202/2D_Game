using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f;          // ��Ÿ�
    public int damage = 1;                   // ���ݷ�
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
            Destroy(gameObject); // ��Ÿ� �ʰ� �� ����
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

            Destroy(gameObject); // �Ѿ� ����
        }
    }
}
