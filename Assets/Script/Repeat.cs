using UnityEngine;

public class Repeat : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;
    private Camera mainCamera;

    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found!");
            return;
        }

        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        if (transform.position.x + repeatWidth < cameraLeftEdge)
        {
            transform.position += Vector3.right * (repeatWidth * 2);
        }
    }
}
