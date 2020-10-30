using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float moveSpeed = 4f;
    private Transform playert;
    private Rigidbody2D rb;
    private Vector2 movement;
    private RaycastHit2D hit;

    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    void Start()
    {
        
        rb = this.GetComponent<Rigidbody2D>();
        playert = player.transform;
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));


    }

    // Update is called once per frame
    void Update()
    {
        if (hit.collider.tag != "Player")
        {
            randomMove();
        }
        Vector2 direction = playert.position - transform.position;
        Vector2 pos = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);
        if (player != null && hit.collider.tag == "Player" && pos.x < maxX && pos.x > minX && pos.y > minY && pos.y < maxY)
        {
            playerDirection();
        }
        



    }

    private void FixedUpdate()
    {
       
        Vector3 direction = playert.position - transform.position;
        hit = Physics2D.Raycast(transform.position, direction,4,~LayerMask.GetMask("Enemy"));
        Vector2 direction2 = playert.position - transform.position;
        Vector2 pos = (Vector2)transform.position + (direction2 * moveSpeed * Time.deltaTime);
        if (hit.collider.tag == "Player" && pos.x < maxX && pos.x > minX && pos.y > minY && pos.y < maxY)
        {
            if (player != null)
            {
                moveCharacter(movement);
            }
        }
        

    }

    void playerDirection()
    {
        Vector3 direction = playert.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        float distance = Vector2.Distance(playert.position, transform.position);
        if (distance < 0.4f)
        {
            Destroy(player);
        }

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void PatrolRandomSpot()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void randomMove()
    {
        lookDir();
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < 0.3f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void lookDir()
    {
        Vector3 direction = moveSpot.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }



}
