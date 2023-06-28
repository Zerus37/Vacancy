using UnityEngine;

public class LocationStarter : MonoBehaviour
{
	[SerializeField] private bool showHpBars;

	public void TEST(bool showHpBar)
	{
		LifeMonster.showHpBar = showHpBars;
	}

    void Start()
	{
		LifeMonster.showHpBar = showHpBars;

		StopAllCoroutines();
		StartCoroutine(MonsterBehaviour.ActionNext());
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
		MonsterBehaviour.RefreshQueue();
	}
}
