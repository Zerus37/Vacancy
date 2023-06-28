using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float xOffset = 0f;
	private float _z, _y;

	private void Start()
	{
		_z = transform.position.z;
		_y = transform.position.y;
	}

	void Update()
	{
		transform.position = new Vector3(target.position.x + xOffset, _y, _z);
	}
}