using UnityEngine;
using UnityEngine.EventSystems;

public class CommandFoldout : Command
{

    private CommandController.SubcommandType _subcommandType;
    private CommandList _list;
    private CommandController _controller;

    private bool _listDisplayed = false;

    public CommandFoldout(int id, CommandController.SubcommandType subcommandType) : base(id) {

        _subcommandType = subcommandType;

        _controller = CommandController.current;
        _list = _controller.GetSubcommandList(_subcommandType);

        // Default is subcommand disabled
        _controller.SetSubcommandsEnabled(_subcommandType, false);

    }

    public override void Execute() {

        _listDisplayed = _list.transform.parent.gameObject.activeInHierarchy;
        _listDisplayed = !_listDisplayed;

        // Toggle display of foldout command
        _controller.SetSubcommandsEnabled(_subcommandType, _listDisplayed);

        _list.commandInstanced += OnCommandInstantiation;

    }

    private void OnCommandInstantiation(int count) {
        
        if(_listDisplayed) {

            // Select first button
            _list.SelectButton(0);

        }

        _list.commandInstanced -= OnCommandInstantiation;

    }

}
