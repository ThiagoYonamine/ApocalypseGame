using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    protected override void Move()
    {
        Vector2 enemyDirection = target.transform.position - transform.position;
       
        this.GetComponent<Rigidbody2D>().AddForce(enemyDirection.normalized * speed, ForceMode2D.Impulse);
    }

    /* Folow enemy shold be on Update method
    protected override void Move()
    {
        //Bullet to follow enemy
        Vector3 enemyDirection = target.transform.position - transform.position;
        this.transform.Translate(enemyDirection.normalized * Time.deltaTime * speed);
    }
    */

    // Single target
    /*
    private void CheckHit()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
       
        if (dist <= 0.5f)
        {
            target.GetComponent<Enemy>().Hit(damage);
            Destroy(this.gameObject);
        }
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.transform.tag == Constant.TAG_ENEMY && hits < maxHits)
        {
            hits++;
            
            collision.GetComponent<Enemy>().Hit(damage);//todo get TowerDamage instead of "bullet" damage
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    /*
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        //Move();
    }
    */
}
