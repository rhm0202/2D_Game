using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f;          // 사거리
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
        // 충돌한 오브젝트의 레이어를 가져와서 체크
        if (((1 << other.gameObject.layer) & destroyOnLayers) != 0)
        {
            Destroy(gameObject);
        }
    }
}
