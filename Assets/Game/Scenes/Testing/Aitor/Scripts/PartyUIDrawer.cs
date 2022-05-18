using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Characters;

public class PartyUIDrawer : MonoBehaviour
{
	public List<CharacterUIDrawer> characterUI;

	public static PartyUIDrawer current;

	// Start is called before the first frame update
	void Start()
	{
		characterUI = GetComponentsInChildren<CharacterUIDrawer>().ToList();
		current = this;
	}

	// Update is called once per frame
	public void UpdateAllValues(List<NavBodySistem> party)
	{
		for(int i = 0; i < characterUI.Count; i++)
		{
			if(i < party.Count)
			{
				activateCharacterSlot(i);

				CharacterUIDrawer.CharacterDebugInfo info = new CharacterUIDrawer.CharacterDebugInfo();

				Character ch = party[i].GetComponent<Character>();

				info.name = party[i].name;

				if(ch)
                {
					info.health = ch.stats.healthPoints.value;
					info.maxHealth = ch.stats.healthPoints.max;
					info.mana = ch.stats.actionPoints.value;
					info.maxMana = ch.stats.actionPoints.max;
				}

				UpdateSingleCharacter(i, info);
			}
			else
			{
				deactivateCharacterSlot(i);
			}
		}
	}

	public void setSelectedCharacter(int index)
	{
		for (int i = 0; i < characterUI.Count; i++)
		{
			characterUI[i].selected = index == i;
		}
	}

	void deactivateCharacterSlot(int index)
	{
		characterUI[index].gameObject.SetActive(false);
	}

	void activateCharacterSlot(int index)
	{
		characterUI[index].gameObject.SetActive(true);
	}

	public void UpdateSingleCharacter(int index, CharacterUIDrawer.CharacterDebugInfo info)
	{
		characterUI[index].UpdateValues(info);
	}
}
