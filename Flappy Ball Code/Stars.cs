using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    [SerializeField] private Text myText;
    void Start()
    {
        myText.text = PlayerPrefs.GetInt("Stars", 0).ToString();
    }
}
