using UnityEngine;

public class Life : MonoBehaviour
{
	[SerializeField] protected float hpMax;
	[SerializeField] protected float hp;

	public void TakeDamage(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			Die();
			return;
		}
		viev();
	}
	
	protected virtual void viev()
	{
	}

	protected virtual void Die()
	{
		Destroy(this.gameObject);
	}
}
