using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPower : UIStat
{
    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnPowerUpdate.AddListener(TextUpdate);
	}

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(Clicker.instance.power_);

        myButton.interactable = cost <= score;
    }
}
