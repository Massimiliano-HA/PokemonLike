using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }
            else if (input.y != 0)
            {
                input.x = 0;
            }

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);
                StartCoroutine(Move(targetPos));
            }
        }

        /*if(input.GetKeyDown(KeyCode.Space)) {
            Interact();
        }*/

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
}
