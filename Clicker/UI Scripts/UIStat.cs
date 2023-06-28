using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStat : MonoBehaviour
{
    [SerializeField] protected Button myButton;
    [SerializeField] protected TextMeshProUGUI myText;
    [SerializeField] protected TextMeshProUGUI costText;
    [SerializeField] protected float upCost;

    [SerializeField] protected float maxValue;

    [SerializeField] protected bool reverseGrow = false;
    [SerializeField] protected bool bigCost = false;

    protected virtual void Start()
    {
        Clicker.OnScoreUpdate.AddListener(StatUpdate);
        //Clicker.OnPowerUpdate.AddListener(TextUpdate); // Подставить нужное событие
    }

    protected virtual void StatUpdate(int score)
    {
    }

    protected void TextUpdate(float stat)
    {
		if (stat == maxValue)
        {
            myButton.interactable = false;
            myText.text = stat.ToString() + " MAX";
            return;
        }

        myText.text = stat.ToString();

        if(reverseGrow)
            upCost = Clicker.StatToCost(31f - stat);
        else if(bigCost)
            upCost = Clicker.StatToCost(stat * 2);
        else
            upCost = Clicker.StatToCost(stat);

        costText.text = upCost.ToString();
    }
}
