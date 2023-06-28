using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private float damage = 10f;

	private void OnTriggerEnter(Collider other)
	{
		if(other.isTrigger)
			return;

		Life victim = other.GetComponent<Life>();
		if (victim != null)
		{
			victim.TakeDamage(damage);
		}
	}
}
