using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float damageBonus;
    public float speedBonus;
    public float duration;

    private int id;

    public void SetId(int _id)
    {
        id = _id;
        Invoke("Finish", duration);
    }

    public void Finish()
    {
        GameEvents.current.FinishPowerUp(gameObject, id);
    }

}
