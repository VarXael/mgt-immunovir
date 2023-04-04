using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pipette : DeskInteractableObject
{
    
    public float PickedUpPipetteHeight;
    public float DefaultPickedUpPipetteInteractionHeight;
    public GameObject PipetteTipPositionObject;
    public GameObject PipetteRaycastPositionObject;
    [HideInInspector]
    public GameObject currentlyHeldPipetteTip;
    public bool HasPipetteTip
    {
        get
        {
            if (currentlyHeldPipetteTip)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public Quaternion PickedUpPipetteRotation;
    public Quaternion PickedUpPipetteInteractionRotation;
    public LayerMask pipetteInteractable;
    public LayerMask pipetteHeightLayerMask;
    public float maxPipetteLiquid;
    public float currentPipetteLiquid;
    public float pipetteLiquidPerTick;
    public bool IsPipetteFull
    {
        get
        {
            if (maxPipetteLiquid == currentPipetteLiquid)
            {
                return true;
            }
            else return false;
        }
    }
    //public Material PipetteIndicatorColor;
    public LineRenderer LineVisualizer;
    private Rigidbody rb;
    private bool changeMovement;
    private List<TubeSolutionType> solutionTypeContained;
    private List<float> solutionLiquidContained;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LineVisualizer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObtainSolution(TubeSolutionType solutionType, float quantityPerTick)
    {

    }
    public void ReleaseSolution(TubeSolutionType solutionType, float quantityPerTick)
    {

    }

    public override void Interact()
    {
        base.Interact();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.rotation = PickedUpPipetteRotation;
        LineVisualizer.enabled = true;
        GameManager.Instance.ChangeUIActiveState(gameObject,"PipetteUI", true);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        LineVisualizer.enabled = false;
        GameManager.Instance.ChangeUIActiveState(gameObject,"PipetteUI", false);
    }

    public override void WhileInteractingAction()
    {
        base.WhileInteractingAction();
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        changeMovement = true;
        WhileInteractingPipette();
    }
    private void WhileInteractingPipette()
    {
        RaycastHit hit = RayCast(Vector3.down, pipetteInteractable);
        PipetteInteractable p;
        if (!hit.collider.gameObject.TryGetComponent(out p)) return;
        if (hit.collider)
        {
            p = hit.collider.gameObject.GetComponent<PipetteInteractable>();
            p.InteractWithPipette(this);
        }
    }

    public override void StopInteractingAction()
    {
        base.StopInteractingAction();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        changeMovement = false;
    }

    public override void MoveInteractableObject(Vector3 pos)
    {
        base.MoveInteractableObject(pos);
        if (!changeMovement)
        {
            //Debug.Log(CalculatePipetteHeigth());
            rb.MovePosition(new Vector3(pos.x, PickedUpPipetteHeight, pos.z));
            rb.MoveRotation(PickedUpPipetteRotation);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, DefaultPickedUpPipetteInteractionHeight, transform.position.z);
            rb.MoveRotation(PickedUpPipetteInteractionRotation);

        }
    }

    private float CalculatePipetteHeigth()
    {
        RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.down, 10f, pipetteHeightLayerMask);

        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Pipette")) continue;
            else
            {
                Debug.Log(hit[i].collider.name);
                return hit[i].point.y;
            }
        }
        return DefaultPickedUpPipetteInteractionHeight;
    }

}
