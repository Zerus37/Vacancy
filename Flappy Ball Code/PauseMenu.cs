using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject menuObj;
	[SerializeField] private float baseGameSpeed = 0.5f;
	[SerializeField] private float gameSpeedBonus = 0.05f;
	public bool pause = true;

	private void Start()
	{
		GameStat.gameSpeed = baseGameSpeed;
		if (GameStat.firstTry)
		{
			SetPause();
		}
		else
		{
			UnPause();
		}
	}

	public void SetPause()
	{
		pause = true;
		Time.timeScale = 0f;
		menuObj.SetActive(true);
	}

	public void UnPause()
	{
		pause = false;
		Time.timeScale = GameStat.gameSpeed;
		menuObj.SetActive(false);
	}

	public void AddGameSpeed()
	{
		if(GameStat.gameSpeed < 1)
		{
			GameStat.gameSpeed += gameSpeedBonus;
			Time.timeScale = GameStat.gameSpeed;
		}
		else
		{
			GameStat.gameSpeed = 1f;
		}
	}

}
