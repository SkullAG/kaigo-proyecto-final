using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Gambits;

[CreateAssetMenu(fileName = "Gambit Set", menuName = "Game/AI/Gambit Set")]
public class GambitSet : ScriptableObject
{

    [SerializeField]
    private List<Gambit> _gambits = new List<Gambit>();
    
    public List<Gambit> list => _gambits;

    public void ShiftGambit(int shift, Gambit gambit) {

        if( _gambits.Contains(gambit) ) {

                int _currentIndex = _gambits.IndexOf(gambit);
                int _nextIndex = Mathf.Clamp(_currentIndex + shift, 0, _gambits.Count - 1);

                //Debug.Log("Swapping gambit " + _currentIndex + " with " + _nextIndex);

                var _temp = _gambits[_nextIndex];

                _gambits[_nextIndex] = _gambits[_currentIndex];
                _gambits[_currentIndex] = _temp;

            }

        }

    public void AddGambit(Gambit gambit) {

        _gambits.Add(gambit);

    }

    public void RemoveGambit(Gambit gambit) {

        _gambits.Remove(gambit);

    }

}
