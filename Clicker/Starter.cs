using UnityEngine;

public class Starter : MonoBehaviour
{
	private void LateUpdate()
	{
		Clicker.instance.UpdateAllStats();
		Destroy(this.gameObject);
	}
}
