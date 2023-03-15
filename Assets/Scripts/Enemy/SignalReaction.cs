namespace HaewolWorkshop
{
    /// <summary>
    /// 인지 단계: 시끄러움과 보통 단계를 나타냄
    /// </summary>
    public enum SignalReactionLevel
    {
        Ignore,
        Weak,
        Strong,
        
    }
    /// <summary>
    /// 인지 반응: 어떤 신호였는지, 신호에 어떻게 반응했었는지 정보
    /// </summary>
    public readonly struct SignalReaction
    {
        public readonly Signal signal;
        public readonly SignalReactionLevel level;

        public SignalReaction(in Signal signal, SignalReactionLevel level)
        {
            this.signal = signal;
            this.level = level;
        }

        /// <summary>
        /// Invalid 하거나, 인지 단계가 더 낮거나, 기척 단계가 더 낮은 경우 false를 반환
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public bool IsRefreshableFrom(SignalReaction old)
        {
            if (old.IsInvalid())
            {
                // 내가 invalid 하지 않으면 무조건 새로 갱신
                return !IsInvalid();
            }

            // 인지 단계가 기존 인지 단계보다 낮으면 갱신 안 함
            if (level < old.level)
            {
                return false;
            }

            // 기척 단계가 기존 기척 단계보다 낮으면 갱신 안 함
            if (signal.level < old.signal.level)
            {
                return false;
            }

            // 인지 단계와 기척 단계가 같거나 더 큰 경우 ...
            // 이 함수를 호출하는 시점이 더 최신의 reaction이므로 갱신
            return true;
        }

        public static readonly SignalReaction Invalid = new SignalReaction(Signal.Invalid, SignalReactionLevel.Ignore);
        public bool IsInvalid() => signal.IsInvalid() || level == SignalReactionLevel.Ignore;
    }
}