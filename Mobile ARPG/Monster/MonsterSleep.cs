using UnityEngine;

public class MonsterSleep : MonoBehaviour
{
	[SerializeField] private MonsterBehaviour myMonster;

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Monster awake!");
		myMonster.enabled = true;
		myMonster.SetTarget(other.transform);
		Destroy(this);
	}
}
