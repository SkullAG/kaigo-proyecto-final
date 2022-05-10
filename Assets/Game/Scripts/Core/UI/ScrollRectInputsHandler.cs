using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectInputsHandler : MonoBehaviour
{
	[SerializeField]
	public ScrollRect scroll { get; private set; }
	public RectTransform viewportRect => scroll.viewport;
	public RectTransform contentRect => scroll.content;

	public List<Button> buttonList = new List<Button>(); //Leo cambia la forma en la que se consigue esto 

	GameObject CurrSelGO => EventSystem.current.currentSelectedGameObject;

	void Start()
	{
		scroll = GetComponent<ScrollRect>();
	}

    void Update()
	{
		foreach (Button but in buttonList)
		{
			if (but.gameObject == CurrSelGO)
			{
				Rect rectBut = ((RectTransform)but.transform).rect;
				if (scroll.vertical && scroll.verticalScrollbar)
				{
					float minButY = but.transform.position.y + rectBut.yMin;
					float minViewY = viewportRect.position.y + viewportRect.rect.yMin;

					if (minButY < minViewY)
                    {
						scroll.verticalScrollbar.value -= (minViewY - minButY) / contentRect.rect.height;
					}

					float maxButY = but.transform.position.y + rectBut.yMax;
					float maxViewY = viewportRect.position.y + viewportRect.rect.yMax;

					if (maxButY > maxViewY)
					{
						scroll.verticalScrollbar.value -= (maxViewY - maxButY) / contentRect.rect.height;
					}
				}

				if (scroll.horizontal && scroll.horizontalScrollbar)
				{
					float minButX = but.transform.position.x + rectBut.xMin;
					float minViewX = viewportRect.position.x + viewportRect.rect.xMin;

					if (minButX < minViewX)
					{
						scroll.horizontalScrollbar.value -= (minButX - minViewX) / contentRect.rect.height;
					}

					float maxButX = but.transform.position.x + rectBut.xMax;
					float maxViewX = viewportRect.position.x + viewportRect.rect.xMax;

					if (maxButX > maxViewX)
					{
						scroll.horizontalScrollbar.value -= (maxButX - maxViewX) / contentRect.rect.width;
					}
				}
			}
		}
	}
}
