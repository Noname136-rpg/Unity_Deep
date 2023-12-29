using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRifle : MonoBehaviour
{
    [Header("Muzzle Effect")]
    [SerializeField]
    private GameObject[] muzzleFlashEffectList;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip AudioC;
    [SerializeField]
    private AudioClip AudioClipFire;

    [Header("Weapon Information")]
    [SerializeField]
    private WeaponInfo weaponInfo;

    private float lastAttackTime = 0;
    private PlayerAnimationContoller animator;

    private AudioSource AudioS;

    // Start is called before the first frame update
     
    private void Awake()
    {
        AudioS = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimationContoller>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        PlaySound(AudioC);

        foreach(GameObject muzzleFlashEffect in muzzleFlashEffectList)
        {
            muzzleFlashEffect.SetActive(false);
        }
    }
    public void StartWeaponAction(int type = 0)
    {
        if (type == 0)  
        {
            if (weaponInfo.isAutomaticAttack == true) 
            {
                StartCoroutine("OnAttackLoop");
            }
            else
            {
                OnAttack();  
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        if (type == 0) 
        {
            StopCoroutine("OnAttackLoop");
        }
    }

    private IEnumerator OnAttackLoop()
    {
        while (true)
        {
            OnAttack();

            yield return null;
        }
    }


    public void OnAttack()
    {
        if (Time.time - lastAttackTime > weaponInfo.attackRate)
        {
            if (animator.MoveSpeed > 0.5f)
            {
                return;
            }

            lastAttackTime = Time.time;

            animator.Play("GunFire", -1, 0);

            StartCoroutine("OnMuzzleFlashEffect");

            PlaySound(AudioClipFire);
        }
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        int effectIndex = Random.Range(0, 5);
        muzzleFlashEffectList[effectIndex].SetActive(true);
        yield return new WaitForSeconds(weaponInfo.attackRate * 0.3f);
        muzzleFlashEffectList[effectIndex].SetActive(false);
    }

    private void PlaySound(AudioClip clip) 
    {
        AudioS.Stop();
        AudioS.clip = clip;
        AudioS.Play();
        
    }
}
