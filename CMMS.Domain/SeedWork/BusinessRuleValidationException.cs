using System;

namespace CMMS.Domain.SeedWork
{
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRule BrokenRule { get; }
        public string Details { get; }

        public BusinessRuleValidationException(string message) : base(message)
        {
        }

        public BusinessRuleValidationException(string message, string details) : base(message)
        {
            this.Details = details;
        }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            this.Details = brokenRule.Message;
        }

        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}
