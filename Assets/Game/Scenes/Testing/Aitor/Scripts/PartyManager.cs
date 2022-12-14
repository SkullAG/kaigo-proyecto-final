using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core.Characters;
using Core.Stats;

public class PartyManager : Singleton<PartyManager>
{

	[Layer] public string _leaderLayer;
	[Layer] public string _partyLayer;

	public System.Action<Character> characterSelected = delegate{}; // Leo
	public System.Action<Character> partyMemberAdded = delegate {};
	public System.Action<Character> partyMemberRemoved = delegate {};

	public NavBodyPuppeteer puppeteer;
	[SerializeField]
	[OnValueChanged("UpdatePartyInfo")]
	public List<NavBodySistem> PartyMembers = new List<NavBodySistem>();

	public int selectedCharacter { 
		
		get { return _selectedCharacter; } 
		
		set { 
				
			_selectedCharacter = value;
			partyInfoDrawer.setSelectedCharacter(_selectedCharacter);

			puppeteer.character = PartyMembers[_selectedCharacter];
			_camera.objective = PartyMembers[_selectedCharacter].transform;
			
			characterSelected(GetSelectedCharacter()); // Leo

			// Set selected as leader
			PartyMembers[_selectedCharacter].gameObject.layer = LayerMask.NameToLayer(_leaderLayer);

			// Set rest of party as party
			foreach(var c in PartyMembers) { 
				if(c != PartyMembers[_selectedCharacter]) {
					c.gameObject.layer = LayerMask.NameToLayer(_partyLayer);
				}
			 }

		} 
		
	}


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

		// Call character selection once at star
		selectedCharacter = _selectedCharacter;

		partyInfoDrawer.setSelectedCharacter(_selectedCharacter);

		puppeteer.character = PartyMembers[_selectedCharacter];
		_camera.objective = PartyMembers[_selectedCharacter].transform;

		foreach(NavBodySistem nbs in PartyMembers)
        {
			StatList sl = nbs.GetComponent<StatList>();

			sl.healthPoints.onMaxUpdated += reciveUpdate;
			sl.healthPoints.onValueUpdated += reciveUpdate;

			sl.actionPoints.onMaxUpdated += reciveUpdate;
			sl.actionPoints.onValueUpdated += reciveUpdate;
		}
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
		addPartyMember(character);

		UpdatePartyInfo();
	}
	public void removePartyMember(NavBodySistem character)
	{
		PartyMembers.Remove(character);
		removePartyMember(character);

		UpdatePartyInfo();
	}

	public List<NavBodySistem> getPartyMembers()
	{
		return PartyMembers;
	}

	void reciveUpdate(int ignore1, int ignore2)
    {
		UpdatePartyInfo();
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
