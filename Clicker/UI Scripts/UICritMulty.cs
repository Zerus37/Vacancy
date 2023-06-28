using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICritMulty : UIStat
{

    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnCritMultyUpdate.AddListener(TextUpdate);
    }

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(Clicker.instance.critMulty_ * 2);

        myButton.interactable = cost <= score;
    }
}
