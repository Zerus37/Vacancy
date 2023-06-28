using UnityEngine;

public class AttackBase : MonoBehaviour
{
	[SerializeField] private Transform attackPoint;
	[SerializeField] private LayerMask layer;
	[SerializeField] private Vector3 scale;

	public void Attack()
	{
		//Collider[] targetsColliders = Physics.OverlapBox(attackPoint.position, attackPoint.localScale, attackPoint.rotation, int layerMask = AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
		Collider[] targetsColliders = Physics.OverlapBox(attackPoint.position, scale, attackPoint.rotation, layer);
		//Collider2D[] targetsColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

		foreach (Collider target in targetsColliders)
		{
			if (TryGetComponent<Life>(out Life targetLife))
			{
				targetLife.TakeDamage(1f);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
		//Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
		Gizmos.DrawWireCube(attackPoint.position, scale);
	}
}
