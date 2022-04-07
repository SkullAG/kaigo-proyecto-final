using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonConstructor : MonoBehaviour
{
    //public GameObject buttons;
    public GameObject _button;
    public Inventory _inventory;

    public void OnEnable()
    {
        _inventory.SendOnInventoryChange.AddListener(UpdateList);
    }

    [SerializeField]
    public List<GameObject> buttonslist = new List<GameObject>();

    private List<GameObject> savedButList = new List<GameObject>();
    public void UpdateList(Dictionary<string, Inventory.Casilla> huecos, List<string> nombres)
    {
        /*if (huecos.Count < buttonslist.Count)
        {
            List<GameObject> butL = buttonslist.GetRange(huecos.Count-1, buttonslist.Count-1);

            foreach(GameObject b in butL)
            {
                savedButList.Add(b);
                b.SetActive(true);
            }
        }*/
        if (huecos.Count > buttonslist.Count)
        {
            int dif = huecos.Count - buttonslist.Count;

            for (int i = 0; i < dif; i++)
            {
                GameObject but = Instantiate(_button, transform.position, transform.rotation);

                but.transform.parent = transform;

                but.GetComponent<ButtonHelper>().SendOnClick.AddListener(_inventory.Use);

                buttonslist.Add(but);
            }
            
        }

        for (int i = 0; i < nombres.Count; i++)
        {
            buttonslist[i].GetComponentInChildren<Text>().text = nombres[i] + " X" + huecos[nombres[i]].stack;

            buttonslist[i].GetComponent<ButtonHelper>().value = i;          

        }

        /*if (huecos[nombres[value]].stack == 0)
        {
            buttonslist.Remove(but);
            but.SetActive(false);
        }*/
    }   
}
