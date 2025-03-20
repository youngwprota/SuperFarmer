using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Sides
{
    Rabbit,
    Sheep,
    Pig,
    Cow,
    Horse,
    Fox,
    Woolf,
    SmallDog,
    BigDog
}

namespace Dice
{
    public class Dice : MonoBehaviour
    {
        private List<Transform> _sides;
        private Rigidbody _rb;
        private Transform _res; 
        private bool _rolling;
        private float _timeSinceStopped;
        private float _max = float.MinValue; 

        [Header("Spawn position")]
        [SerializeField]
        private Vector3 spawnPosition;

        [Header("UI Button")]
        [SerializeField]
        private Button rollButton; 

        private void Start()
        {
            _rb = GetComponent<Rigidbody>(); 
            _sides = new List<Transform>(GetComponentsInChildren<Transform>().Where(t => t != transform)); 
            rollButton.onClick.AddListener(StartDice);
        }

        private void FixedUpdate()
        {
            if (!_rolling) return; 

            if (IsDiceStopped()) 
            {
                _timeSinceStopped += Time.fixedDeltaTime;

                if (!(_timeSinceStopped >= 0.5f)) return;
                _rolling = false; 
                var result = GetResult(); 
                OnDiceRolled?.Invoke(result);
            }
            else
            {
                _timeSinceStopped = 0f; 
            }
        }

        private void StartDice()
        {
            _rolling = true;
            _timeSinceStopped = 0f; 
            transform.position = spawnPosition; 
            var randomTorque = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
            _rb.AddTorque(randomTorque, ForceMode.Impulse);
        }

        private bool IsDiceStopped()
        {
            return _rb.velocity.magnitude < 0.1f && _rb.angularVelocity.magnitude < 0.1f;
        }

        private Sides GetResult()
        {
            _res = null;
            _max = float.MinValue;

            foreach (var side in _sides)
            {
                _max = Mathf.Max(_max, side.position.y);
            }

            foreach (var side in _sides.Where(side => Mathf.Approximately(side.position.y, _max)))
            {
                _res = side;
            }

            return Enum.TryParse(_res.name, out Sides result) ? result : Sides.Rabbit;
        }

        public event Action<Sides> OnDiceRolled;
    }
}