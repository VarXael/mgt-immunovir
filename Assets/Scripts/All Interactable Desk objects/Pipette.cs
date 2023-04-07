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
    [SerializeField]
    private List<TubeSolutionType> solutionTypeContained;
    [SerializeField]
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


    public void PutTipOnPipette(PipetteTip tipToPutOn)
    {
        if (currentlyHeldPipetteTip != null) return;
        if (tipToPutOn.hasBeenUsed == true) return;
        currentlyHeldPipetteTip = tipToPutOn.gameObject;
        tipToPutOn.transform.position = PipetteTipPositionObject.transform.position;
        tipToPutOn.transform.parent = transform;
    }
    public void RemoveTipFromPipette()
    {
        if (currentlyHeldPipetteTip == null) return;
        Rigidbody rb = currentlyHeldPipetteTip.GetComponent<Rigidbody>();
        Destroy(currentlyHeldPipetteTip.GetComponent<MeshCollider>());
        currentlyHeldPipetteTip.AddComponent<BoxCollider>();
        rb.isKinematic = false;
        rb.AddTorque(Vector3.right * -5, ForceMode.Impulse);
        currentlyHeldPipetteTip.GetComponent<PipetteTip>().hasBeenUsed = true;
        currentlyHeldPipetteTip.transform.parent = null;
        currentlyHeldPipetteTip.transform.position = new Vector3(currentlyHeldPipetteTip.transform.position.x, currentlyHeldPipetteTip.transform.position.y - 0f, currentlyHeldPipetteTip.transform.position.z);
        currentlyHeldPipetteTip = null;
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
        UIManager.Instance.ChangeUIActiveState(gameObject,"PipetteUI", true);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        LineVisualizer.enabled = false;
        UIManager.Instance.ChangeUIActiveState(gameObject,"PipetteUI", false);
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
        if (!hit.collider) return;
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
    public void AddSolutionInPipette(TubeSolutionType type, float quantity)
    {
        bool doesTypeAlreadyExist = false;
        int indexToMeasure = 0;
        for(int i = 0; i < solutionTypeContained.Count; i++)
        {
            if (solutionTypeContained[i] == type)
            {
                indexToMeasure = i;
                doesTypeAlreadyExist = true;
                break;
            }
        }
        if (doesTypeAlreadyExist)
        {
            if(GetCurrentPipetteLiquid() + quantity > maxPipetteLiquid)
            {
                quantity = maxPipetteLiquid - GetCurrentPipetteLiquid();
            }
            solutionLiquidContained[indexToMeasure] = solutionLiquidContained[indexToMeasure] + quantity;
        }
        else
        {
            solutionTypeContained.Add(type);

            if (GetCurrentPipetteLiquid() + quantity > maxPipetteLiquid)
            {
                quantity = maxPipetteLiquid - GetCurrentPipetteLiquid();
            }

            solutionLiquidContained.Add(quantity);
        }
        currentPipetteLiquid = GetCurrentPipetteLiquid();
        UIManager.Instance.GetUIFromName("PipetteUI").UpdateUI(gameObject);
    }

    private float GetCurrentPipetteLiquid()
    {
        float totalSum = 0;
        for(int i = 0; i<solutionLiquidContained.Count; i++)
        {
            totalSum = totalSum + solutionLiquidContained[i];
        }
        currentPipetteLiquid = totalSum;
        return totalSum;
    }
    private void RemoveSolutionFromPipette(Pipette pipetteRef)
    {
        //GameManager.Instance.pipetteUI.RemoveSolutionToPipetteContainer(solutionQuantity_ul);
    }


}
