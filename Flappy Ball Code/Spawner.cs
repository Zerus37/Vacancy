using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float cooldown;
	[SerializeField] private float cooldownMinimal;
	[SerializeField] private float cooldownOffset = 0f;
	[SerializeField] private GameObject[] prefabs;

	[SerializeField] private PauseMenu pauseMenu;

	private void Start()
	{
		Spawn();
	}

	private void Spawn()
	{
		int i = Random.Range(0, prefabs.Length);
		Instantiate(prefabs[i], spawnPoint.position, Quaternion.identity);
		if(cooldown > cooldownMinimal)
			cooldown += cooldownOffset;
		Invoke(nameof(Spawn), cooldown);

		pauseMenu.AddGameSpeed();
	}
}