using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSurce, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}