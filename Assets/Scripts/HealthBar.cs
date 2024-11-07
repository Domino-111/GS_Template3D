using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Damageable agent;
    [SerializeField] private Gradient gradient;

    private Image healthBar;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = agent.GetHealthPercent();
        healthBar.color = gradient.Evaluate(agent.GetHealthPercent());
    }
}
