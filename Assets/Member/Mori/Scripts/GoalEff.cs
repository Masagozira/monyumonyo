using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEff : MonoBehaviour
{
    private GameObject _marimo;
    private CircleCollider2D _marimoC;
    [SerializeField]
    private GameObject _goalEff;
    private void Start()
    {
        _marimo = GameObject.Find("bone_11");
        _marimoC = _marimo.GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _marimoC)
        {
            Instantiate(_goalEff);
        }
    }
}
