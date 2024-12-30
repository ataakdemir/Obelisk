using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public VectorValue startingPos;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;
    public ResponseHandler responseHandler;
    public Interactable Interactable { get; set; }

    public NotesInteraction notesPanel;

    private Vector3 movementInput;
    private Animator animator;

    public GameObject fullBoard;
    public GameObject emptyBoard;

    public GameObject fullBoardIcon;
    public GameObject emptyBoardIcon;
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = startingPos.initialValue;
    }

    void Update()
    {
        UpdateAnimator();

        if (dialogueUI != null && dialogueUI.isOpen || fullBoard.activeSelf || (notesPanel != null && notesPanel.notlar.activeSelf))
        {
            speed = 0;
        }
        else
        {
            speed = 5;
        }

        if (Input.GetKeyDown(KeyCode.E) && dialogueUI != null && !dialogueUI.isOpen)
        {
            Interactable?.Interact(this);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && dialogueUI != null && dialogueUI.isOpen)
        {
            dialogueUI.CloseDialogueBox();
            responseHandler.ResetResponseBox();
            dialogueUI.ResetDialogue();

        }

        if (GameManager.Instance.AllNPCsTalkedTo())
        {
            fullBoardIcon.SetActive(true);
            emptyBoardIcon.SetActive(false);
        }
        else
        {
            fullBoardIcon.SetActive(false);
            emptyBoardIcon.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.B) && !dialogueUI.isOpen)
        {
            if (!fullBoard.activeSelf) 
            {
                if (GameManager.Instance.AllNPCsTalkedTo())
                {
                    fullBoard.SetActive(true);
                    emptyBoard.SetActive(false);
                }
                else
                {
                    fullBoard.SetActive(true);
                    emptyBoard.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && fullBoard.activeSelf)
        {
            fullBoard.SetActive(false);
            emptyBoard.SetActive(false);
        }

    }
    void FixedUpdate()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") * 0.8f, 0f);
        transform.position += movementInput.normalized * Time.fixedDeltaTime * speed;

        if (movementInput.x < 0 && (dialogueUI == null || !dialogueUI.isOpen))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);

        transform.Translate(move * Time.deltaTime * 5f);
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

    void UpdateAnimator()
    {
        float currentSpeed = movementInput.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }
}