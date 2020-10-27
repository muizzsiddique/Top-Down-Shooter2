using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    public Camera camera;

    void Start()
    {
        
    }

    void Update()
    {
        movePlayer();
        lookAtMouse();
    }

    void LateUpdate()
    {
        camera.transform.position = transform.position + new Vector3(0.0f, 0.0f, -10.0f);
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
        Vector3 mouse = Input.mousePosition;

        Debug.Log(mouse);
    }
}
