using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleLog : Singleton<BattleLog>
{
    
    [SerializeField] private RectTransform _container;
    [SerializeField] private int _maxLogs;
    [SerializeField] private string _testText;
    [SerializeField] private string _format = "<b>·</b> {0}";
    [SerializeField] private float _timeToClose = 5;

    private List<string> _logs = new List<string>();
    private float _timer;
    private bool _enabled;

    private ScrollRect _scrollRect;
    private TextMeshProUGUI _textMesh;

    private void Awake() {

        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _scrollRect = GetComponentInChildren<ScrollRect>();

    }

    public override void OnEnable() {

        base.OnEnable();

        _container.gameObject.SetActive(false);
        _enabled = false;

    }

    private void Update() {

        if(_enabled) {

            _timer -= Time.deltaTime;

            if(_timer < 0) {

                _timer = 0; // Reset to 0 because why nah gg
                _container.gameObject.SetActive(false);
                _enabled = false;

            }

        }

    }

    public void WriteLine(string text) {

        _container.gameObject.SetActive(true);
        _enabled = true;
        _timer = _timeToClose; 

        _logs.Add(string.Format(_format, text));

        UpdateText();

    }

    private void UpdateText() {

        if(_logs.Count > _maxLogs) {
            _logs.RemoveAt(0);
        }
        
        _textMesh.text = string.Join("<br>", _logs);

    }

    [Button]
    private void Test() {

        WriteLine(_testText);

    }

}
