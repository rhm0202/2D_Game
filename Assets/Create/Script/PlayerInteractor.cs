using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 2f;
    public LayerMask interactLayerMask; // ← "Portal" or "Interactable" 레이어만

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, interactDistance, interactLayerMask);

        if (hit.collider != null)
        {
            // 예: 포탈일 경우
            Portal portal = hit.collider.GetComponent<Portal>();
            if (portal != null)
            {
                portal.Enter(); // 예시 메서드
                return;
            }

            //IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            //if (interactable != null)
            //{
            //    interactable.Interact();
            //}
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * interactDistance));
    }
}