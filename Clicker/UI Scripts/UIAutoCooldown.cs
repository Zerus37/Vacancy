using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAutoCooldown : UIStat
{
    [SerializeField] private Slider mySlider;

    protected override void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        Clicker.OnAutoCooldownUpdate.AddListener(TextUpdate);
        Clicker.ShowAutoClick.AddListener(ShowAutoClick);

        Clicker.instance.autoPosition = transform.position;
    }

    protected override void StatUpdate(int score)
    {
        float cost = Clicker.StatToCost(31f - Clicker.instance.autoCooldown_);

        myButton.interactable = cost <= score;
    }

    private void ShowAutoClick(float _value)
	{
        mySlider.value = _value;
    }
}
