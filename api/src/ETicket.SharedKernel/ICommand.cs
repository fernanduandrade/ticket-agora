namespace ETicket.SharedKernel;

public interface ICommand<out TResponse> : IRequest<TResponse> {}