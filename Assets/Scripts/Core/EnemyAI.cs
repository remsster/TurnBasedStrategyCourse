using System;
using TurnBaseStrategy.Action;
using TurnBaseStrategy.Grid;
using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class EnemyAI : MonoBehaviour
    {

        private enum State
        {
            WaitingForEnemyTurn,
            TakingTurn,
            Busy
        }

        private float timer;
        private State state;

        private void Awake()
        {
            state = State.WaitingForEnemyTurn;
        }

        // ----------------------------------------------------------------------------
        // Unity Engine Methods
        // ----------------------------------------------------------------------------


        private void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnchanged;
        }

        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn) return;
            switch (state)
            {
                case State.WaitingForEnemyTurn:
                    break;
                case State.TakingTurn:
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        if(TryTakeEnemyAIAction(SetStateTakingTurn))
                        {
                            state = State.Busy;
                        }
                        else
                        {
                            // No more actions for enemies
                            TurnSystem.Instance.NextTurn();
                        }
                        
                    }
                    break;
                case State.Busy:
                    break;
            }

        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        private void SetStateTakingTurn()
        {
            timer = 0.5f;
            state = State.TakingTurn;
        }

        private bool TryTakeEnemyAIAction(Unit enemyUnit, System.Action onEnemyAIActionComplete)
        {
            SpinAction spinAction = enemyUnit.GetSpinAction();

            GridPosition actionGridPosition = enemyUnit.GridPosition;

            if (!spinAction.IsValidActionGridPosition(actionGridPosition)) return false;

            if (!enemyUnit.TrySpendActionPointsToTakeAction(spinAction)) return false;
                
            spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
            return true;
        }

        private bool TryTakeEnemyAIAction(System.Action onEnemyAIActionComplete)
        {
            foreach(Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
            {
                if(TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete)) return true;
            }
            return false;
        }

        private void TurnSystem_OnTurnchanged(object sender, EventArgs e)
        {
            if (!TurnSystem.Instance.IsPlayerTurn)
            {
                state = State.TakingTurn;
                timer = 2f;
            }

        }
    }
}
