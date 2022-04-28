using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Core.Characters;
using System.Linq;

public class Inventory : MonoBehaviour
{    
	public Transform _capsule;
	bool notEmpty = false;
	public Targetter _target;
	private int value;
	public int coins;

	public System.Action effectApplied = delegate {};

	public UnityEvent<Dictionary<string, Casilla>, List<string>> SendOnInventoryChange;
	public void boton()    
	{        
		_capsule.gameObject.SetActive(true);
	}

	[Serializable]
	public class Casilla
	{
		public Objects objeto;
		public int stack;

		public Casilla(Objects o)
		{
			stack = 1;
			objeto = o;
		}

	};

	public Dictionary<string, Casilla> huecos { get; private set; } = new Dictionary<string, Casilla>();

	[SerializeField]
	public List<string> nombres = new List<string>();

	// Añadido: devuelve array de todos los objetos en el inventario
	public Objects[] items => huecos.Values.Select(x => x.objeto).ToArray();

	private void OnEnable()
	{
		SendOnInventoryChange.Invoke(huecos, nombres);
	}

	public void OnTargetConfirmed(Character target) 
	{
		Debug.Log("Aplicando efecto");
		ApplyEffect(value);
		_target.targetConfirmed -= OnTargetConfirmed;
		_target.targetCancelled -= OnTargetCancelled;
	}

	public void OnTargetCancelled(Character target) {

		_target.targetConfirmed -= OnTargetConfirmed;
		_target.targetCancelled -= OnTargetCancelled;

	}

	public void Use(int value)
	{
		if (notEmpty)
		{
			_target.Enable();
			_target.targetConfirmed += OnTargetConfirmed;
			_target.targetCancelled += OnTargetCancelled;

			this.value = value;
		}      
	}

	public void ApplyEffect(int value)
	{



		string _name = nombres[value];

		huecos[_name].objeto.Use(_target.currentTarget);
		huecos[_name].stack--;

		SendOnInventoryChange.Invoke(huecos, nombres);

		if (huecos[nombres[value]].stack <= 0)
		{
			huecos.Remove(nombres[value]);
			nombres.RemoveAt(value);
			SendOnInventoryChange.Invoke(huecos, nombres);
		}

		effectApplied();

	}
	
	public void Add(Objects obj)
	{

		if (huecos.ContainsKey(obj.name))
		{

			if (huecos[obj.name].stack < huecos[obj.name].objeto.stackMax)
			{
				

				
				huecos[obj.name].stack++;

				SendOnInventoryChange.Invoke(huecos, nombres);
				Debug.Log("Sumando al stack " + huecos[obj.name].objeto.name);
			}
			else
			{
				Debug.Log("Stack Maximo alcanzado " + huecos[obj.name].objeto.stackMax);
			}
		}
		else
		{
			Debug.Log("A�adiendo ");
			notEmpty = true;
			Casilla c = new Casilla(obj);
			huecos.Add(obj.name, c);
			nombres.Add(obj.name);
			SendOnInventoryChange.Invoke(huecos, nombres);
		}
	}

	public void Quit(int value) 
	{
		if (notEmpty)
		{
			Debug.Log("Tirando " + huecos[nombres[value]].objeto.name);
			huecos[nombres[value]].stack--;

			SendOnInventoryChange.Invoke(huecos, nombres);

			if (huecos[nombres[value]].stack <= 0)
			{
				notEmpty = false;
				huecos.Remove(nombres[value]);
				nombres.RemoveAt(value);
				SendOnInventoryChange.Invoke(huecos, nombres);
			}
		}
	}

	public void ShowDic()
	{
		Debug.Log(huecos["LifePotion"].objeto.name + " " + huecos["LifePotion"].stack);
		Debug.Log(huecos["ManaPotion"].objeto.name + " " + huecos["ManaPotion"].stack);		
	}

}
