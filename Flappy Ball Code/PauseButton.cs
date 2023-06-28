using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PlayerBall myPlayer;

	void OnMouseOver()
    {
        myPlayer.mute = true;
    }

    void OnMouseExit()
    {
        myPlayer.mute = false;
    }
}
