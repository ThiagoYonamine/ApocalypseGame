using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButton : MonoBehaviour
{
    public GameObject powerBtn;
    public Image powerDelay;
    public Image powerFill;
    private float powerCooldown;
    private float currentFill;

    public void SetPowerCooldown(float x)
    {
        powerCooldown = x;
    }

    public void ResetCooldown()
    {
        powerBtn.SetActive(false);
        powerDelay.gameObject.SetActive(true);
        powerFill.gameObject.SetActive(true);
        currentFill = 0f;
    }

    private void Start()
    {
        ResetCooldown();
        powerCooldown = 15f;
        currentFill = 0f;
        powerFill.fillAmount = currentFill/powerCooldown;
    }

    private void UpdateCooldown()
    {
        currentFill += Time.deltaTime;
        powerFill.fillAmount = currentFill / powerCooldown;
    }

    private void EnablePower()
    {
        if(powerFill.fillAmount >= 1f)
        {
            powerBtn.SetActive(true);
            powerDelay.gameObject.SetActive(false);
            powerFill.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateCooldown();
        EnablePower();
    }
}
