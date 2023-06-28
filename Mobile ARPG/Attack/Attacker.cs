using UnityEngine;

public class Attacker : MonoBehaviour
{
	[SerializeField] private Transform attackPoint;
	[SerializeField] private GameObject attackVisualPrefab;
	
	[SerializeField] private LayerMask layer;
	[SerializeField] private float damage;
	[SerializeField] private Vector3 scale;

	public void Attack()
	{
		int rndPlusMinusOne = Random.Range(0, 2) * 2 - 1;
		Instantiate(attackVisualPrefab, attackPoint.position - attackPoint.right * rndPlusMinusOne, attackPoint.rotation).transform.localScale = new Vector3(rndPlusMinusOne, 1, 1);
		
		Collider[] targetsColliders = Physics.OverlapBox(attackPoint.position, scale / 2, attackPoint.rotation, layer);
		foreach (Collider target in targetsColliders)
		{
			if (target.gameObject.TryGetComponent<Life>(out Life targetLife))
			{
				targetLife.TakeDamage(damage);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(attackPoint.position, scale);
	}
}
