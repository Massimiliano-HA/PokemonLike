using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    private void Start()
    {
        // Assurez-vous que vous référencez le composant Animator de votre joueur
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
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
                
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            Interact();
        }

        animator.SetBool("isMoving", isMoving);
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
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactableLayer);

        return collider == null;
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
}
