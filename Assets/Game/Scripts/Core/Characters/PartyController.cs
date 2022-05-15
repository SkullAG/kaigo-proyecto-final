using System.Linq;
using UnityEngine;
using Core.Characters;
using UnityEngine.InputSystem;

public class PartyController : Singleton<PartyController>
{
    
    public float distanceToFollow = 1.5f;
    public bool lookAtLeaderOnStandby = true;
    public float timeToStandby = 5;

    [SerializeField]
    private InputActionReference _escapeInputAction;

    private float _standbyTimer = 0;
    private bool _inAction = false;
    private bool _gambitsDisabled = false;

    private PartyManager _manager;
    private Character[] _party;
    private Character[] _noLeaderParty;
    private Character _leader;

    private bool _busy = false;

    private void Start() {

        _manager = PartyManager.current;

        _manager.partyMemberAdded += OnPartyUpdate;
        _manager.partyMemberRemoved += OnPartyUpdate;
        _manager.characterSelected += OnPartyUpdate;

        OnPartyUpdate(null);

    }

    private void OnDisable() {

        _manager.partyMemberAdded -= OnPartyUpdate;
        _manager.partyMemberRemoved -= OnPartyUpdate;
        _manager.characterSelected -= OnPartyUpdate;

    }

    private void OnPartyUpdate(Character partyMember) {

        // Get all characters in party
        _party = _manager.PartyMembers.Select( x => x.GetComponent<Character>() ).ToArray();

        _leader = _manager.GetSelectedCharacter();

        _noLeaderParty = _party.Where(x => x != _leader).ToArray();

    }

    private void Update() {

        if(_escapeInputAction.action.ReadValue<float>() > InputSystem.settings.defaultButtonPressPoint) {

            Debug.Log("Escaping!");

            // Force party to not be in action
            _inAction = false;

        } else {

            // Check party's state if not escaping
            _inAction = IsPartyInAction();

        }

        if(!_inAction) {

            // Disable gambits when not in action
            if(!_gambitsDisabled) {

                for (int i = 0; i < _noLeaderParty.Length; i++) {
                    _noLeaderParty[i].gambits.SetEnabled(false);
                }

                _gambitsDisabled = true;

            }

            // Follow leader in row
            for (int i = 0; i < _noLeaderParty.Length; i++) {

                Character _member = _noLeaderParty[i];
                Character _target = i == 0 ? _leader : _noLeaderParty[i - 1];

                float _distance = Vector3.Distance(_target.transform.position, _member.transform.position);

                if(_distance > distanceToFollow) {

                    // Move
                    _member.navBody.ObjectivePoint = _target.transform.position;

                    if(lookAtLeaderOnStandby) _standbyTimer = 0;

                } else if ( _distance < distanceToFollow * 0.75f ) {

                    // Stop
                    _member.navBody.Objective = null;
                    _member.navBody.ObjectivePoint = _member.transform.position;

                    if(lookAtLeaderOnStandby) {

                        if(_standbyTimer > timeToStandby) {

                            // Look at leader on standby
                            Vector3 _dir = (_leader.transform.position - _member.transform.position);
                            float _angle = _member.navBody.RotateTowards(_dir);

                        } else {

                            _standbyTimer += Time.deltaTime;

                        }

                    }

                }
 
            }

        } else {

            Debug.Log("Party in action!");

            // Enable gambits when in action
            if(_gambitsDisabled) {

                for (int i = 0; i < _noLeaderParty.Length; i++) {
                    _noLeaderParty[i].gambits.SetEnabled(true);
                }

                _gambitsDisabled = false;

            }

        }

    }

    public bool IsPartyInAction() {

        foreach (Character c in _party) {

            // If any character from party is performing action
            // or is being targetted, party is in action mode.

            if( c.queue.isPerformingAction || c.isBeingTargetted ) {

                return true;

            }
            
        }

        return false;

    }

}
