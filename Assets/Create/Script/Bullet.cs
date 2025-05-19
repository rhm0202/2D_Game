using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f;          // ��Ÿ�
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
        // �浹�� ������Ʈ�� ���̾ �����ͼ� üũ
        if (((1 << other.gameObject.layer) & destroyOnLayers) != 0)
        {
            Destroy(gameObject);
        }
    }
}
