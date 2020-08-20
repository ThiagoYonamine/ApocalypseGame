using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    //Enemy to follow
    protected GameObject target;

    //Tower that fired
    protected GameObject tower;

    protected int hits = 0;
    protected int maxHits = 1;

    public float speed;
    public float damage;

    public virtual void SetTower(GameObject ptower)
    {
        tower = ptower;
        FindTarget();
    }

    protected abstract void Move();
    protected virtual void FindTarget()
    {
        target = tower.GetComponent<Tower>().GetTarget();
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Move();
        }
    }

}
