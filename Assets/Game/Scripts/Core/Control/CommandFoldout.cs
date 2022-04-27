using UnityEngine;
using UnityEngine.EventSystems;

public class CommandFoldout : Command
{

    public CommandList commandList;

    private bool _listDisplayed = false;

    public CommandFoldout(int id, CommandList commandList) : base(id) {

        this.commandList = commandList;

        // Default is inactive
        commandList.transform.parent.gameObject.SetActive(false);

    }

    public override void Execute() {

        // Toggle display of foldout command
        _listDisplayed = commandList.transform.parent.gameObject.activeInHierarchy;
        _listDisplayed = !_listDisplayed;

        commandList.transform.parent.gameObject.SetActive(_listDisplayed);
        commandList.commandInstanced += OnCommandInstantiation;

    }

    private void OnCommandInstantiation(int count) {
        
        if(_listDisplayed) {

            // Select first button
            commandList.SelectButton(0);

        }

        commandList.commandInstanced -= OnCommandInstantiation;

    }

}
