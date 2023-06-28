using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumbersUI : MonoBehaviour
{
	public static NumbersUI Instance { get; private set; }

	private class ActiveText{
		public TextMeshProUGUI UIText;
		public float maxTime;
		public float currentTime;
		public float delta = 0f;
		public Vector3 startPosition;

		public void MoveText()
		{
			delta = 1.0f - (currentTime / maxTime);
			Vector3 pos = startPosition + new Vector3(delta*64, delta*64, 0f);

			UIText.transform.position = pos;
		}
	}

	[SerializeField] private TextMeshProUGUI textPrefab;

	const int POOL_SIZE = 64;
	private Queue<TextMeshProUGUI> textPool = new Queue<TextMeshProUGUI>();
	private List<ActiveText> activeTexts = new List<ActiveText>();

	private Transform myTransform;

	private void Start()
	{
		Instance = this;
		myTransform = transform;

		for (int i = 0; i < POOL_SIZE; i++)
		{
			TextMeshProUGUI temp = Instantiate(textPrefab, myTransform);
			temp.gameObject.SetActive(false);
			textPool.Enqueue(temp);
		}
	}

	private void Update()
	{
		for (int i = 0; i < activeTexts.Count; i++)
		{
			ActiveText at = activeTexts[i];
			at.currentTime -= Time.deltaTime;

			if (at.currentTime < 0.0f)
			{
				at.UIText.gameObject.SetActive(false);
				textPool.Enqueue(at.UIText);
				activeTexts.RemoveAt(i);
				--i;
			}
			else
			{
				var color = at.UIText.color;
				color.a = 1f - at.delta;
				at.UIText.color = color;

				at.MoveText();
			}
		}
	}

	public void AddText(int amount, Vector3 pos)
	{
		var t = textPool.Dequeue();
		t.text = amount.ToString();
		t.gameObject.SetActive(true);

		ActiveText at = new ActiveText() { maxTime = 1.0f };
		at.currentTime = at.maxTime;
		at.UIText = t;
		at.startPosition = pos;

		at.MoveText();
		activeTexts.Add(at);
	}
}
