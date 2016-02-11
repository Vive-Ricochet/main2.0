﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PlayerItemManager : MonoBehaviour
{
	public class Item
	{
		public string Name;
		public int Attack;
		public int Defense;
		public int Durability
		{
			get; set;
		}

		public Item(string name, int attack, int defense, int durability)
		{
			Name = name;
			Attack = attack;
			Defense = defense;
			Durability = durability;
		}

		public void decrementDurability()
		{
			Durability = Durability - 1;
		}
	}

	private List<PickupProperties> righthand = new List<PickupProperties>();
	private List<PickupProperties> lefthand = new List<PickupProperties>();

	public void addItem(string hand, Item item) {
		switch(hand){
			case "left":
//				lefthand.Add (item);
				break;
			case "right":
//				righthand.Add (item);
				break;
		}
	}

	public void RemoveItem(string hand, Item item){
		switch(hand){
		case "left":
//			lefthand.Remove (item);
			break;
		case "right":
//			righthand.Remove (item);
			break;
		}
	}

	public List<PickupProperties> getLH(){
		return lefthand;
	}

	public List<PickupProperties> getRH(){
		return righthand;
	}


	public void TickDurability(List<Item> hand)
	{
		if (hand != null)
		{
			for (int i = 0; i < hand.Count; i++)
			{
				Item item = hand.ElementAt(i);
				item.decrementDurability();
				//print("I get here");
				if (item.Durability <= 0)
				{
					print(item.Name + " at 0");
					hand.Remove(item);
				}
			}
		}
		else print("null list or some shit");
	}

	public int getTotalAttack(List<Item> hand)
	{
		int total = 0;
		foreach(Item thing in hand)
		{
			total = thing.Attack;
		}
		return total;
	}

	public int getTotalDefense(List<Item> hand)
	{
		int total = 0;
		foreach (Item thing in hand)
		{
			total = thing.Defense;
		}
		return total;
	}

	public void Clash(List<Item> Thishand, List<Item> Otherhand)
	{
		int attack = getTotalAttack(Thishand);
		int defense = getTotalDefense(Otherhand);

		int damage = attack - defense + 5;

		print(damage);
	}
}