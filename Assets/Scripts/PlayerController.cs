using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    private Camera cameraObj;

    void Start()
    {
        cameraObj = Camera.main;
    }

    void Update()
    {
        movePlayer();
        lookAtMouse();
    }

    void LateUpdate()
    {
        cameraObj.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
    }

    void movePlayer()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float normSpeed = Mathf.Max(Mathf.Abs(vertical), Mathf.Abs(horizontal));

        transform.position += new Vector3(horizontal, vertical, 0.0f).normalized * normSpeed * Time.deltaTime * walkSpeed;
    }

    void lookAtMouse()
    {
        Vector3 mousePos = cameraObj.ScreenToWorldPoint(Input.mousePosition); // Camera.main == current camera

        Vector3 lookAt = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        float zRot = Mathf.Atan2(lookAt.y, lookAt.x);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRot * Mathf.Rad2Deg);

        Debug.Log(zRot);
    }
}
