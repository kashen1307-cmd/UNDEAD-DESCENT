using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float Cameraspeed ;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(target.position.x, target.position.y, -10f);

        transform.position = Vector3.Slerp(transform.position, position, Cameraspeed * Time.deltaTime);
    }
}
