using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public string Name { get; private set; }
        public Dictionary<Sides, int> Chips { get; private set; }

        public Player (string name)
        {
            Name = name;
            Chips = new Dictionary<Sides, int>
            {
                { Sides.Rabbit, 1 },
                { Sides.Sheep, 0 },
                { Sides.Pig, 0 },
                { Sides.Cow, 0 },
                { Sides.Horse, 0 },
                { Sides.SmallDog, 0 },
                { Sides.BigDog, 0 }
            };
        }

        public void AddChips(Sides side, int count)
        {
            if (Chips.ContainsKey(side))
            {
                Chips[side] += count;
            }
        }
        
        public override string ToString()
        {
            return $"{Name}: {string.Join(", ", Chips.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
        }
    }
}
