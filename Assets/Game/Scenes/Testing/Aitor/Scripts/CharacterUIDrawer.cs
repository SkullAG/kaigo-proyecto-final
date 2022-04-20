using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIDrawer : MonoBehaviour
{
	public TextMeshProUGUI characterName;
	public DisplayBar HPBar;
	public DisplayBar MPBar;

	public Color offColor = Color.black;
	public Color activeColor = Color.cyan;

	public bool selected { get { return _selected; } set { _selected = value; activationImage.color = value ? activeColor : offColor; } }
	bool _selected;

	public Image activationImage;

	public class CharacterDebugInfo
	{
		public int? health = null;
		public int? mana = null;
		public int? maxHealth = null;
		public int? maxMana = null;
		public string name = null;
	}

	public void UpdateValues(CharacterDebugInfo info)
	{
		if(info.name != null)
		{
			characterName.text = info.name;
		}

		HPBar._value = info.health.GetValueOrDefault((int)HPBar._value);
		HPBar.MaxValue = info.maxHealth.GetValueOrDefault((int)HPBar.MaxValue);
		MPBar._value = info.mana.GetValueOrDefault((int)MPBar._value);
		MPBar.MaxValue = info.maxMana.GetValueOrDefault((int)MPBar.MaxValue);
	}
}
