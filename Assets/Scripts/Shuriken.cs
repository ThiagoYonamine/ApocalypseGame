using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Projectile
{
    private bool fired = false;

    protected override void Move()
    {
        if (!fired)
        {
            Vector2 enemyDirection = target.transform.position - transform.position;
            float enemyDirectionx = target.transform.position.x - transform.position.x;
            //enemyDirection.normalized
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.02f*enemyDirection.x, 0.1f*enemyDirection.y), ForceMode2D.Impulse);
            fired = true;
        } else {
            speed *= 1.5f;
        }
    }

    // Folow enemy shold be on Update method
    protected void Follow()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        //Bullet to follow enemy
        Vector3 enemyDirection = target.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && hits < maxHits)
        {
            hits++;

            collision.GetComponent<Enemy>().Hit(damage);//todo get TowerDamage instead of "bullet" damage
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
}
