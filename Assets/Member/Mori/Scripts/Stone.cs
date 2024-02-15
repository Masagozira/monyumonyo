using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private Vector2 _stoVec;
    private Rigidbody2D _rig;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _isPlayer = false;
    [SerializeField]
    private GameObject _pushMaru;
    private Rigidbody2D _pushMaruRig;
    private GameObject _player;

    private void Start()
    {
        _rig= this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("bone_11");
        _pushMaruRig = _pushMaru.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("HIT");
        if (collision.gameObject.tag == "Player")
        {
            _canMove = false;
            _isPlayer = true;
            _rig.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rig.constraints = RigidbodyConstraints2D.FreezePositionX;
            return;
        }


        //_stoVec += new Vector2(0.1f, 0);
        //this.transform.position = _stoVec;

        if (_canMove==true&&_isPlayer==false)
        {
            _stoVec.x += 0.2f;
            this.transform.position = _stoVec;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayer = false;
            _rig.constraints = RigidbodyConstraints2D.None
                | RigidbodyConstraints2D.FreezePositionX
                | RigidbodyConstraints2D.FreezeRotation;
        }

    }
    private void Update()
    {
        _stoVec = this.transform.position;
        if (this.transform.position.x - 2.2f < _pushMaru.transform.position.x&&_isPlayer==false)
        {
            _canMove = true;
        }
        else _canMove = false;

        if (this.transform.position.x + 2.3f > _player.transform.position.x)
        {
            _isPlayer = true;
        }
        else _isPlayer = false;
        if (_player == true&&_canMove==true)
        {
            _pushMaruRig.velocity = new Vector2(0, 0);
        }
    }
}