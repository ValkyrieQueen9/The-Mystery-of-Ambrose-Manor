using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed; 
    [SerializeField]
    private float _rotationSpeed;
    private Rigidbody2D _rigidbody; 
    private PlayerAwarness _playerAwarenessController; 
    private Vector2 _targetDirection;
    private Menus menuManager;



    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody =GetComponent<Rigidbody2D>() ; 
        _playerAwarenessController = GetComponent<PlayerAwarness>();
        menuManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menus>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!menuManager.IsGamePaused)
        {
            UpdateTargetDirection();
            RotateTowardsTarget();
            SetVelocity();
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
        
    }

    private void UpdateTargetDirection()
    {

        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;

        }
    }

    private void RotateTowardsTarget(){

        if (_targetDirection == Vector2.zero) { 
            return; 
        }
        Quaternion TargetRotation = Quaternion.LookRotation(transform.forward, _targetDirection ) ; 
        //funfact if below you put a - iot runs from the player 
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, _rotationSpeed*Time.deltaTime);  
        _rigidbody.SetRotation(rotation); 

    }
    private void SetVelocity(){
        if (_targetDirection == Vector2.zero){ 
            _rigidbody.velocity = Vector2.zero; 
        }
        else 
        {
            _rigidbody.velocity =  transform.up *_speed; 

        }
    }


}
