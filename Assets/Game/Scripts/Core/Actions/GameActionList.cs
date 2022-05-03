using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System.Linq;

namespace Core.Actions
{

    [CreateAssetMenu(fileName = "Game Actions", menuName = "Game/Settings/Game Actions")]
    public class GameActionList : ScriptableObject
    {
        
        SimpleFactory<GameAction> _factory = new SimpleFactory<GameAction>(

            new System.Type[] {

                // List of types of actions that can be created
                typeof(BattleAction),
                typeof(ItemAction)

            }

        );

        [SerializeReference]
        public List<GameAction> actions = new List<GameAction>();

        [Dropdown("names"), SerializeField]
        private string _actionType;

        private string[] names => _factory.GetNames();

        [Button]
        private void Add() {

            actions.Add(_factory.CreateInstance(_actionType));

        }

        public GameAction FindAction(string id) {

            return actions.FirstOrDefault(x => x.id == id);

        }

    }
    
}

