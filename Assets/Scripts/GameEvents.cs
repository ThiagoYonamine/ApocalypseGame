using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public event Action<GameObject> onTriggerPowerUp;
    public void TriggerPowerUp(GameObject power)
    {
        onTriggerPowerUp(power);
    }

    public event Action<GameObject, int> onFinishPowerUp;
    public void FinishPowerUp(GameObject power, int id)
    {
        onFinishPowerUp(power, id);
    }


    public void Awake()
    {
        current = this;
    }
}
