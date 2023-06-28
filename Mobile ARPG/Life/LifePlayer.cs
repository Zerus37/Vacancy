using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifePlayer : Life
{
	private static bool somePlayersAlive = true;
	public static bool SomePlayersAlive => somePlayersAlive;

	[SerializeField] private Slider hpBar;

	private void Start()
	{
		somePlayersAlive = true;
		viev();
	}

	protected override void viev()
	{
		hpBar.value = hp / hpMax;
	}

	protected override void Die()
	{
		somePlayersAlive = false;
		Invoke("Restart",1f);
	}

	private void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
