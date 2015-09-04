using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;

public class RecItem{
	public int ID;
	public string Name;
	public int Level;
	public float Critical;
}

public sealed class ItemIO{

	public static void Write(List<RecItem> ItemList, string filePath)
	{
		XmlDocument Document = new XmlDocument ();
		XmlElement ItemListElement = Document.CreateElement ("ItemList");
		Document.AppendChild (ItemListElement);
		
		foreach (RecItem Item in ItemList) {
			XmlElement ItemElement = Document.CreateElement ("Item"); 
			ItemElement.SetAttribute ("ID", Item.ID.ToString());
			ItemElement.SetAttribute ("Name", Item.Name); 
			ItemElement.SetAttribute ("Level", Item.Level.ToString ()); 
			ItemElement.SetAttribute ("Critical", Item.Critical.ToString ()); 
			ItemListElement.AppendChild (ItemElement);
		}
		Document.Save (filePath);
	}


	public static List<RecItem> Read(string filePath){

		XmlDocument Document = new XmlDocument ();
		Document.Load (filePath);
		XmlElement ItemListElement = Document ["ItemList"];

		List<RecItem> ItemList = new List<RecItem> ();

		foreach (XmlElement itemElement in ItemListElement.ChildNodes) {
			RecItem Item = new RecItem();
			Item.ID = System.Convert.ToInt32(itemElement.GetAttribute("ID"));
			Item.Name = itemElement.GetAttribute("Name");
			Item.Level = System.Convert.ToInt32(itemElement.GetAttribute("Level"));
			//Item.Level = int.Parse(itemElement.GetAttribute("Level");;
			Item.Critical = System.Convert.ToSingle(itemElement.GetAttribute("Critical"));

			ItemList.Add(Item);
		}
		return ItemList;
	}

}









