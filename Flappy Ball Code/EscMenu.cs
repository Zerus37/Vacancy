using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
	[SerializeField] private GameObject myPopup;
	[SerializeField] private Image myImage;
	[SerializeField] private PlayerBall myPlayer;

	private bool isActive = false;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (myPlayer.GameOver)
				return;

			Switch();
		}
	}

	public void Switch()
	{
		isActive = !isActive;
		myPopup.SetActive(isActive);
		myImage.enabled = isActive;
		myPlayer.enabled = !isActive;

		Time.timeScale = 1f;
		if (isActive)
		{
			Time.timeScale = 0f;
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
