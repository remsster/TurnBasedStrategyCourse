using System;
using UnityEngine;

using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.Core
{ 
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly int shootHash = Animator.StringToHash("Shoot");
        private readonly int isWalkingHash = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            if(TryGetComponent<MoveAction>(out MoveAction moveAction))
            {
                moveAction.OnStartMoving += MoveAction_OnStartMoving;
                moveAction.OnStopMoving += MoveAction_OnStopMoving;
            }

            if (TryGetComponent<ShootAction>(out ShootAction shootAction))
            {
                shootAction.OnShoot += ShootAction_OnShoot;
            }
        }

        private void MoveAction_OnStartMoving(object sender, EventArgs e)
        {
            animator.SetBool(isWalkingHash, true);
        }

        private void MoveAction_OnStopMoving(object sender, EventArgs e)
        {
            animator.SetBool(isWalkingHash, false);
        }

        private void ShootAction_OnShoot(object sender, EventArgs e)
        {
            animator.SetTrigger(shootHash);
        }


    }
}
