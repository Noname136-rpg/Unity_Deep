using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode keyCodeJump = KeyCode.Space;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipWalk;
    [SerializeField]
    private AudioClip audioClipRun;


    private MouseController MousController;
    private MoveMentController MoveController;
    private PlayerStatus Pstatus;
    private PlayerAnimationContoller Panimator;
    private AudioSource PaudioSource;
    private FireRifle Rifle;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        MousController = GetComponent<MouseController>();
        MoveController = GetComponent<MoveMentController>();
        Pstatus = GetComponent<PlayerStatus>();
        Panimator = GetComponent<PlayerAnimationContoller>();
        PaudioSource = GetComponent<AudioSource>();
        Rifle = GetComponentInChildren<FireRifle>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateJump();
        UpdateRifleAction();
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
            PaudioSource.clip = isRun ? audioClipRun : audioClipWalk;

            if(PaudioSource.isPlaying == false) 
            {
                PaudioSource.loop = true;
                PaudioSource.Play();
            }
        }
        else
        {
            MoveController.MoveSpeed = 0;
            Panimator.MoveSpeed = 0;

            if (PaudioSource.isPlaying == true)
            {
                PaudioSource.Stop();
            }
        }

        MoveController.MoveTo(new Vector3(x, 0, z));
    }
    private void UpdateJump()
    {
        if(Input.GetKeyDown(keyCodeJump))
        {
            MoveController.Jump();
        }
    }

    private void UpdateRifleAction()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Rifle.StartWeaponAction();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Rifle.StopWeaponAction();
        }
    }
}
