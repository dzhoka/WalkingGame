using System;
namespace WalkingGame
{
    public class Character
    {
        readonly string[] walk1Pose =
        {
            "()",
            "[]",
            "|>"
        };

        readonly string[] walk2Pose =
        {
            "()",
            "[]",
            "/|"
        };

        readonly string[] jumpPose =
        {
            "()",
            "[R",
            ""
        };

        readonly string[] squatPose =
        {
            "",
            "()",
            "[R"
        };

        State state = State.walk1;

        public void SetState(State state)
        {
            this.state = state;
        }

        public State GetState()
        {
            return state;
        }

        public string[] GetPose()
        {
            switch (state)
            {
                case State.walk1:
                    return walk1Pose;

                case State.walk2:
                    return walk2Pose;

                case State.jump:
                    return jumpPose;

                case State.squat:
                    return squatPose;

                default:
                    throw new InvalidOperationException("unknown character state");
            }
        }
    }
}
