using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController charController;
	[SerializeField] private Transform charModel;
	public float speed = 3.0f;

	private void Start()
	{
		if (charController == null)
			charController = this.gameObject.GetComponent<CharacterController>();
	}

	public void Move(float Horizontal, float Vertical)
    {
		Vector3 moveVector = new Vector3(Horizontal, 0f, Vertical);//.normalized Нормализация происходит в class Joystick
		charController.SimpleMove(moveVector * speed);
		//charModel.LookAt(moveVector);
		//RotateTowards(charModel.rotation, moveVector);

		Vector3 newDirection = Vector3.RotateTowards(charModel.forward, moveVector, 1f, 0.0f);
		charModel.rotation = Quaternion.LookRotation(newDirection);
		//Debug.Log(moveVector);
	}
}
