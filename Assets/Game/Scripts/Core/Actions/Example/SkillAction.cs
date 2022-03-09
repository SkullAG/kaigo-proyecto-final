using UnityEngine;

namespace Core.Actions.Example
{

    [CreateAssetMenu(fileName = "SkillAction", menuName = "Game/Actions/Example/Skill")]
    public class SkillAction : GameAction {

        [SerializeField]
        private int damage;

        // Reset parameters
        public override void Initialize() {

            //

        }

        public override void Execute() {

            // Add action specific stuff here
            base.StartSequence();

        }

    }
    
}

