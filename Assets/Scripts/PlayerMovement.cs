using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public RawImage rawImage;
    public bool swordObtained = false;
    public float speed;
    private float alpha = 1.0f;
    private float oneSecond = 1.0f;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public AudioSource SwordSwingSound;
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1); 
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && swordObtained == true 
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
            SwordSwingSound.Play();
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateMoveAnimation();
        } 
    }

    void OnEnable()
    {
        rawImage.CrossFadeAlpha(0, 1, false);
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void MoveCharacter() {

        change.Normalize();
        playerRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);

    }

    public void Knock(float knockbackTime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.RaiseSignal();
        if (currentHealth.runtimeValue > 0)
        { 
            StartCoroutine(KnockCo(knockbackTime));
        }
        else
        {
            rawImage.CrossFadeAlpha(1, 1, false);
            Application.LoadLevel(Application.loadedLevel);
        }
        
    }

    void UpdateMoveAnimation()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isMoving", true);
        }
        else { animator.SetBool("isMoving", false); }
    }

        private IEnumerator KnockCo(float knockbackTime)
    {
        if (playerRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    public void enableSword()
    {
        swordObtained = true;
    }
}
