using UnityEngine;

public class Normal_Boss_Run : StateMachineBehaviour
{
Transform player;
    Rigidbody2D rb;
    public float speed = 1.1f;
    NormalFireBoss boss;
    public float attackRange = 8f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<NormalFireBoss>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        
        float distance = Vector2.Distance(player.position, rb.position);
        // Debug.Log("Distance to player: " + distance + " | Within range? " + (distance <= attackRange));

        if (distance > attackRange)
        {
            // Only move if player is out of attack range
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            // Player is in range, trigger attack
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }
}
