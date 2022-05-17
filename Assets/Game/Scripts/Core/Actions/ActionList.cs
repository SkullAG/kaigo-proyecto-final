using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Core.Characters;

using NaughtyAttributes;

namespace Core.Actions
{

    [System.Serializable]
    public class ActionReference {
        
        public GameActionList actions;

        [Dropdown("ids"), ShowIf("actionsDefined")]
        public string actionID;
        
        private string[] ids => actions?.IDs;

        private bool actionsDefined => actions != null;

        public GameAction sharedAction => GetSharedAction();

        public GameAction Instantiate(Character actor, Character target) {

            if(actions != null) {

                GameAction _action = actions.FindAction(actionID);

                if(_action == null) {

                    Debug.LogError("Couldn't find action with ID " + actionID + " in " + actions.name);

                    return null;

                }

                _action.actor = actor;
                _action.target = target;

                return _action.Copy();

            } 

            return null;

        }

        public GameAction GetSharedAction() {

            return actions?.FindAction(actionID);

        }

    }

    public class ActionList : MonoBehaviour
    {

        [SerializeField]
        private ActionReference[] _actionReferences;

        [Space(15)]

        [SerializeField]
        private GameActionList _addFromList;

        public ActionReference[] references => _actionReferences;

        [Button]
        private void Load() {

            List<ActionReference> _list = _actionReferences.ToList();

            foreach (var item in _addFromList.actions) {

                _list.Add( new ActionReference() { actions = _addFromList,  actionID = item.id } );
                
            }

            _actionReferences = _actionReferences.Union(_list).ToArray();

        }

        public ActionReference GetReference(string id) {

            return _actionReferences
                .First(a => a.actionID == id);

        }

        public ActionReference GetReference(int index) {

            return _actionReferences[index];

        }

        public bool Contains(ActionReference reference) {

            // Identify by ID, not by reference of object lul
            return _actionReferences.Any(r => r.actionID == reference.actionID);

        }

    }
    
}

