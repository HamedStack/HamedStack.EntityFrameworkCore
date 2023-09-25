// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HamedStack.EntityFrameworkCore;

/// <summary>
/// Contains extension methods for the <see cref="DbContext"/> class.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Retrieves table information for the given entity type from the specified DbContext.
    /// </summary>
    /// <param name="dbContext">The DbContext instance.</param>
    /// <param name="clrEntityType">The CLR type of the entity for which to retrieve table information.</param>
    /// <returns>A <see cref="TableInfo"/> object containing information about the table associated with the entity, or null if the entity type is not found.</returns>
    public static TableInfo? GetTableInfo(this DbContext dbContext, Type clrEntityType)
    {
        var entityType = dbContext.Model.FindEntityType(clrEntityType);
        if (entityType == null) return null;
        var properties = entityType.GetProperties().ToList();
        var table = entityType.GetTableName();
        var schema = entityType.GetSchema();

        var tableInfo = new TableInfo
        {
            TableName = table,
            TableSchema = schema,
            Columns = properties
                .Select(property => new TableColumnInfo
                {
                    ColumnName = property.GetColumnName(StoreObjectIdentifier.SqlQuery(entityType)),
                    ColumnType = property.GetColumnType(StoreObjectIdentifier.SqlQuery(entityType))
                }).ToList()
        };

        return tableInfo;
    }
}