using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] Camera _camera;

    public void Interact()
    {
        Debug.Log("You Interacted!");
        this.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce( _camera.transform.forward * 10, ForceMode.Impulse);
    }

    public void OnHover()
    {
        
    }

    public void OnHoverExit()
    {
        
    }
}
 


