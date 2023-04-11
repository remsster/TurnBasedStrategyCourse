using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Action;
using System;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {

        [SerializeField] private bool isEnemy;


        private const int ACTION_POINTS_MAX = 2;

        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;
        private int actionPoints = 2;

        public static event EventHandler OnAnyActionPointsChanged;

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
            
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        private void Update()
        {

            

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
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

        // -- Public --
        public SpinAction GetSpinAction() => spinAction;

        public MoveAction GetMoveAction() => moveAction;

        public BaseAction[] GetBaseActionArray() => baseActionArray;

        public Vector3 GetWorldPosition() => transform.position;

        public void Damage()
        {
            Debug.Log(transform + " damaged!");
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

