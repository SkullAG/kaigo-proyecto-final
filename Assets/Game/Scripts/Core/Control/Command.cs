using UnityEngine;

public abstract class Command : MonoBehaviour
{
    
    [SerializeField] private string _text;

    [SerializeField, TextArea] private string _description;

    public string text => _text;
    public string description => _description;

    public abstract void Execute();

}
