using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISuperCooldown : UIStat
{
    [SerializeField] private Slider mySlider;

    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnSuperCooldownUpdate.AddListener(TextUpdate);
        Clicker.ShowSuperClick.AddListener(ShowSuperClick);
    }

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(31f - Clicker.instance.superCooldown_);

        myButton.interactable = cost <= score;
    }

    private void ShowSuperClick(float _value)
    {
        mySlider.value = _value;
    }
}
