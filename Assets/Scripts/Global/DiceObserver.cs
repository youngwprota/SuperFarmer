using System;
using UnityEngine;

namespace Global
{
    public class DiceObserver : MonoBehaviour
    {
        [SerializeField] private Dice.Dice blueDice;
        [SerializeField] private Dice.Dice orangeDice;

        private readonly Sides[] _diceValues = new Sides[2]; 
        private int _valuesCount = 0; 

        private void OnDiceRolled(Sides result)
        {
            if (_valuesCount >= 2) return;

            _diceValues[_valuesCount] = result;
            _valuesCount++;

            if (_valuesCount == 2)
            {
                CheckForMatch();
            }
        }

        private void CheckForMatch()
        {
            OnMatch?.Invoke(_diceValues[0], _diceValues[1]);
            ResetDiceValues();
        }

        private void ResetDiceValues()
        {
            _diceValues[0] = default;
            _diceValues[1] = default;
            _valuesCount = 0;
        }

        private void Awake()
        {
            if (blueDice == null || orangeDice == null) return;

            blueDice.OnDiceRolled += OnDiceRolled;
            orangeDice.OnDiceRolled += OnDiceRolled;
        }

        private void OnDestroy()
        {
            if (blueDice == null || orangeDice == null) return;

            blueDice.OnDiceRolled -= OnDiceRolled;
            orangeDice.OnDiceRolled -= OnDiceRolled;
        }

        public event Action<Sides, Sides> OnMatch;
    }
}