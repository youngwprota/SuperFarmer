using System;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace Chips
{
    public class ChipsManager : MonoBehaviour
    {
        private Dictionary<Sides, int> totalChips = new Dictionary<Sides, int>
        {
            { Sides.Rabbit, 60 },
            { Sides.Sheep, 24 },
            { Sides.Pig, 20 },
            { Sides.Cow, 12 },
            { Sides.Horse, 6 },
            { Sides.SmallDog, 4 },
            { Sides.BigDog, 2 }
        };

        public void ManageChips(Player.Player player, Sides side1, Sides side2)
        {
            if (side1 == side2 && IsValidSide(side1))
            {
                HandleMatch(player, side1);
            }
            else
            {
                HandleDifferentSides(player, side1, side2);
            }

            HandleSpecialCases(player, side1, side2);
        }

        private void HandleMatch(Player.Player player, Sides side)
        {
            if (player.Chips[side] == 0)
            {
                GiveChips(player, side, 1);
            }
            else
            {
                GiveChips(player, side, 2);
            }
        }

        private void HandleDifferentSides(Player.Player player, Sides side1, Sides side2)
        {
            if (IsValidSide(side1) && player.Chips[side1] != 0)
            {
                GiveChips(player, side1, 1);
            }

            if (IsValidSide(side2) && player.Chips[side2] != 0)
            {
                GiveChips(player, side2, 1);
            }
        }

        private void HandleSpecialCases(Player.Player player, Sides side1, Sides side2)
        {
            if (IsFoxCase(side1, side2))
            {
                HandleFox(player, side1, side2);
            }

            if (IsWoolfCase(side1, side2))
            {
                HandleWoolf(player, side1, side2);
            }

            if (IsCombinedCase(side1, side2))
            {
                HandleCombinedCase(player);
            }
        }

        private void HandleFox(Player.Player player, Sides side1, Sides side2)
        {
            if (player.Chips[Sides.SmallDog] == 0)
            {
                ReturnChipsToPool(player, Sides.Rabbit);
            }
            else
            {
                ReturnChipsToPool(player, Sides.SmallDog);
            }
        }

        private void HandleWoolf(Player.Player player, Sides side1, Sides side2)
        {
            if (player.Chips[Sides.BigDog] == 0)
            {
                ReturnChipsToPool(player, Sides.Sheep);
                ReturnChipsToPool(player, Sides.Pig);
                ReturnChipsToPool(player, Sides.Cow);
            }
            else
            {
                ReturnChipsToPool(player, Sides.BigDog);
            }
        }

        private void HandleCombinedCase(Player.Player player)
        {
            if (player.Chips[Sides.BigDog] == 0)
            {
                ReturnChipsToPool(player, Sides.Sheep);
                ReturnChipsToPool(player, Sides.Pig);
                ReturnChipsToPool(player, Sides.Cow);
            }
            else
            {
                ReturnChipsToPool(player, Sides.BigDog);
            }

            if (player.Chips[Sides.SmallDog] == 0)
            {
                ReturnChipsToPool(player, Sides.Rabbit);
            }
            else
            {
                ReturnChipsToPool(player, Sides.SmallDog);
            }
        }

        private void GiveChips(Player.Player player, Sides side, int count)
        {
            if (totalChips[side] >= count)
            {
                player.AddChips(side, count);
                totalChips[side] -= count;
            }
        }

        private void ReturnChipsToPool(Player.Player player, Sides side)
        {
            totalChips[side] += player.Chips[side];
            player.Chips[side] = 0;
        }

        private bool IsValidSide(Sides side)
        {
            return totalChips.ContainsKey(side) && totalChips[side] > 0;
        }

        private bool IsFoxCase(Sides side1, Sides side2)
        {
            return side1 == Sides.Fox || side2 == Sides.Fox;
        }

        private bool IsWoolfCase(Sides side1, Sides side2)
        {
            return side1 == Sides.Woolf || side2 == Sides.Woolf;
        }

        private bool IsCombinedCase(Sides side1, Sides side2)
        {
            return (side1 == Sides.Woolf && side2 == Sides.Fox) || (side1 == Sides.Fox && side2 == Sides.Woolf);
        }
    }
}