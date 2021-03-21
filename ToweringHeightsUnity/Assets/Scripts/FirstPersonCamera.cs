using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float lookSpeed = 200f;
    public float xRotation = 0f;
    public Transform playerBody;
    public Transform equippedWeapon;

    public static FirstPersonCamera fpsCamInstance;

    // Start is called before the first frame update
    private void Awake()
    {
        fpsCamInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * lookSpeed;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotates camera 180 degrees up and down & and rotates body left and right
        equippedWeapon.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
