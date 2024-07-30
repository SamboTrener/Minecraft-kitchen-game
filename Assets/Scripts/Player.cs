using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler OnDropSomething;
    public event EventHandler<OnSelectedTableChangedEventArgs> OnSelectedTableChanged;
    public class OnSelectedTableChangedEventArgs : EventArgs
    {
        public BaseTable selectedTable;
    }

    [SerializeField] private GameInput gameInput;
    int baseSpeed = 5;
    [SerializeField] int rotateSpeed = 10;
    public float SpeedMultiplier { get; set; } = 1;
    [SerializeField] Transform kitchenObjectHoldPoint;

    public bool IsWalking { get; private set; }
    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject
    {
        get => kitchenObject;
        set
        {
            kitchenObject = value;
            if (kitchenObject != null)
            {
                OnPickedSomething?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnDropSomething?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    Vector3 lastInteractDirection;
    public BaseTable SelectedTable { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += HandleInteractions;
    }

    private void HandleInteractions(object sender, EventArgs e)
    {
        if (SelectedTable != null)
        {
            SelectedTable.Interact(this);
        }
    }

    void Update()
    {
        HandleInteractions();
        HandleMovement();
    }

    private void HandleInteractions()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying())
        {
            SetSelectedTable(null);
            return;
        }
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        var interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseTable baseTable))
            {
                if (baseTable != SelectedTable)
                {
                    SetSelectedTable(baseTable);
                }
            }
            else
                SetSelectedTable(null);
        }
        else
            SetSelectedTable(null);
    }

    void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        var moveDistance = baseSpeed * SpeedMultiplier * Time.deltaTime;
        var playerRadius = .7f;
        var playerHight = 2f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);

            if (canMove)
                moveDir = moveDirX;
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                    moveDir = moveDirZ;
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        IsWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    void SetSelectedTable(BaseTable selectedTable)
    {
        this.SelectedTable = selectedTable;

        OnSelectedTableChanged?.Invoke(this, new OnSelectedTableChangedEventArgs { selectedTable = selectedTable });
    }

    public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;

    public void ClearKitchenObject() => KitchenObject = null;

    public bool HasKitchenObject() => KitchenObject != null;
}
