using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] musicNotes;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    // Combo attack Logic
    private int attack_state = 0;

    public List<AudioClip> attackSounds;       // Different sounds for each attack
    private AudioSource audioSource;           // Only ONE AudioSource to play them all

    // note: if we do mulitple attacks, only one audio source is given at one moment and cut audio short, 
    // have to implement multiple audio sources.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>(); // This audiosource is the 
    }

    private void Update()
    {
        // Update which move the play has picked
        if (Input.GetKey(KeyCode.Alpha1)) { attack_state = 0; }
        if (Input.GetKey(KeyCode.Alpha2)) { attack_state = 1; }
        if (Input.GetKey(KeyCode.Alpha3)) { attack_state = 2; }
        if (Input.GetKey(KeyCode.Alpha4)) { attack_state = 3; }
        if (Input.GetKey(KeyCode.Alpha5)) { attack_state = 4; }
        if (Input.GetKey(KeyCode.Alpha6)) { attack_state = 5; }
        if (Input.GetKey(KeyCode.Alpha7)) { attack_state = 6; }

        // Attack Event Logic
        if (Input.GetKey(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // pool fireball

        /* 
            Maybe put different animation / color in same pooling list, and just keep certain bounds for certain attack methods
            Purpose: Color and certain attack animation specified 
            0. do | 0 - 9
            1. re | 10-19
            2. mi | 20 - 29
            3. fa | 30 - 39
            4. so | 40 - 49
            5. la | 50 - 59
            6. ti | 60 - 69
        */

        musicNotes[attack_state].transform.position = firePoint.position;
        musicNotes[attack_state].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        PlaySound(attack_state);
    }

    private int FindMusicNote()
    {
        for (int i = 0; i < musicNotes.Length; i++)
        {
            if (!musicNotes[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < attackSounds.Count)
        {
            audioSource.clip = attackSounds[index];  // ✅ Just reuse the same audioSource
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound index!");
        }
    }
}
