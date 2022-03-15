using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class DisplaySegments : MonoBehaviour
{
#if UNITY_EDITOR
	[MenuItem("GameObject/HUD/DisplaySegments")]
	static void CreateSegmentContainer(MenuCommand menuCommand)
	{
		GameObject SegmentContainer = new GameObject();
		SegmentContainer.name = "SegmentsBackground";
		SegmentContainer.AddComponent<Image>().color = Color.black;
		DisplaySegments segments = SegmentContainer.AddComponent<DisplaySegments>();

		RectTransform thisTransform = SegmentContainer.GetComponent<RectTransform>();

		thisTransform.anchorMin = new Vector2(0, 0.5f);
		thisTransform.anchorMax = new Vector2(0, 0.5f);

		thisTransform.pivot = new Vector2(0, 0.5f);

		GameObjectUtility.SetParentAndAlign(SegmentContainer, menuCommand.context as GameObject);

		segments.SegmentSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
		segments.UpdateValues();
	}
#endif
	[MinValue(0)]
	[OnValueChanged("UpdateValues")]
	public int _value = 3;
	[MinValue(1)]
	[OnValueChanged("UpdateValues")]
	public int MaxValue = 3;
	[MinValue(1)]
	[OnValueChanged("UpdateValues")]
	public int SubSegmentations = 1;
	[OnValueChanged("UpdateValues")]
	public Vector2 Size = new Vector2(40, 40);
	[OnValueChanged("UpdateValues")]
	public float separation = 2f;
	[OnValueChanged("UpdateValues")]
	public Sprite SegmentSprite;

	public List<GameObject> Segments { get; private set; } = new List<GameObject>();

	private RectTransform rectTransform; 
	void UpdateSegments()
	{
		if (!rectTransform)
			rectTransform = GetComponent<RectTransform>();

		if (MaxValue < SubSegmentations)
			SubSegmentations = MaxValue;

		if (MaxValue < 1 || SubSegmentations < 1)
        {
			MaxValue = 1;
			SubSegmentations = 1;
		}

		float _SN = (float)MaxValue / (float)SubSegmentations;
		int SegNum = (int)(_SN + _SN % 1);

		rectTransform.sizeDelta = new Vector2(Size.x * SegNum + separation * (SegNum-1), Size.y);

		for (int i = 0; i < Mathf.Max(SegNum, Segments.Count); i++)
		{
			if(i < SegNum)
			{
				if (Segments.Count <= i)
				{
					GameObject Seg = new GameObject();
					Seg.name = "SegmentsBackground";
					Seg.AddComponent<Image>().sprite = SegmentSprite;
					Seg.GetComponent<RectTransform>().SetParent(transform);

					//GameObjectUtility.SetParentAndAlign(Seg, gameObject);

					Segments.Add(Seg);
				}

				Image img = Segments[i].GetComponent<Image>();
				img.sprite = SegmentSprite;
				img.type = Image.Type.Filled;
				img.fillOrigin = 2;
				img.fillClockwise = false;

				//float newVal = 1 + (SubSegmentations * i - _value) / SubSegmentations;

				img.fillAmount = 1 + ((float)_value - (float)SubSegmentations * ((float)i+1f)) * (1 / (float)SubSegmentations);

				RectTransform thisTransform = Segments[i].GetComponent<RectTransform>();

				thisTransform.anchorMin = new Vector2(0, 0.5f);
				thisTransform.anchorMax = new Vector2(0, 0.5f);

				thisTransform.pivot = new Vector2(0, 0.5f);
				thisTransform.localPosition = new Vector2(Size.x * i + separation * i, 0);
				//thisTransform.offsetMin = Vector2.one * 50;
				thisTransform.sizeDelta = Size;
				thisTransform.localScale = Vector3.one;

			}
			else
			{
				Segments[i].SetActive(false);
				DestroyImmediate(Segments[i]);
			}
		}

		Segments.RemoveRange(SegNum, Segments.Count - SegNum);
	}

	private void Update()
	{
		UpdateValues();
	}

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		UpdateValues();
	}

	private void UpdateValues()
	{
		if (_value < 0)
			_value = 0;
		if (_value > MaxValue)
			_value = MaxValue;

		UpdateSegments();
	}

	public void SetValue(float value)
	{
		//Debug.Log(value);
		_value = (int)value;
		UpdateValues();
	}

	public void SetMaxValue(float value)
	{
		//Debug.Log(value);
		MaxValue = (int)value;
		UpdateValues();
	}
}
