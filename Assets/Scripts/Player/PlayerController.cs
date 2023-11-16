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

        /*if(input.GetKeyDown(KeyCode.Space)) {
            Interact();
        }*/

        animator.SetBool("isMoving", isMoving);

        if (justTeleported)
        {
            justTeleported = false;
            isMoving = false;
            StopAllCoroutines();
            canMove = false;
        }
    }

    /*void Interact() 
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, InteractableLayer);
        if(collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }*/

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

    private bool IsWalkable(Vector3 targetPos) {
        if(Physics2D.OverlapCircle(targetPos, 0.1f, SolidObjects) != null) {
            return false;
        }
        return true;
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
