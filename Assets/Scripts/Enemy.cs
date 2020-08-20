using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject attackPrefab;
    public Animator animator;

    public float range = 2f;
    private float speed = 1f;
    private float health = 100f;


     

    public void Hit(float damage)
    {
        animator.SetTrigger(Constant.ANIM_HIT);
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }
    }

    public void Attack()
    {

        Instantiate(attackPrefab, this.transform.position, this.transform.rotation);
    }


    public void ExecuteWalk()
    {
        Rigidbody2D rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
    }

    private void CheckAttack()
    {
        if (Mathf.Abs(Constant.WALL_POSITION - transform.position.x) <= range)
        {
           
            animator.SetBool(Constant.ANIM_ATTACK, true);
        }
        else
        {
            animator.SetBool(Constant.ANIM_ATTACK, false);
        }
    }





    public void Update()
    {
        CheckAttack();
    }
}
