using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
  //  public float yRange;
  //  public float negatifYRange;

    public VectorValue startingPos;

    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public Interactable Interactable { get; set; }


    void Start()
    {
        transform.position = startingPos.initialValue;
    }
    private void Update()
    {
        if (dialogueUI.isOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);//null kontrolu yapiyor, null degilse metodu cagiriyor
        }
    }
    void FixedUpdate()
    {
        

//        if (transform.position.y > yRange)
        {
//            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
  //      if (transform.position.y < negatifYRange)
        {
  //          transform.position = new Vector3(transform.position.x, negatifYRange, transform.position.z);
        }


        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * speed;

      


    }
}

