using UnityEngine;
using System;
using System.Linq;
using Core.Characters;

namespace Core.Actions
{

    [System.Serializable]
    public class ActionReference {

        public string actionID;
        public GameActionList actions;

        public GameAction sharedAction => GetSharedAction();

        public GameAction Instantiate(Character actor, Character target) {

            if(actions != null) {

                GameAction _action = actions.FindAction(actionID);

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
        private GameActionList _gameActions;

        [SerializeField]
        private ActionReference[] _actionReferences;

        public ActionReference[] references => _actionReferences;

        public ActionReference GetReference(string id) {

            return _actionReferences
                .First(a => a.actionID == id);

        }

        public ActionReference GetReference(int index) {

            return _actionReferences[index];

        }

        public bool Contains(ActionReference reference) {

            return _actionReferences.Contains(reference);

        }

    }
    
}

