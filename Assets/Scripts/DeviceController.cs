using System.Threading;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    public float rotationSpeed;

    private float X;
    private float Y;

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, -Input.GetAxis("Mouse X") * rotationSpeed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        else
        {

            if (transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                Debug.Log(transform.rotation.eulerAngles);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
            }
        }
    }
}
