using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public VectorValue startingPos;

    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public Interactable Interactable { get; set; }

    private Vector3 movementInput;

    private Animator animator;

         
    void Start()
    {
    animator = GetComponent<Animator>();
    transform.position = startingPos.initialValue;
    }

    void Update()
    {
        UpdateAnimator();
        if (dialogueUI.isOpen)
        {
            speed = 0;
        }
        else
        {
            speed = 6;
        }

        if (Input.GetKeyDown(KeyCode.E) && !dialogueUI.isOpen)
        {
            Interactable?.Interact(this); // Null kontrolu yapiyor, null degilse metodu cagýrýyor
        }
    }
    void UpdateAnimator()
    {
        float currentSpeed = movementInput.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }
    void FixedUpdate()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") * 0.8f, 0f);
        transform.position += movementInput.normalized * Time.fixedDeltaTime * speed;

        if (movementInput.x < 0 && !dialogueUI.isOpen)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out var interactable))
        {
            Interactable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out var interactable))
        {
            if (Interactable == interactable)
            {
                Interactable = null;
            }
        }
    }
}
