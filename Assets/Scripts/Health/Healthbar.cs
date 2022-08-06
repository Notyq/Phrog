using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    public void Start()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    public void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
