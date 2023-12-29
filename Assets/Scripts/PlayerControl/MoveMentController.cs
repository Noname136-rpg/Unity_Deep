using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveMentController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveForce;
    private CharacterController characterController;
    
    public float MoveSpeed
    {
        set => moveSpeed = Mathf.Max(0, value);
        get => moveSpeed;   
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);  

        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y , direction.z*moveSpeed);
    }
}
