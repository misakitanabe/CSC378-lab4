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
        attackSelectorUI = FindFirstObjectByType<AttackSelectorUI>(); // finds UI manager in scene
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

        //how are the indices formalized?
        /*
            0 - do | 0-9 
            1 - re | 10-19
            2 - mi | 20-29
            3 - fa | 30-39
            4 - so | 40-49
            5 - la | 50-59
            6 - ti | 60-69
            I dont understand what ur doing...
        */

        for (int i = 0; i < selectedNotes.Length; i++)
        {
            if (selectedNotes[i])
            {
                PlaySound(i);
                GameObject noteHolder = musicNotes[i];          // MusicNote_Do in hierarchy
                GameObject note = FindMusicNote(noteHolder);    // Do in hierarchy
                note.transform.position = firePoint.position;
                note.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            }
        }
    }

    // returns the first note gameobject it finds that is inactive inside of a noteHolder ie MusicNote_Do
    private GameObject FindMusicNote(GameObject noteHolder)
    {
        // iterates through the pooled note GameObjects 
        foreach (Transform child in noteHolder.transform)
        {
            // returns that gameobject if inactive in hierarchy
            if (!child.gameObject.activeInHierarchy)
            {
                return child.gameObject;
            }
        }
        return null; // All children are active - no available notes
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < attackSounds.Count)
        {
            audioSource.clip = attackSounds[index];
            audioSource.PlayOneShot(attackSounds[index]);
        }
        else
        {
            Debug.LogWarning("Invalid sound index!");
        }
    }
}
