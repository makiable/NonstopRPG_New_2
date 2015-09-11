using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IhasChanged{
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;


	// Use this for initialization
	void Start () {
	
		HasChange ();
	}

	#region IhasChanged implementation
	public void HasChange ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
		builder.Append (" = ");
		foreach (Transform slotTransform in slots) {
			GameObject item = slotTransform.GetComponent<Slot>().item;
			if (item) {
				builder.Append(item.name);
				builder.Append(" = ");
			}
		}
		inventoryText.text = builder.ToString ();
	}
	#endregion
}

namespace UnityEngine.EventSystems
{
	public interface IhasChanged:IEventSystemHandler{
		void HasChange();
	}

}
