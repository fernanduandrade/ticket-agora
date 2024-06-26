namespace ETicket.SharedKernel;

public interface IQuery<out TResponse> : IRequest<TResponse> { }