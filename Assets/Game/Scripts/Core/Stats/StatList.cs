using UnityEngine;
using NaughtyAttributes;

namespace Core.Stats {

    public class StatList : MonoBehaviour
    {
        
        // Resource stats
        [BoxGroup("Resource")] public Resource healthPoints;
        [BoxGroup("Resource")] public Resource actionPoints;

        // Base stats
        [BoxGroup("Base")] public Bravery bravery;
        [BoxGroup("Base")] public Constitution constitution;
        [BoxGroup("Base")] public Vitality vitality;
        [BoxGroup("Base")] public Determination determination;
        [BoxGroup("Base")] public Agility agility;

        private void Awake() {

            UpdateResource();

            healthPoints.value = healthPoints.max;
            actionPoints.value = actionPoints.max;

            vitality.onValueChanged += OnAttributeValueChanged;
            determination.onValueChanged += OnAttributeValueChanged;

        }

        private void OnValidate() {

            UpdateResource();

        }

        private void OnAttributeValueChanged(float value) {

            UpdateResource();
            
        }

        public void UpdateResource() {

            healthPoints.max = Mathf.RoundToInt(vitality.CalculateMaximumHealth());
            actionPoints.max = Mathf.RoundToInt(determination.CalculateMaxActionPoints());

        }

    }

}
 
