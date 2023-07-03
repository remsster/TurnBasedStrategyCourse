using System;
using System.Collections.Generic;

using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class UnitManager : MonoBehaviour
    {
        private List<Unit> unitList;
        private List<Unit> friendlyUnitList;
        private List<Unit> enemyUnitList;

        public static UnitManager Instance { get; private set; }

        // ----------------------------------------------------------------------------
        // Unity Engine Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one UnitManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            unitList = new List<Unit>();
            friendlyUnitList = new List<Unit>();
            enemyUnitList = new List<Unit>();
        }

        private void Start()
        {
            Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
            Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        // -- Private --

        private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            Debug.Log(unit + " spawned");
            if (unit.IsEnemy)
            {
                enemyUnitList.Add(unit);
            }
            else
            {
                friendlyUnitList.Add(unit);
            }
            unitList.Add(unit);
        }

        private void Unit_OnAnyUnitDead(object sender, EventArgs e)
        {
            Unit unit = sender as Unit;
            Debug.Log(unit + " died");
            if (unit.IsEnemy)
            {
                enemyUnitList.Remove(unit);
            }
            else
            {
                friendlyUnitList.Remove(unit);
            }
            unitList.Remove(unit);
        }

        // -- Public --

        public List<Unit> GetUnitList() => unitList;
        public List<Unit> GetFriendlyUnitList() => friendlyUnitList;
        public List<Unit> GetEnemyUnitList() => enemyUnitList;


    }
}
