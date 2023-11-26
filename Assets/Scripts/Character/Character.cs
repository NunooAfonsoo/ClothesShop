using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance;
    public Inventory Inventory { get; private set; }

    [SerializeField] private Transform characterCenter;
    [SerializeField] private Transform characterVisuals;

    [SerializeField] private Sprite defaultHoodSprite;
    [SerializeField] private Sprite defaultFaceMaskSprite;
    [SerializeField] private Sprite defaultPelvisSprite;

    [SerializeField] private SpriteRenderer hood;
    [SerializeField] private SpriteRenderer faceMask;
    [SerializeField] private SpriteRenderer pelvis;

    [SerializeField] private InteractTextUI interactCanvas;

    [SerializeField] private IntEventSO moneySpentEvent;

    private Rigidbody2D rb;
    private Animator animator;

    private float speed = 7.5f;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Inventory = new Inventory();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(characterCenter.position, Constants.INTERACT_RANGE);
        bool hasInteractableCloseBy = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                hasInteractableCloseBy = true;

                interactCanvas.ShowInteractText(interactable.GetInteractableText());

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                return;
            }
        }
        if (!hasInteractableCloseBy)
        {
            interactCanvas.HideInteractText();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 motionVector = new Vector2(Input.GetAxis(Constants.HORIZONTAL_AXIS), Input.GetAxis(Constants.VERTICAL_AXIS));
        Turn(motionVector);

        Vector2 newPosition = rb.position + motionVector * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        SetAnimation(motionVector);
    }

    private void Turn(Vector2 motionVector)
    {
        if (motionVector.x > 0)
        {
            characterVisuals.rotation = Quaternion.Euler(new Vector3(characterVisuals.rotation.eulerAngles.x, 0, characterVisuals.rotation.eulerAngles.z));
        }
        else if (motionVector.x < 0)
        {
            characterVisuals.rotation = Quaternion.Euler(new Vector3(characterVisuals.rotation.eulerAngles.x, 180, characterVisuals.rotation.eulerAngles.z));
        }

    }

    private void SetAnimation(Vector2 motionVector)
    {
        if (motionVector.x == 0 && motionVector.y == 0)
        {
            animator.SetBool(Constants.MOVE_STATE, false);
        }
        else
        {
            animator.SetBool(Constants.MOVE_STATE, true);
        }
    }

    public void EquipItem(ItemSO itemSO, EquipLocation equipLocation)
    {
        switch (equipLocation)
        {
            case EquipLocation.Hood:
                hood.sprite = itemSO.ItemIcon;
                break;
            case EquipLocation.FaceMask:
                faceMask.sprite = itemSO.ItemIcon;
                break;
            case EquipLocation.Pelvis:
                pelvis.sprite = itemSO.ItemIcon;
                break;
        }
    }

    public void EquipDefaultItem(EquipLocation equipLocation)
    {
        switch (equipLocation)
        {
            case EquipLocation.Hood:
                hood.sprite = defaultHoodSprite;
                break;
            case EquipLocation.FaceMask:
                faceMask.sprite = defaultFaceMaskSprite;
                break;
            case EquipLocation.Pelvis:
                pelvis.sprite = defaultPelvisSprite;
                break;
        }
    }

    public bool IsWearingItem(ItemSO item)
    {
        return item.ItemIcon == hood.sprite || item.ItemIcon == faceMask.sprite || item.ItemIcon == pelvis.sprite;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(characterCenter.position, Vector3.forward, Constants.INTERACT_RANGE);
    }
    #endif
}
