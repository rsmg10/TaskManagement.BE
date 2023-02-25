using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MITT.Services.Helpers;

public static class MapperExtention
{
    public static T To<T>(this object from) => from.Adapt<T>();
}

public class MappingRegistration : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        //config.NewConfig<CustomerDto, Customer>();
    }
}

public static class MapsterExtention
{
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);
        return services.AddSingleton<IMapper>(mapperConfig);
    }
}