using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;


    private MouseController MousController;
    private MoveMentController MoveController;
    private PlayerStatus Pstatus;
    private PlayerAnimationContoller Panimator;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        MousController = GetComponent<MouseController>();
        MoveController = GetComponent<MoveMentController>();
        Pstatus = GetComponent<PlayerStatus>();
        Panimator = GetComponent<PlayerAnimationContoller>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        MousController.UpdateRotate(mouseX, mouseY);
    }
    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(x != 0 || z != 0) 
        {
            bool isRun = false;

            if(z > 0)
            {
                isRun = Input.GetKey(keyCodeRun);
            }

            MoveController.MoveSpeed = isRun ? Pstatus.RunSpeed : Pstatus.WalkSpeed;
            Panimator.MoveSpeed = isRun ? 1 : 0.5f;
        }
        else
        {
            MoveController.MoveSpeed = 0;
            Panimator.MoveSpeed = 0;
        }

        MoveController.MoveTo(new Vector3(x, 0, z));
    }
}
