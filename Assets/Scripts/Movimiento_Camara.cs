using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Camara : MonoBehaviour
{
    public float sensitivity = 100f;
    public float minLookAngle = -60f;
    public float maxLookAngle = 80f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;


        Vector3 initialRotation = transform.localRotation.eulerAngles;
        xRotation = initialRotation.x;
        yRotation = initialRotation.y;
    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

}
