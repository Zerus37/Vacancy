using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISuperMulty : UIStat
{

    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnSuperMultyUpdate.AddListener(TextUpdate);
    }

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(Clicker.instance.superMulty_ * 2);

        myButton.interactable = cost <= score;
    }
}
