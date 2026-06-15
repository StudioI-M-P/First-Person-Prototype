using UnityEngine;
using UnityEngine.InputSystem;
public interface IInteractable
{
    void Interact();
    void OnHover();
    void OnHoverExit();
}

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] Camera camHolder;
    [SerializeField] LayerMask mask;

    private InputActions inputActions;
    private InputAction interactAction;

    private float interactDistance = 3f;
    private Color hoverColor = Color.red;
    private IInteractable currentInteractable;
    private Renderer render;
    private Color oColor;

    private void Awake()
    {
        inputActions = new InputActions();
        interactAction = inputActions.Player.Interact;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        interactAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(camHolder.transform.position, camHolder.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance);

        if (Physics.Raycast(ray, out hit, interactDistance, mask) && hit.collider.TryGetComponent(out IInteractable i))
        {
            if(currentInteractable != i)
            {
                Clear();
                currentInteractable = i;
                currentInteractable.OnHover();

                render = hit.collider.GetComponent<Renderer>();
                if (render)
                {
                    oColor = render.material.color;
                    render.material.color = hoverColor;
                }
            }

            if (interactAction.WasPerformedThisFrame()) currentInteractable.Interact();
        }
        else
        {
            Clear();
        }
    }

    void Clear()
    {
        if(currentInteractable != null) currentInteractable.OnHoverExit();
        if(render) render.material.color = oColor;
        currentInteractable = null;
        render = null;
    }
}
