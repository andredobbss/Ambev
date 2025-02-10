using MongoDB.Driver;
using System.Globalization;

namespace Ambev.Infraestructure.Repositories.Filter;

public class MongoFilterService<TEntity> where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;

    public MongoFilterService(IMongoCollection<TEntity> collection)
    {
        _collection = collection;
    }

    public async Task<List<TEntity>> ApplyFilters(Dictionary<string, string> filters)
    {
        var filterDefinitions = new List<FilterDefinition<TEntity>>();

        foreach (var filter in filters)
        {
            string key = filter.Key;
            string value = filter.Value;

            // Filtro para campos string
            if (IsStringField(key))
            {
                if (value.StartsWith("*") && value.EndsWith("*"))
                {
                    // Contém
                    filterDefinitions.Add(Builders<TEntity>.Filter.Regex(key, new MongoDB.Bson.BsonRegularExpression(value.Trim('*'), "i")));
                }
                else if (value.StartsWith("*"))
                {
                    // Termina com
                    filterDefinitions.Add(Builders<TEntity>.Filter.Regex(key, new MongoDB.Bson.BsonRegularExpression($"{value.Trim('*')}$", "i")));
                }
                else if (value.EndsWith("*"))
                {
                    // Começa com
                    filterDefinitions.Add(Builders<TEntity>.Filter.Regex(key, new MongoDB.Bson.BsonRegularExpression($"^{value.Trim('*')}", "i")));
                }
                else
                {
                    // Igualdade exata
                    filterDefinitions.Add(Builders<TEntity>.Filter.Eq(key, value));
                }
            }
            // Filtro para campos numéricos e de data
            else if (IsNumericOrDateField(key))
            {
                var baseKey = key.Replace("_min", "").Replace("_max", "");
                if (key.StartsWith("_min"))
                {
                    filterDefinitions.Add(Builders<TEntity>.Filter.Gte(baseKey, ConvertValue(baseKey, value)));
                }
                else if (key.StartsWith("_max"))
                {
                    filterDefinitions.Add(Builders<TEntity>.Filter.Lte(baseKey, ConvertValue(baseKey, value)));
                }
            }
            // Filtro para igualdade exata
            else
            {
                filterDefinitions.Add(Builders<TEntity>.Filter.Eq(key, ConvertValue(key, value)));
            }
        }

        var finalFilter = filterDefinitions.Any() ? Builders<TEntity>.Filter.And(filterDefinitions) : Builders<TEntity>.Filter.Empty;
        return await _collection.Find(finalFilter).ToListAsync();
    }

    // Métodos auxiliares
    private bool IsStringField(string fieldName)
    {
        var property = typeof(TEntity).GetProperty(fieldName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        return property != null && property.PropertyType == typeof(string);
    }

    private bool IsNumericOrDateField(string fieldName)
    {
        var property = typeof(TEntity).GetProperty(fieldName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (property == null) return false;

        var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        return type == typeof(int) || type == typeof(decimal) || type == typeof(double) ||
               type == typeof(float) || type == typeof(long) || type == typeof(DateTime);
    }

    private object ConvertValue(string fieldName, string value)
    {
        var property = typeof(TEntity).GetProperty(fieldName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (property == null) return value;

        var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        return type == typeof(DateTime) ? DateTime.Parse(value, CultureInfo.InvariantCulture) : Convert.ChangeType(value, type);
    }
}

