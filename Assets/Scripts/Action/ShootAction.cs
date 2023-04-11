using System;
using System.Collections.Generic;

using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.Action
{
    public class ShootAction : BaseAction
    {
        [SerializeField] private int maxShootDistance = 7;
        [SerializeField] private float aimRotationSpeed = 10f;

        private enum State
        {
            Amining,
            Shooting,
            Cooloff
        }

        private State state;
        private float stateTimer;
        private Unit targetUnit;
        private bool canShootBullet;

        public event EventHandler<OnShootEventArgs> OnShoot;

        public class OnShootEventArgs : EventArgs
        {
            public Unit targetUnit;
            public Unit shootingUnit;
        }


        private void Update()
        {
            if (!isActive) return;

            stateTimer -= Time.deltaTime;
            switch(state)
            {
                case State.Amining:
                    Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, aimRotationSpeed * Time.deltaTime);
                    break;
                case State.Shooting:
                    if (canShootBullet)
                    {
                        Shoot();
                        canShootBullet = false;
                    }
                    break;
                case State.Cooloff:
                    break;
            }

            if (stateTimer <= 0) { NextState(); }
        }

        private void NextState()
        {
            switch (state)
            {
                case State.Amining:
                    state = State.Shooting;
                    float shootingStateTime = .1f;
                    stateTimer = shootingStateTime;
                    break;
                case State.Shooting:
                    state = State.Cooloff;
                    float cooloffStateTime = .5f;
                    stateTimer = cooloffStateTime;                    
                    break;
                case State.Cooloff:
                    ActionComplete();
                    break;
            }
        }

        private void Shoot()
        {
            OnShoot?.Invoke(this, new OnShootEventArgs { targetUnit = targetUnit, shootingUnit = unit});
            targetUnit.Damage();
        }

        public override string GetActionName() => "Shoot";

        public override List<GridPosition> GetValidActionGridPostionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GridPosition;

            for (int x = -maxShootDistance; x <= maxShootDistance; x++)
            {
                for (int z = -maxShootDistance; z <= maxShootDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > maxShootDistance) continue;
                                        
                    if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                    Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                    // both units are on the same team
                    if (targetUnit.IsEnemy == unit.IsEnemy) continue;

                    validGridPositionList.Add(testGridPosition);

                }
            }

            return validGridPositionList;
        }

        public override void TakeAction(GridPosition gridPosition, Action<bool> onActionComplete)
        {
            ActionStart(onActionComplete);

            targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

            state = State.Amining;
            float aminingStateTime = 1f;
            stateTimer = aminingStateTime;

            canShootBullet = true;
        }
    }
}
