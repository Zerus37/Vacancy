using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] private Text myText;

    void Start()
    {
        Clicker.OnScoreUpdate.AddListener(ScoreUpdate);
    }

    private void ScoreUpdate(int score)
	{
        myText.text = score.ToString();
    }
}
