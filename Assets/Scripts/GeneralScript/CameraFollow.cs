using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ????????? ?? ??????
    public BoxCollider2D bounds; // ????????, ???? ???????? ???? ???????

    private float halfHeight;
    private float halfWidth;

    private void Start()
    {
        // ?????????? ???????? ?????? ?? ?????? ?????? ? ??????????? ?? ???????
        Camera camera = GetComponent<Camera>();
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * camera.aspect;
    }

    private void Update()
    {
        if (player != null && bounds != null)
        {
            // ????????? ??????? ??????
            Vector3 targetPosition = player.position;

            // ????????? ???? ??????? ? ?????????
            Bounds boundBounds = bounds.bounds;
            float minX = boundBounds.min.x + halfWidth;
            float maxX = boundBounds.max.x - halfWidth;
            float minY = boundBounds.min.y + halfHeight;
            float maxY = boundBounds.max.y - halfHeight;

            // ????????? ??????? ??????, ??? ???? ?? ???????? ?? ???? ???????
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

            // ???????????? ???? ??????? ??????
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}