using System.Collections.Generic;
using Chips;
using UnityEngine;

namespace Global
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private List<Player.Player> players = new List<Player.Player>();
        [SerializeField] private DiceObserver diceObserver; 
        [SerializeField] private ChipsManager chipsManager; 
        [SerializeField] [Range(2, 4)] private int playerCount = 2;

        private int _currentPlayerIndex = 0;
        
        private void Start()
        {
            for (var i = 0; i < playerCount; i++) 
                players.Add(new Player.Player($"Player {i + 1}"));
          
            diceObserver.OnMatch += HandleDiceMatch;
        }

        private void HandleDiceMatch(Sides side1, Sides side2)
        {
            var currentPlayer = players[_currentPlayerIndex];

            PrintPlayerChips();
        
            chipsManager.ManageChips(currentPlayer, side1, side2);

            PrintPlayerChips();
        
            _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        }

        private void OnDestroy()
        {
            diceObserver.OnMatch -= HandleDiceMatch;
        }

        public void PrintPlayerChips()
        {
            foreach (var player in players)
            {
                Debug.Log(player);
            }
        }
    }
}