using UnityEngine;

public class InteractionTest : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mask;

    private float interactDistance = 3f;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance, mask))
        {
            Debug.Log("Interactable Detected");
        }
    }
}
