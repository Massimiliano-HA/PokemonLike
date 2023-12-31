using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ISavable
{
    public float moveSpeed;
    public LayerMask SolidObjects;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask grassLayer;
    public bool justTeleported = false;
    public bool canMove = true;

    private void Start()
    {
        // Assurez-vous que vous référencez le composant Animator de votre joueur
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if(!canMove)
            return;

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);

                if(IsWalkable(targetPos)) {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            Interact();
        }

        animator.SetBool("isMoving", isMoving);

        if (justTeleported)
        {
            justTeleported = false;
            isMoving = false;
            StopAllCoroutines();
            canMove = false;
        }
    }

    void Interact() 
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if(collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactableLayer);
        return collider == null;

        if(Physics2D.OverlapCircle(targetPos, 0.1f, SolidObjects) != null) {
            return false;
        }
        return true;
    }

    public void LookForward(Vector3 targetPos) {

        var xdiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if(xdiff == 0 || ydiff == 0) {
            Debug.Log("Looking forward");
            animator.SetFloat("moveX", Mathf.Clamp(xdiff, -1f, 1f));
            animator.SetFloat("moveY", Mathf.Clamp(ydiff, -1f, 1f));
        }
        else {
            Debug.LogError("Can't look diagonally");
        }
    }

    public void CheckForEncounters() {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null) {
            if(Random.Range(1, 101) <= 10) {
                Debug.Log("Encountered a wild pokemon");
            }
        }
    }

    public object CaptureState()
    {
        float[] position = new float[] { transform.position.x, transform.position.y };
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);
    }
}
