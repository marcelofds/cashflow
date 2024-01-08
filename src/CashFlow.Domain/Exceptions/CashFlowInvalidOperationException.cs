namespace CashFlow.Domain.Exceptions;

public class CashFlowInvalidOperationException : Exception
{
    public CashFlowInvalidOperationException(string message) : base(message)
    {
    }
}