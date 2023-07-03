using System;
using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {

        [SerializeField] private bool isEnemy;
        [SerializeField] private int actionPoints = 2;


        private const int ACTION_POINTS_MAX = 2;

        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;
        private HealthSystem healthSystem;

        public static event EventHandler OnAnyActionPointsChanged;
        public static event EventHandler OnAnyUnitSpawned;
        public static event EventHandler OnAnyUnitDead;

        public GridPosition GridPosition => gridPosition;
        public bool IsEnemy => isEnemy;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
            baseActionArray = GetComponents<BaseAction>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            healthSystem.OnDead += HealthSystem_OnDead;
            OnAnyUnitSpawned?.Invoke(this,EventArgs.Empty);
        }

        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                // Unit changed Grid Position
                GridPosition oldGridPosition = gridPosition;
                gridPosition = newGridPosition;
                LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
            }
        }

        // ----------------------------------------------------------------------------
        // Overridden Methods
        // ----------------------------------------------------------------------------

        public override string ToString()
        {
            return gameObject.name;
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        // -- Private --
        private void SpendActionPoints(int amount)
        {
            actionPoints -= amount;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            if (isEnemy && !TurnSystem.Instance.IsPlayerTurn || !isEnemy && TurnSystem.Instance.IsPlayerTurn)
            {
                actionPoints = ACTION_POINTS_MAX;
                OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        private void HealthSystem_OnDead(object sender, EventArgs e)
        {
            LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition,this);
            Destroy(gameObject);
            OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
        }

        // -- Public --
        public SpinAction GetSpinAction() => spinAction;

        public MoveAction GetMoveAction() => moveAction;

        public BaseAction[] GetBaseActionArray() => baseActionArray;

        public Vector3 GetWorldPosition() => transform.position;

        public void Damage(int damageAmount)
        {
            healthSystem.Damage(damageAmount);
        }

        public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
        {
            if (CanSpendActionPointsToTakeAction(baseAction))
            {
                SpendActionPoints(baseAction.GetActionPointsCost());
                return true;
            }
            return false;
        }

        public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
        {
            return actionPoints >= baseAction.GetActionPointsCost();
        }

        public int GetActionPoints() => actionPoints;



        


        
    }
}

