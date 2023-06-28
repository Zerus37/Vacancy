using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICritChance : UIStat
{

    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnCritChanceUpdate.AddListener(TextUpdate);
    }

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(Clicker.instance.critChance_);

        myButton.interactable = cost <= score;
    }
}
