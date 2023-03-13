using UnityEngine;

namespace HaewolWorkshop
{ 
    /// <summary>
    /// 방황 상태를 사용하는 Enemy가 구현하는 인터페이스
    /// </summary>
    public interface IWanderable
    {
        public EnemyWanderType WanderType { get; set; }
        public EnemyWanderSpots WanderSpots { get; set; }
    }
}