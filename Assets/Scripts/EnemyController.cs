using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State { Patrol, Wait, Chase }

    Rigidbody2D rb;

    State state = State.Wait;
    bool canChangeState = false;
    IEnumerator patrolRoutine;

    Vector3 target;
    bool inBoundary = true;

    float rotation = 0.0f;

    public Collider2D boundary;
    public GameObject player;
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        patrolRoutine = DoPatrol(1f, 2f);
        StartCoroutine(patrolRoutine);
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Patrol:
                rb.velocity = (target - transform.position).normalized * moveSpeed;
                break;
            case State.Wait:
                rb.velocity = Vector3.zero;
                break;
            case State.Chase:
                rb.velocity = (player.transform.position - transform.position).normalized * moveSpeed;
                break;
        }

        if (rb.velocity != Vector2.zero)
            rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        rb.rotation = rotation;

        if (canChangeState && state != State.Chase && inBoundary && Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            state = State.Chase;
            StopCoroutine(patrolRoutine);
        }
        else if (state == State.Chase && !inBoundary && Vector3.Distance(player.transform.position, transform.position) >= 2f)
        {
            StartCoroutine(patrolRoutine);
        }
    }

    IEnumerator DoPatrol(float startTime, float waitTime)
    {
        canChangeState = false;
        state = State.Wait;
        NextTarget();
        yield return new WaitForSeconds(startTime);

        canChangeState = true;
        while (true)
        {
            state = State.Patrol;
            yield return new WaitUntil(() => (target - transform.position).magnitude < 0.3f);
            state = State.Wait;
            NextTarget();
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void OnHit()
    {
        StopAllCoroutines();
        GetComponentInParent<Transform>().gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == boundary)
        {
            inBoundary = false;
            if (gameObject.activeSelf) StartCoroutine(patrolRoutine);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == boundary)
        {
            inBoundary = true;
        }
    }

    void NextTarget()
    {
        target = new Vector3(
            Random.Range(boundary.bounds.min.x, boundary.bounds.max.x),
            Random.Range(boundary.bounds.min.y, boundary.bounds.max.y)
        );
    }
}
