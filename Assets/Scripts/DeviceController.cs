using System.Threading;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    public float rotationSpeed;
    public float resetSpeed = 10f;

    private float X;
    private float Y;

    private float lerpUnit = 0.01f;

    // Update is called once per frame
    void Update()
    {
        
        // Rotate device on right-click.
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, -Input.GetAxis("Mouse X") * rotationSpeed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        else
        {
            // Reset rotation when right-click is released.
            if (transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), lerpUnit * resetSpeed);
            }
        }
    }
}
