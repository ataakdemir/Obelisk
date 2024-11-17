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

    private Vector2 movementInput;

    void Start()
    {
        transform.position = startingPos.initialValue;
    }

    void Update()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");

        if (dialogueUI.isOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this); // Null kontrolu yapiyor, null degilse metodu cagýrýyor
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput.normalized * speed;

        if (movementInput.x < 0)
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
