using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 10f;
    //todo create const to base position
    private Vector3 wallPosition;

    private float angle;
    // Start is called before the first frame update
    void Start()
    {

        angle = -30f;
        wallPosition = new Vector3(Constant.WALL_POSITION, transform.position.y, -9f);
        rb.AddForce(new Vector2(0,2), ForceMode2D.Impulse);
        //rb.AddRelativeForce(new Vector2(-1, 1), ForceMode2D.Impulse);
        //rb.AddTorque(-100f);
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, wallPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, wallPosition, Time.deltaTime * speed);
        }
        else
        {
            transform.position = wallPosition;
        }

        if (angle < 20)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            angle += Time.deltaTime * (Mathf.Abs(wallPosition.x - transform.position.x)) * (speed / 1.1f);
        }
    }

    void Update()
    {

        Move();
       
    }

}
