using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float Cameraspeed ;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        // 2. Lock the camera onto it!
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogError("Camera couldn't find the Player! Is the tag set?");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, position, Cameraspeed * Time.deltaTime);
    }
}
