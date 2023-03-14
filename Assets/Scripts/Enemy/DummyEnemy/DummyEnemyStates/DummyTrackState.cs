using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int)DummyEnemy.DummyEnemyState.Track)]
    public class DummyTrackState : FSMState<DummyEnemy>
    {
        public DummyTrackState(IFSMEntity entity) : base(entity)
        {
        }

        public override void InitializeState()
        {
            base.InitializeState();

            ownerEntity.OnSignalReactionEvent += OnSignal;
            SetMoveTargetToSignalPosition();
        }

        public override void ClearState()
        {
            base.ClearState();

            ownerEntity.OnSignalReactionEvent -= OnSignal;
            ownerEntity.Agent.speed = ownerEntity.Data.MoveSpeed;
        }

        protected virtual void OnSignal(SignalReaction reaction)
        {
            if (!reaction.IsRefreshableFrom(ownerEntity.currentTrackingSignalReaction))
            {
                return;   
            }
            // 새로운 기척으로 갱신
            ownerEntity.currentTrackingSignalReaction = reaction;
            SetMoveTargetToSignalPosition();
        }

        private void SetMoveTargetToSignalPosition()
        {
            ownerEntity.MoveTarget = ownerEntity.currentTrackingSignalReaction.signal.position;
            // 속도는 인지 단계에 따라 빠르게 / 보통으로 조절
            ownerEntity.Agent.speed = ownerEntity.currentTrackingSignalReaction.level == SignalReactionLevel.Strong
                ? ownerEntity.Data.RunSpeed
                : ownerEntity.Data.MoveSpeed;
        }
    }
}