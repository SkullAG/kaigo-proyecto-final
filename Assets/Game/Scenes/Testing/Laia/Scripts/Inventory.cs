using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{    
	public Transform _capsule;
	public Button _button;
	bool notEmpty = false;
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

	private Dictionary<string, Casilla> huecos2=new Dictionary<string, Casilla>();

	[SerializeField]
	public List<Casilla> huecos = new List<Casilla>();
	[SerializeField]
	public List<string> nombres = new List<string>();

	public void Use(int value)
	{
		if (notEmpty)
		{
			Debug.Log("Usando " + huecos[value].objeto.name);
			huecos[value].objeto.Use();
			//huecos[value].SetStackValue(huecos[value].stack - 1);
			

			if (huecos[value].stack <= 0)
            {
				notEmpty = false;
				huecos.RemoveAt(value);
				//huecos2.Remove();			
			}
		}      
	}
	
	public void Add(Objects obj)
    {

		if (huecos2.ContainsKey(obj.name))
        {
			if (huecos2[obj.name].stack < huecos2[obj.name].objeto.stackMax)
			{
				Debug.Log("Añadiendo " + huecos2[obj.name].objeto.name);

				notEmpty = true;
				huecos2[obj.name].stack++;
				//Casilla ca = huecos[i];
				//ca.stack +=1;
			}
			else
			{
				Debug.Log("Stack Maximo alcanzado " + huecos2[obj.name].objeto.stackMax);
			}
		}
		else
		{
			Casilla c = new Casilla(obj);
			huecos2.Add(obj.name, c);
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

	public void Quit(int value) 
	{
		Debug.Log("Quitando " + huecos[value].objeto.name);
		huecos.RemoveAt(value);
	}

    public void ShowDic()
    {
       
			//for(int i=0;i<huecos2.Count;i++)
            //{
				Debug.Log(huecos2["LifePotion"].objeto.name + " " + huecos2["LifePotion"].stack);
				Debug.Log(huecos2["ManaPotion"].objeto.name + " " + huecos2["ManaPotion"].stack);
			//			}

		
    }

}
