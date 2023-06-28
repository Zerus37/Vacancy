using UnityEngine;
using UnityEngine.UI;

public class SoundValueUI : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
	[SerializeField] private AudioSource[] audioSrc;
    private float[] baseVolumes;
    private float volume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume", 1);

		baseVolumes = new float[audioSrc.Length];
        for (int i = 0; i < audioSrc.Length; i++)
        {
            baseVolumes[i] = audioSrc[i].volume;
        }

		mySlider.value = volume;
    }

    public void SetVolume(float vol)
	{
        PlayerPrefs.SetFloat("SoundVolume", vol);
        volume = vol;

        for (int i = 0; i < audioSrc.Length; i++)
        {
            audioSrc[i].volume = baseVolumes[i] * volume;
        }
    }

}
