using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DisplayBar : MonoBehaviour
{
#if UNITY_EDITOR
	[MenuItem("GameObject/HUD/DisplayBar")]
	static void CreateBar(MenuCommand menuCommand)
	{
		GameObject barBackground = new GameObject();
		barBackground.name = "BarBackground";
		barBackground.AddComponent<Image>().color = Color.black;
		DisplayBar thisBar = barBackground.AddComponent<DisplayBar>();
		barBackground.GetComponent<RectTransform>().offsetMax = new Vector2(50, -30);

		GameObjectUtility.SetParentAndAlign(barBackground, menuCommand.context as GameObject);

		//Crea el Contenedor
		GameObject BarContainer = new GameObject();
		BarContainer.name = "Container";
		BarContainer.AddComponent<RectMask2D>();
		thisBar.containerTransform = BarContainer.GetComponent<RectTransform>();
		thisBar.containerTransform.SetParent(thisBar.transform);

		thisBar.containerTransform.anchorMin = new Vector2(0, 0);
		thisBar.containerTransform.anchorMax = new Vector2(1, 1);

		thisBar.containerTransform.offsetMin = new Vector2(4, 4);
		thisBar.containerTransform.offsetMax = new Vector2(0, 0);

		thisBar.containerTransform.localPosition = Vector3.zero;
		thisBar.containerTransform.localScale = Vector3.one;

		//Crea barra interior
		GameObject Bar = new GameObject();
		Bar.name = "Bar";
		Bar.AddComponent<Image>();
		Bar.AddComponent<RectMask2D>();
		thisBar.barTransform = Bar.GetComponent<RectTransform>();
		thisBar.barTransform.SetParent(thisBar.containerTransform);

		thisBar.barTransform.pivot = new Vector2(0.5f, 0.5f);
		thisBar.barTransform.anchorMin = new Vector2(0, 0);
		thisBar.barTransform.anchorMax = new Vector2(1, 1);

		thisBar.barTransform.offsetMin = new Vector2(0, 0);
		thisBar.barTransform.offsetMax = new Vector2(0, 0);

		thisBar.barTransform.localPosition = Vector3.zero;
		thisBar.barTransform.localScale = Vector3.one;

		//Crea un pivot para lo que tenga la barra dentro, no es necesario pero mejor prevenir
		GameObject contents = new GameObject();
		contents.name = "ContentPivot";
        thisBar.insideOfBarTransform = contents.AddComponent<RectTransform>(); ;
		thisBar.insideOfBarTransform.SetParent(thisBar.barTransform);

		thisBar.insideOfBarTransform.pivot = new Vector2(0.5f, 0.5f);
		thisBar.insideOfBarTransform.anchorMin = new Vector2(0.5f, 0.5f);
		thisBar.insideOfBarTransform.anchorMax = new Vector2(0.5f, 0.5f);

		thisBar.insideOfBarTransform.offsetMin = new Vector2(0, 0);
		thisBar.insideOfBarTransform.offsetMax = new Vector2(0, 0);

		thisBar.insideOfBarTransform.localPosition = Vector3.zero;
		thisBar.insideOfBarTransform.localScale = Vector3.one;


		thisBar.UpdateValues();
	}
#endif
	[OnValueChanged("UpdateValues")]
	public bool invertValue = false;
	[OnValueChanged("UpdateValues")]
	public float _value = 100;
	[MinValue(0)]
	[OnValueChanged("UpdateValues")]
	public float MaxValue = 100;

	[Range(0, 360)]
	[OnValueChanged("UpdateValues")]
	public int angle = 0;
	[OnValueChanged("UpdateValues")]
	public RectTransform barTransform;
	[OnValueChanged("UpdateValues")]
	public RectTransform containerTransform;
	[OnValueChanged("UpdateValues")]
	public RectTransform insideOfBarTransform;

	private void Start()
    {
		if(!containerTransform)
        {
			GameObject BarContainer = new GameObject();
			BarContainer.name = "Container";
			BarContainer.AddComponent<RectMask2D>();
			containerTransform = BarContainer.GetComponent<RectTransform>();
			containerTransform.SetParent(transform);

			//containerTransform.pivot = new Vector2(1, 0.5f);
			containerTransform.anchorMin = new Vector2(0, 0);
			containerTransform.anchorMax = new Vector2(1, 1);

			containerTransform.offsetMin = new Vector2(4, 4);
			containerTransform.offsetMax = new Vector2(0, 0);

			containerTransform.localPosition = Vector3.zero;
			containerTransform.localScale = Vector3.one;
        }



		if (!barTransform)
		{
			GameObject Bar = new GameObject();
			Bar.name = "Bar";
			Bar.AddComponent<Image>();
			barTransform = Bar.GetComponent<RectTransform>();
			barTransform.SetParent(containerTransform);

			barTransform.pivot = new Vector2(0.5f, 0.5f);
			barTransform.anchorMin = new Vector2(0, 0);
			barTransform.anchorMax = new Vector2(1, 1);

			barTransform.offsetMin = new Vector2(0, 0);
			barTransform.offsetMax = new Vector2(0, 0);

			barTransform.localPosition = Vector3.zero;
			barTransform.localScale = Vector3.one;
		}

		UpdateValues();
	}

	private void UpdateValues()
    {
		if (_value < 0)
			_value = 0;
		if (_value > MaxValue)
			_value = MaxValue;
		
		if(invertValue)
        {
			barTransform.localPosition = new Vector2(-containerTransform.rect.width + containerTransform.rect.width * ((MaxValue - _value) / MaxValue), -containerTransform.rect.height + containerTransform.rect.height * ((MaxValue - _value) / MaxValue)) * new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
		}
        else
        {
			barTransform.localPosition = new Vector2(-containerTransform.rect.width + containerTransform.rect.width * (_value / MaxValue), -containerTransform.rect.height + containerTransform.rect.height * (_value / MaxValue)) * new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
		}

		if (insideOfBarTransform)
		{
			insideOfBarTransform.localPosition = -barTransform.localPosition;
		}
	}

    public void SetValue(float value)
	{
		//Debug.Log(value);
		_value = value;
		UpdateValues();
	}

	public void SetMaxValue(float value)
	{
		//Debug.Log(value);
		MaxValue = value;
		UpdateValues();
	}
}
