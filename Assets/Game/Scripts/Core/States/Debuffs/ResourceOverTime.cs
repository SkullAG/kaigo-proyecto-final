using UnityEngine;
using Core.States;
using Core.Characters;

[CreateAssetMenu(fileName = "Resource Over Time Effect", menuName = "Game/States/Effects/Resource Over Time")]
public class ResourceOverTime : Effect
{

    public enum ResourceType { HP, AP }

    public ResourceType affectedResource;

    public float timeBetweenTicks = 1;
    public int valuePerTick = 1;

    private float _timer;

    private void OnEnable() {

        _timer = 0;

    }

    public override void Apply(Character actor) {
        
        _timer += Time.deltaTime; 

        if( _timer >= timeBetweenTicks ) {

            _timer = 0;
            
            switch(affectedResource) {

                case ResourceType.HP:
                    actor.stats.healthPoints.value += valuePerTick;
                    break;
                
                case ResourceType.AP:
                    actor.stats.actionPoints.value += valuePerTick;
                    break;

            }

        }

    }

}
