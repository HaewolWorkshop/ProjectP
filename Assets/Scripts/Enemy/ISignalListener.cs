using UnityEngine;

namespace HaewolWorkshop
{
    public delegate void SignalEvent(Signal signal);
    public delegate void SignalReactionEvent(SignalReaction signal);
    public interface ISignalListener
    {
        /// <summary>
       /// 기척이 발생하면 이 함수를 호출합니다.
       /// </summary>
       /// <param name="signal"></param>
        public void OnSignal(Signal signal);
    }
}