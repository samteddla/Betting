using MediatR;

namespace SportBet.Service.Message;

// public record GetBetResultQuery(int CardId) : IRequest<ErrorOr<BetResultResponse>>
public class BetResult : IRequest<BetResultResponse>
{

}
public class BetResultResponse
{
    public int BetResultId { get; set; }
}
