using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public GameObject muzzleFlash;

    private Camera cameraObj;
    private Vector3 cameraDiff;
    private Rigidbody2D rb;

    void Start()
    {
        cameraObj = Camera.main;
        cameraDiff = cameraObj.transform.position - transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        movePlayer();
    }

    void Update()
    {
        lookAtMouse();
    }

    void LateUpdate()
    {
        cameraObj.transform.position = transform.position + cameraDiff;
    }

    void movePlayer()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontal, vertical) * walkSpeed;
    }

    void lookAtMouse()
    {
        Vector3 mousePos = cameraObj.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        float zRot = Mathf.Atan2(lookAt.y, lookAt.x);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRot * Mathf.Rad2Deg);

        shootAtMouse(mousePos);
    }

    void shootAtMouse(Vector3 mousePosition)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(muzzleFlash);
            Vector3 position = Vector3.zero;

            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
                if (t.gameObject.name == "MFSpawn")
                    position = t.position;

            obj.transform.position = position + new Vector3(0f, 0f, -1f);
            obj.transform.Rotate(transform.rotation.eulerAngles);
            obj.GetComponent<MFAnimator>().SetVelocity(rb.velocity);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, ~LayerMask.GetMask("Player"));
            if (hit.collider != null)
                hit.collider.GetComponent<EnemyController>().OnHit();
        }
    }
}

// Testing