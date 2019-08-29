using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SoundMgr : MonoBehaviour
//주의사항: 배경음악 전환과 효과음 재생을 동시에 실행 시 효과음 재생을 배경음악 전환보다 앞에 둘 경우 재생이 안된다.
{
    public static SC_SoundMgr _soundMgr;

    public float sfxVolume;
    public float musicVolume;
    public AudioSource source;
    public AudioClip[] _s_footSteps;
    public WaitForSeconds delayFootStep;
    public AudioClip _b_inn;
    public AudioClip _b_area1;
    public AudioClip _b_area2;
    public AudioClip _b_area3;
    public AudioClip _s_innDoor;
    public AudioClip _s_Box;
    private IEnumerator _CO_footStep;
    public AudioClip _s_biff;
    public AudioClip _s_click;
    public AudioClip _s_Coin;

    private void Awake()
    {
        _soundMgr = this;
        source = Camera.main.gameObject.GetComponent<AudioSource>();
        delayFootStep = new WaitForSeconds(_s_footSteps[0].length);
        _CO_footStep = _sfxFootstep();
    }

    public void AdjustSFXVol(float input) { sfxVolume = input; }
    public void AdjustMusicVol(float input) { musicVolume = source.volume = input; }

    public void BGM_Inn()
    {
        source.clip = _b_inn;
        source.Play();
    }
    public void BGM_Area1()
    {
        source.clip = _b_area1;
        source.Play();
    }
    public void BGM_Area2()
    {
        source.clip = _b_area2;
        source.Play();
    }
    public void BGM_Area3()
    {
        source.clip = _b_area3;
        source.Play();
    }
    public void BGM_Stop()
    {
        source.Stop();
    }
    public void SFX_InnDoor() { source.PlayOneShot(_s_innDoor, sfxVolume); }
    private IEnumerator _sfxFootstep()
    {
        while (true)
        {
            source.PlayOneShot(_s_footSteps[Random.Range(0, _s_footSteps.Length)], sfxVolume);
            yield return delayFootStep;
        }
    }
    public void SFX_FootStepStart() { StartCoroutine(_CO_footStep); }
    public void SFX_FootStepStop() { StopCoroutine(_CO_footStep); }
    public void SFX_Box() { source.PlayOneShot(_s_Box, sfxVolume); }
    public void SFX_ClickOK() { source.PlayOneShot(_s_click, sfxVolume); }
    public void SFX_ClickBiff() { source.PlayOneShot(_s_biff, sfxVolume); }
    public void SFX_Coin() { source.PlayOneShot(_s_Coin, sfxVolume); }
    public AudioClip _s_PutinBag;
    public void SFX_PutinBag() { source.PlayOneShot(_s_PutinBag, sfxVolume); }
    public AudioClip _s_Find;
    public void SFX_Find() { source.PlayOneShot(_s_Find, sfxVolume); }
    public AudioClip _s_Spot;
    public void SFX_Spot() { source.PlayOneShot(_s_Spot, sfxVolume); }
    public AudioClip _s_Heal;
    public void SFX_Heal() { source.PlayOneShot(_s_Heal, sfxVolume); }
    public AudioClip _s_SkillGet;
    public void SFX_SkillGet() { source.PlayOneShot(_s_SkillGet, sfxVolume); }
    public AudioClip _s_Dmg;
    public void SFX_Dmg() { source.PlayOneShot(_s_Dmg, sfxVolume); }
    public AudioClip _s_NoDmg;
    public void SFX_NoDmg() { source.PlayOneShot(_s_NoDmg, sfxVolume); }
    public AudioClip _s_Die;
    public void SFX_Die() { source.PlayOneShot(_s_Die, sfxVolume); }
    public AudioClip _s_Stun;
    public void SFX_Stun() { source.PlayOneShot(_s_Stun, sfxVolume); }
    public AudioClip _s_Counter;
    public void SFX_Counter() { source.PlayOneShot(_s_Counter, sfxVolume); }
    public AudioClip _s_Bomb;
    public void SFX_Bomb() { source.PlayOneShot(_s_Bomb, sfxVolume); }
    public AudioClip _s_SimpleSkill;
    public void SFX_SimpleSkill() { source.PlayOneShot(_s_SimpleSkill, sfxVolume); }
    public AudioClip _s_SimpleHit;
    public void SFX_SimpleHit() { source.PlayOneShot(_s_SimpleHit, sfxVolume); }
    public AudioClip _s_Switch;
    public void SFX_Switch() { source.PlayOneShot(_s_Switch, sfxVolume); }
    public AudioClip _s_PlayerFail;
    public void SFX_PlayerFail() { source.PlayOneShot(_s_PlayerFail, sfxVolume); }
    public AudioClip _s_PlayerWin;
    public void SFX_PlayerWin() { source.PlayOneShot(_s_PlayerWin, sfxVolume); }
    public AudioClip _s_Guard;
    public void SFX_Guard() { source.PlayOneShot(_s_Guard, sfxVolume); }
}
