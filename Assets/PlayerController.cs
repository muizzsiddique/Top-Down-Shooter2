using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float normSpeed = Mathf.Max(Mathf.Abs(vertical), Mathf.Abs(horizontal));

        transform.position += new Vector3(horizontal, vertical, 0.0f).normalized * Time.deltaTime * normSpeed * walkSpeed;
    }
}
