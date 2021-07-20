namespace Exa.Math.ControlSystems {
    // NOTE: This is completely broken
    public class PidAngleController : PidController {
        public PidAngleController(float proportional, float integral, float derivitive)
            : base(proportional, integral, derivitive) { }
    }
}