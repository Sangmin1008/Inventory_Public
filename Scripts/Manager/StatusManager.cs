using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    [Header("Initial Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attack = 10f;
    [SerializeField] private float defense = 5f;

    [Header("Current Stats")]
    [SerializeField] private float currentHealth = 30f;
    
    [Header("Extra Stats")]
    [SerializeField] private float extraAttack = 0f;
    [SerializeField] private float extraDefense = 0f;

    [Header("Event Channels")]
    [SerializeField] private FloatEventChannelSO OnHeal;
    [SerializeField] private FloatEventChannelSO OnAttackIncreased;
    [SerializeField] private FloatEventChannelSO OnDefenseIncreased;
    [SerializeField] private VoidEventChannelSO OnStatusChanged;

    public float MaxHealth => maxHealth;
    public float Attack => attack;
    public float Defense => defense;

    public float CurrentHealth => currentHealth;

    public float ExtraAttack => extraAttack;
    public float ExtraDefense => extraDefense;

    private void Start()
    {
        OnStatusChanged.RegisterListener(UIManager.Instance.StatusUI.Render);
    }

    private void OnEnable()
    {
        OnHeal.RegisterListener(ApplyHeal);
        OnAttackIncreased.RegisterListener(ApplyAttack);
        OnDefenseIncreased.RegisterListener(ApplyDefense);
    }
    
    private void OnDisable()
    {
        OnHeal.UnregisterListener(ApplyHeal);
        OnAttackIncreased.UnregisterListener(ApplyAttack);
        OnDefenseIncreased.UnregisterListener(ApplyDefense);
    }
    
    private void ApplyHeal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void ApplyAttack(float amount)
    {
        extraAttack += amount;
    }

    private void ApplyDefense(float amount)
    {
        extraDefense += amount;
    }
}
