using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public GameObject bullet;
    public Animator animator;
    public float range = 16f;

    //variable to instatiate effect prebaf. Prefab = position + centerEffect
    public Vector3 centerEffect;

    // Fire Speed = 1/fireSpeed -- How many time it shoot in one sec.
    public float fireSpeed = 1f;
    private float fireCooldown = 0f;
    
    private GameObject target;
    public PowerButton powerBtn;

    public GameObject GetTarget()
    {
        if (target == null)
        {
            FindTarget();
        }

        return target;
    }

    public void ActivatePower(GameObject go)
    {
        GameObject powerObj = Instantiate(go, this.transform.position + centerEffect, this.transform.rotation);
        PowerUp power = powerObj.GetComponent<PowerUp>();
        power.SetId(this.gameObject.GetInstanceID());
        if (power!=null)
        {
            this.fireSpeed += power.speedBonus;
            Debug.Log("fireSpeed power : " + fireSpeed);
        }
        if(powerBtn != null)
        {
            powerBtn.ResetCooldown();
        }
       

    }

    public void FinishPower(GameObject go, int id)
    {
        if (id == this.gameObject.GetInstanceID())
        {
            PowerUp power = go.GetComponent<PowerUp>();
            if (power != null)
            {
                this.fireSpeed -= power.speedBonus;
                Destroy(go);
            }
        }

    }

    public void Select()
    {
       //
    }


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);
        GameEvents.current.onTriggerPowerUp += ActivatePower;
        GameEvents.current.onFinishPowerUp += FinishPower;

        //powerBtn.SetPowerCooldown(5f);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        RefreshBullet();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.7f);
        Gizmos.DrawSphere(this.transform.position, range);
    }

    private void Shoot()
    {
        if(target != null)
        {
            if(fireCooldown <= 0)
            {
                animator.SetTrigger(Constant.ANIM_ATTACK);
                fireCooldown = 1.0f / fireSpeed;
                Vector2 projectilePosition = new Vector2(this.transform.position.x+0.3f, this.transform.position.y);
                GameObject go = Instantiate(bullet, projectilePosition, this.transform.rotation) as GameObject;
                go.GetComponent<Projectile>().SetTower(this.gameObject);
               
            }
        }
    }

    private void RefreshBullet()
    {
        fireCooldown -= Time.deltaTime;
    }

    private void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, range);
        target = FindFirstEnemy(enemies);
    }

    private GameObject FindFirstEnemy(Collider2D[] enemies)
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D go in enemies)
        {
            if(go.transform.tag == Constant.TAG_ENEMY)
            {
                float curDistance = go.transform.position.x;
                if (curDistance < minDistance)
                {
                    closest = go.gameObject;
                    minDistance = curDistance;
                }
            }
        }
        return closest;
    }

}
