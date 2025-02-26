using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRes : MonoBehaviour
{
    private List<Transform> _sides;
    private Rigidbody _rb;
    private float max = -float.MinValue;
    private Transform res;

    [SerializeField] private Vector3 _startPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sides = new List<Transform>(GetComponentsInChildren<Transform>());
        _sides.Remove(transform);
    }

    void Update()
    {
        if (_rb.velocity.sqrMagnitude < 0.1f)
        {
           GetResult();
        }
    }

    public void StartDice()
    {
        Debug.ClearDeveloperConsole();
        transform.position = _startPosition;

        Vector3 randomTorque = new Vector3(
            Random.Range(-100f, 100f),
            Random.Range(-100f, 100f),
            Random.Range(-100f, 100f)
        );

        _rb.AddTorque(randomTorque, ForceMode.Impulse);
    }

    public void GetResult()
    {
        max = float.MinValue;

        for (int i = 0; i < _sides.Count; i++)
        {
            max = Mathf.Max(max, _sides[i].transform.position.y);
        }

        foreach (Transform side in _sides)
        {
            if (side.transform.position.y == max)
            {
                res = side;
            }
        }
        Debug.Log(res);
    }

}
