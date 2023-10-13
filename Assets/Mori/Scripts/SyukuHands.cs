using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyukuHands : MonoBehaviour
{
    //©g‚ÌTransform
    [SerializeField] 
    private Transform _armsTra;

    //ƒ^[ƒQƒbƒg‚ÌTransform
    [SerializeField] private Transform _target;

    private Vector3 dir;
    private Vector3 dir2;

    // Update is called once per frame
    void Update()
    {
        //Œü‚«‚½‚¢•ûŒü‚ğŒvZ
        dir = (_target.position - _armsTra.position);
        //Œü‚«‚½‚¢•ûŒü‚É‰ñ“]
        _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

}

/*
void Update()
{
    //Œü‚«‚½‚¢•ûŒü‚ğŒvZ
    dir = (_target.position - _armsTra.position);
    //Œü‚«‚½‚¢•ûŒü‚É‰ñ“]
    _armsTra.rotation = Quaternion.FromToRotation(Vector3.up, dir);

}
void LateUpdate()
{
    transform.position
        = Vector3.Lerp(transform.position
        , _target.transform.position + dir
        , 6.0f * Time.deltaTime);
}
*/