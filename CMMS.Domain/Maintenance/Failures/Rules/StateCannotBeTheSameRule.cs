using CMMS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Domain.Maintenance.Failures.Rules
{
    public class StateCannotBeTheSameRule : IBusinessRule
    {
        private readonly FailureState _previousState;
        private readonly FailureState _nextState;

        public StateCannotBeTheSameRule(FailureState previousState, FailureState nextState)
        {
            _previousState = previousState;
            _nextState = nextState;
        }

        public string Message => "State cannot be the same as the previous";

        public bool IsBroken() => _previousState == _nextState;
     
    }
}
