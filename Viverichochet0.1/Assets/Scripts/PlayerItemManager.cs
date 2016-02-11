using UnityEngine;
using System.Collections.Generic;
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
    private List<Item> righthand = new List<Item>();
    private List<Item> lefthand = new List<Item>();

    public void addItem(List<Item> hand, Item item)
    {
        hand.Add(item);
    }
    public void RemoveItem(List<Item> hand, Item item)
    {
        hand.Remove(item);
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

    void FixedUpdate()
    {
        if(Input.GetButtonDown("checkClash")){
            Item trashcan = new Item("Trashcan", 3, 3, 3);
            Item manhole = new Item("Manhole", 2, 2, 2);
            addItem(righthand, trashcan);
            addItem(lefthand, manhole);
            Clash(righthand, lefthand);
        }

        if (Input.GetButtonDown("addtrashcan"))
        {
            Item trashcan = new Item("Trashcan", 3, 3, 3);
            //print("trashcan made?");
            addItem(righthand, trashcan);
            Item manhole = new Item("Manhole", 2, 2, 2);
            //print("Item added?");
            addItem(righthand, manhole);
            TickDurability(righthand);
            foreach (Item thing in righthand) print("Tic one:" + thing.Durability + " " + thing.Name);
            TickDurability(righthand);
            foreach (Item thing in righthand) print("Tic Two:" +  thing.Durability + " " +  thing.Name);
            TickDurability(righthand);
            foreach (Item thing in righthand) print("Tic Three:" + thing.Durability + " " + thing.Name);
            if (righthand.ElementAtOrDefault(0) != null)
            {
                print(righthand.ElementAt(0).Name);
            }
            else print("we did it boys");
        }
    }
}