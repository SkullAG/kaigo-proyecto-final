using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core.Characters;

public class PartyManager : Singleton<PartyManager>
{
	public NavBodyPuppeteer puppeteer;
	[SerializeField]
	[OnValueChanged("UpdatePartyInfo")]
	public List<NavBodySistem> PartyMembers = new List<NavBodySistem>();
	public int selectedCharacter { get { return _selectedCharacter; } set 
		{ 
			_selectedCharacter = value;
			partyInfoDrawer.setSelectedCharacter(_selectedCharacter);
			puppeteer.character = PartyMembers[_selectedCharacter];
			_camera.objective = PartyMembers[_selectedCharacter].transform;
		} }
	int _selectedCharacter = 0;
	public PartyUIDrawer partyInfoDrawer;
	public CameraManager _camera;

	// Start is called before the first frame update
	void Start()
	{
		//_input = InputSingleton.current.input;
		//_changeCharacterInput = _input.actions.FindAction("ChangeCharacter");
		InputSingleton.current.input.actions.FindAction("ChangeCharacter").started += selectedCharacterPlus1;

		UpdatePartyInfo();
		partyInfoDrawer.setSelectedCharacter(_selectedCharacter);

		puppeteer.character = PartyMembers[_selectedCharacter];
		_camera.objective = PartyMembers[_selectedCharacter].transform;
	}

    //private void OnEnable()
    //{
	//	Debug.Log(InputSingleton.current);
	//	InputSingleton.current.input.actions.FindAction("ChangeCharacter").performed += selectedCharacterPlus1;
	//}
	//private void OnDisable()
	//{
	//	InputSingleton.current.input.actions.FindAction("ChangeCharacter").performed -= selectedCharacterPlus1;
	//}

	public void addPartyMember(NavBodySistem character)
	{
		PartyMembers.Add(character);

		UpdatePartyInfo();
	}
	public void removePartyMember(NavBodySistem character)
	{
		PartyMembers.Remove(character);

		UpdatePartyInfo();
	}

	public List<NavBodySistem> getPartyMembers()
	{
		return PartyMembers;
	}

	void UpdatePartyInfo()
	{
		partyInfoDrawer.UpdateAllValues(PartyMembers);
	}

	void selectedCharacterPlus1(InputAction.CallbackContext context)
    {
		if (selectedCharacter + 1 >= PartyMembers.Count)
		{
			selectedCharacter = 0;
		}
		else
		{
			selectedCharacter++;
		}
	}

	public Character GetSelectedCharacter() {

		return PartyMembers[selectedCharacter].GetComponent<Character>();

	}
}
