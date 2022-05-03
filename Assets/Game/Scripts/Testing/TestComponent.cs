using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TestComponent : MonoBehaviour
{

    private SimpleFactory<TestBaseClass> _factory = new SimpleFactory<TestBaseClass>(

        new System.Type[] {

            typeof(TestDerivedClass),
            typeof(TestDerivedClass2)

        }

    );

    [SerializeReference]
    private List<TestBaseClass> _list = new List<TestBaseClass>();

    [Dropdown("names"), SerializeField]
    private string _selectedType;

    private string[] names => _factory.GetNames();

    [Button]
    private void Add() {

        var _obj = _factory.CreateInstance(_selectedType);
        _list.Add(_obj);

    }

}
