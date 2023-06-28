using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float startSpeed = 3f;
    [SerializeField] private float speedBoost = 0.001f;

    [SerializeField] private float minimalSpeed = 5f;

	[SerializeField] private float speedBonusThreshold = 8f;
	[SerializeField] private float scaleGrowthSpeed = 0.01f;

	[SerializeField] private GameObject[] bonusEffects;
	[SerializeField] private GameObject[] dethEffects;
	[SerializeField] private AudioSource jumpSound;
	[SerializeField] private AudioSource looseSound;
	[SerializeField] private AudioSource bashSound;

	[SerializeField] private PauseMenu pauseMenu;

	public bool mute = false;

	private bool gameOver = false;
	public bool GameOver => gameOver;
	private bool gameFinish = false;

	private int scaleCount = 0;
	private float scaleGravity = 1f;
	private float scaleJump = 1f;
	private float baseGravity;
	private float baseJumpSpeed;

	private bool speedBonus = false;

	private Vector3 predictTopZone = new Vector3(2,1,1);
	private Vector3 predictDownZone = new Vector3(2,-1,1);

	public bool TakeHit()
	{
		if (!speedBonus)
		{
			foreach (GameObject effect in dethEffects)
			{
				effect.SetActive(!effect.activeSelf);
			}

			GameStat.firstTry = false;

			Time.timeScale = 0f;
			gameOver = true;
			looseSound.Play();
			return false;
		}
		else
		{
			DropSpeedBonus();
			return true;
		}
	}

	public void SetGameOver(bool flag)
	{
		gameOver = flag;
	}

	public void SetGameFinish(bool flag)
	{
		gameFinish = flag;
	}

	private void Start()
	{
		if (_rb == null)
			_rb = GetComponent<Rigidbody2D>();

		_rb.velocity = new Vector2(startSpeed, 0f);
		baseGravity = _rb.gravityScale;
		baseJumpSpeed = jumpSpeed;
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			if (pauseMenu.pause)
			{
				pauseMenu.UnPause();
				return;
			}

			//if (gameOver && !mute)
			if (gameOver)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			if (gameFinish)
			{
				RefreshPosition();
				return;
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				return;
			}

			if (mute)
			{
				if(Input.GetMouseButtonDown(0))
					return;
			}

			_rb.AddForce(new Vector2(speedBoost, jumpSpeed), ForceMode2D.Impulse);
			jumpSound.Play();

			SpeedUp();
			PredictAndChit(transform.position, _rb.velocity);
		}
	}

	private void SpeedUp()
	{
		scaleCount++;
		scaleGravity += scaleGrowthSpeed;
		scaleJump += scaleGrowthSpeed / 2;
		RefreshGravityAndSpeed();

		//pauseMenu.AddGameSpeed();

		if (_rb.velocity.x >= speedBonusThreshold && !speedBonus)
		{
			GetSpeedBonus();
		}
	}

	private void GetSpeedBonus()
	{
		speedBonus = true;

		foreach (GameObject effect in bonusEffects)
		{
			effect.SetActive(true);
		}
	}

	private void DropSpeedBonus()
	{
		scaleCount /= 2;

		_rb.AddForce(new Vector2((speedBoost * -scaleCount) - 1f, 0f), ForceMode2D.Impulse);
		scaleGravity -= scaleGrowthSpeed * scaleCount;
		scaleJump -= scaleGrowthSpeed * scaleCount / 2;
		RefreshGravityAndSpeed();
		if (_rb.velocity.x <= speedBonusThreshold)
		{
			speedBonus = false;

			foreach (GameObject effect in bonusEffects)
			{
				effect.SetActive(false);
			}
		}

		if (_rb.velocity.x <= minimalSpeed)
		{
			_rb.velocity = new Vector2(minimalSpeed, _rb.velocity.y);
		}

		bashSound.Play();
	}

	private void RefreshGravityAndSpeed()
	{
		_rb.gravityScale = baseGravity * scaleGravity;
		jumpSpeed = baseJumpSpeed * scaleJump;
	}

	private void RefreshPosition()
	{
		TrailRenderer trail = GetComponent<TrailRenderer>();

		//trail.enabled = false;

		Time.timeScale = 1f;
		SetGameFinish(false);
		transform.position = Vector3.zero;
		GetComponent<BarrierCounter>().winScreenOf();

		trail.Clear();
		//trail.enabled = true;
	}

	private void PredictAndChit(Vector3 origin, Vector3 speed)
    {
		Vector3 predictPoint = origin + speed * 0.4f + Physics.gravity * 0.08f;

        Collider2D cllideWallTop = Physics2D.OverlapArea(predictPoint, predictPoint + predictTopZone);
		if (cllideWallTop != null)
		{
			_rb.AddForce(new Vector2(0f, -jumpSpeed/8), ForceMode2D.Impulse);
		}

		Collider2D cllideWallDown = Physics2D.OverlapArea(predictPoint, predictPoint + predictDownZone);
		if (cllideWallDown != null)
		{
			_rb.AddForce(new Vector2(0f, jumpSpeed/8), ForceMode2D.Impulse);
		}
	}

	void OnDrawGizmos()
	{
		Vector3 predictPoint = transform.position + new Vector3(_rb.velocity.x, _rb.velocity.y,0f) * 0.4f + Physics.gravity * 0.08f;

		Gizmos.color = Color.red;
		Gizmos.DrawCube(predictPoint + predictTopZone/ 2, predictTopZone);


		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(predictPoint + predictDownZone / 2, predictTopZone);
	}
}
