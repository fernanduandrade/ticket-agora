using AutoMapper;

namespace ETicket.Application.Interfaces.Common;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}