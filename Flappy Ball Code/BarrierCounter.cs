using UnityEngine;
using UnityEngine.UI;

public class BarrierCounter : MonoBehaviour
{
	[SerializeField] private Text counterText;
	[SerializeField] private Text starsText;
	[SerializeField] private GameObject winScreen;
	[SerializeField] private PlayerBall myPlayer;
	[SerializeField] private int needToWin = 100;
	[SerializeField] private ParticleSystem counterParticle;
	[SerializeField] private AudioSource counterSound;
	[SerializeField] private ParticleSystem winParticle;
	private int count = 0;

	private int bestScore;

	private void Start()
	{
		bestScore = PlayerPrefs.GetInt("Stars", 0);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("barrier"))
		{
			count++;
			counterText.text = count.ToString();

			if (count % 10 == 0)
			{
				counterParticle.Play();
				counterSound.Play();
			}

			if (count > bestScore)
			{
				bestScore = count;
				PlayerPrefs.SetInt("Stars", bestScore);
				starsText.text = PlayerPrefs.GetInt("Stars", 0).ToString();
				winParticle.Play();
			}

			if (count % needToWin == 0)
			{
				Win();
			}
		}
	}

	private void Win()
	{
		Time.timeScale = 0f;
		winScreen.SetActive(true);
		myPlayer.SetGameFinish(true);
	}

	public void winScreenOf()
	{
		winScreen.SetActive(false);
	}
}