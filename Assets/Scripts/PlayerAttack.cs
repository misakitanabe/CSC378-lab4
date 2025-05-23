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

    public List<AudioClip> attackSounds;
    private AudioSource audioSource;

    private AttackSelectorUI attackSelectorUI;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        attackSelectorUI = FindObjectOfType<AttackSelectorUI>(); // finds UI manager in scene
    }

    private void Update()
    {
        // Attack Event Logic
        if (Input.GetKey(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        bool[] selectedNotes = attackSelectorUI.GetSelectedNotes();

        for (int i = 0; i < selectedNotes.Length; i++)
        {
            if (selectedNotes[i])
            {
                GameObject note = musicNotes[i];
                note.transform.position = firePoint.position;
                note.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                PlaySound(i);
            }
        }
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < attackSounds.Count)
        {
            audioSource.clip = attackSounds[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid sound index!");
        }
    }
}
