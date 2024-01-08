using System.Diagnostics.CodeAnalysis;
using CashFlow.Domain.Aggregates;

namespace CashFlow.Domain.Exceptions;

public class CashFlowNotFoundException : Exception
{
    private const string CandidateNotFoundMessage = "Title not found.";

    public CashFlowNotFoundException(string message) : base(message)
    {
    }

    public static void ThrowWithTitleNotFoundMessageIfNull([NotNull] BillToPay? title)
    {
        if (title == null) throw new CashFlowNotFoundException(CandidateNotFoundMessage);
    }

    public static void ThrowWithTitleNotFoundMessageIfNull([NotNull] BillToReceive? title)
    {
        if (title == null) throw new CashFlowNotFoundException(CandidateNotFoundMessage);
    }
}