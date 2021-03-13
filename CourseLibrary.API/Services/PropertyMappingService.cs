using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id",new PropertyMappingValue(new List<string>(){ "Id" }) },
                { "MainCategory",new PropertyMappingValue(new List<string>(){ "MainCategory" }) },
                { "Name",new PropertyMappingValue(new List<string>(){ "FirstName", "LastName" }) },
                { "Age",new PropertyMappingValue(new List<string>(){ "DateOfBirth" },true) }
            };

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<AuthorDto, Author>(_authorPropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSurce, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSurce, TDestination>>();
            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot fin exact property mapping instance for<{typeof(TSurce)},{typeof(TDestination)}>");

        }
    }
}
