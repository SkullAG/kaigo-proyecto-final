using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Core.Characters;

public class Inventory : MonoBehaviour
{    
	public Transform _capsule;
	public Button _button;
	bool notEmpty = false;
	public GameObject _Ibutton;
	public GameObject _Panel;
	public Targetter _target;
	private int value;

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

	private Dictionary<string, Casilla> huecos = new Dictionary<string, Casilla>();

	[SerializeField]
	public List<string> nombres = new List<string>();

	private void Awake()
    {
		_target.onTargetSelect += OnTargetSelect;

	}


	public void OnTargetSelect(Character target) 
	{
		ApplyEffect(value);
		Debug.Log("OnTargetSelect");
	}
	public void Use(int value)
	{
		if (notEmpty)
		{
			_target.Enable();

			this.value = value;
		}      
	}
	public void ApplyEffect(int value)
    {
		Debug.Log("Usando " + huecos[nombres[value]].objeto.name);
		huecos[nombres[value]].objeto.Use(_target.currentTarget);
		huecos[nombres[value]].stack--;

		SendOnInventoryChange.Invoke(huecos, nombres);

		if (huecos[nombres[value]].stack <= 0)
		{
			//notEmpty = false;
			huecos.Remove(nombres[value]);
			nombres.RemoveAt(value);
			//huecos.Remove();	
			SendOnInventoryChange.Invoke(huecos, nombres);
		}
	}
	
	public void Add(Objects obj)
    {

		if (huecos.ContainsKey(obj.name))
        {
			//Debug.Log("Añadiendo primero");

			if (huecos[obj.name].stack < huecos[obj.name].objeto.stackMax)
			{
				

				
				huecos[obj.name].stack++;

				SendOnInventoryChange.Invoke(huecos, nombres);
				//SendOnInventoryChange.Invoke(huecos);
				Debug.Log("Sumando al stack " + huecos[obj.name].objeto.name);
			}
			else
			{
				Debug.Log("Stack Maximo alcanzado " + huecos[obj.name].objeto.stackMax);
			}
		}
		else
		{
			Debug.Log("Añadiendo ");
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



/*public void Add2(Objects obj)
{
	for(int i = 0; i < huecos.Count; i++)
	{
		if (huecos[i].objeto == obj)
		{
			if (huecos[i].stack < huecos[i].objeto.stackMax)
			{
				Debug.Log("Añadiendo " + huecos[i].objeto.names);

				notEmpty = true;
				huecos[i].stack++;
				huecos[i].SetStackValue(huecos[i].stack + 1);
				//Casilla ca = huecos[i];
				//ca.stack +=1;
			}
			else
			{
				Debug.Log("Stack Maximo alcanzado " + huecos[i].objeto.stackMax);
			}
			return;
		}
	}
	Casilla c = new Casilla();

	c.objeto = obj;
	c.stack = 1;

	huecos.Add(c);
}*/
