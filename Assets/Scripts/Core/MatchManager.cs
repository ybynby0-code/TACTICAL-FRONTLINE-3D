using System;
using UnityEngine;

namespace TacticalFrontline.Core
{
    public enum MatchPhase { Warmup, Active, Overtime, Complete }
    public enum TeamId { None, Alpha, Bravo }

    public sealed class MatchManager : MonoBehaviour
    {
        [SerializeField, Min(30)] private float roundDuration = 300f;
        [SerializeField] private MatchPhase phase = MatchPhase.Warmup;
        public float TimeRemaining { get; private set; }
        public MatchPhase Phase => phase;
        public event Action<MatchPhase> PhaseChanged;
        public event Action<float> TimerChanged;

        private void Start() => TimeRemaining = roundDuration;

        private void Update()
        {
            if (phase != MatchPhase.Active && phase != MatchPhase.Overtime) return;
            TimeRemaining = Mathf.Max(0f, TimeRemaining - Time.deltaTime);
            TimerChanged?.Invoke(TimeRemaining);
            if (TimeRemaining <= 0f) SetPhase(MatchPhase.Complete);
        }

        public void BeginMatch()
        {
            TimeRemaining = roundDuration;
            SetPhase(MatchPhase.Active);
        }

        public void SetPhase(MatchPhase next)
        {
            if (phase == next) return;
            phase = next;
            PhaseChanged?.Invoke(phase);
        }
    }
}
