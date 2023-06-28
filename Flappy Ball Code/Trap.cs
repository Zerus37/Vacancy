using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private bool destoyable = true;


	private void OnTriggerEnter2D(Collider2D other)
	{
		bool needEffect = other.GetComponent<PlayerBall>().TakeHit();
		if (needEffect)
		{
			Instantiate(destroyEffect, (transform.position + other.transform.position)/2, Quaternion.identity);

			if (destoyable)
			{
				Destroy(this.gameObject);
			}
		}
	}
}