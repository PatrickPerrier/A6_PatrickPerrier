using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float minX = -10f;

    [SerializeField]
    float maxX = 10f;

    private Vector3 newPos;

    [SerializeField]
    GameObject target;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, minX, maxX), 5, -15);
    }
    
}
