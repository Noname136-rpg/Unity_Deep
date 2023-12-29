using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationContoller : MonoBehaviour
{
    private Animator  animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public float MoveSpeed
    {
        set => animator.SetFloat("MoveMentSpeed", value);
        get => animator.GetFloat("MoveMentSpeed");
    }

    public void Play(string stateName, int layer, float normalixedTime)
    {
        animator.Play(stateName, layer, normalixedTime);
    }
}
