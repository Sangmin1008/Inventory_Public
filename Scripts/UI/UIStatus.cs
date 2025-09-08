using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI attackNumberText;
    [SerializeField] private TextMeshProUGUI defenseNumberText;
    [SerializeField] private TextMeshProUGUI healthNumberText;
    

    private void Start()
    {
        Render();
    }

    // Status 변경 시 새로 Render
    public void Render()
    {
        attackNumberText.text = $"{StatusManager.Instance.Attack}(+{StatusManager.Instance.ExtraAttack})";
        defenseNumberText.text = $"{StatusManager.Instance.Defense}(+{StatusManager.Instance.ExtraDefense})";
        healthNumberText.text = $"{StatusManager.Instance.CurrentHealth}/{StatusManager.Instance.MaxHealth}";
    }
    
    public void CloseUI() => UIManager.Instance.CloseUI(UIType.Status);

}
