using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFAnimator : MonoBehaviour
{
    public float animationSpeed;

    void Update()
    {
        transform.localScale -= new Vector3(1/animationSpeed, 1/animationSpeed) * Time.deltaTime;
        if (transform.localScale.x <= 0f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
