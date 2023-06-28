using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LifeMonster : Life
{
	public static bool showHpBar;

	[SerializeField] private LineRenderer hpBar;
	[SerializeField] private MonsterBehaviour myMonster;

	private void Start()
	{
		if (myMonster == null)
			myMonster = gameObject.GetComponent<MonsterBehaviour>();

		if (!showHpBar)
		{
			if (hpBar != null)
				hpBar.enabled = false;
			return;
		}

		if (hpBar == null)
			hpBar = gameObject.GetComponent<LineRenderer>();
	}

	protected override void viev()
	{
		if (!showHpBar)
			return;
		hpBar.startWidth = hp / hpMax;
	}

	protected override void Die()
	{
		myMonster.Die();
		Destroy(this);
	}
}
