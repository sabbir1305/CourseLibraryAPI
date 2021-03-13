using CourseLibrary.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class IqueryableExtentions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,Dictionary<string,PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }
            var orderByAfterSplit = orderBy.Split(',');
            string orderByQuery = "";
            foreach (var clause in orderByAfterSplit.Reverse())
            {
                var trimmedClause = clause.Trim();
                var orderDesc = trimmedClause.EndsWith(" desc");

                var indexOfFirstSpace = trimmedClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedClause : trimmedClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new AccessViolationException($"Key mapping for {propertyName} is missing");
                }

                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException("PropertyMappingValue");
                }
                foreach (var destProperty in propertyMappingValue.DestinationProperties)
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDesc = !orderDesc;
                    }
                    orderByQuery =
                        orderByQuery + (string.IsNullOrWhiteSpace(orderByQuery) ? string.Empty : ", ") 
                        + destProperty + (orderDesc ? " descending" : " ascending");
                }
            }
            return source.OrderBy(orderByQuery);
        }
    }
}
