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

    /// <summary> 
    /// Adds or updates an entity in the specified <see cref="DbContext"/> based on a condition. 
    /// If the provided entity is not null, the method sets the entity state to <see cref="EntityState.Added"/> 
    /// if the specified condition is true, indicating that the entity should be added to the context. 
    /// Otherwise, it sets the entity state to <see cref="EntityState.Modified"/>, indicating that 
    /// the existing entity should be updated. 
    /// </summary> 
    /// <typeparam name="TEntity">The type of entity to be added or updated.</typeparam> 
    /// <param name="context">The <see cref="DbContext"/> instance to which this method is applied.</param> 
    /// <param name="entity">The entity to be added or updated.</param> 
    /// <param name="addCondition">A delegate that represents the condition for determining whether to add or update the entity.</param> 
    /// <remarks> 
    /// The common use case for the <paramref name="addCondition"/> is to check whether the entity's primary key, 
    /// often represented by the property <c>Id</c>, is equal to zero. For example: 
    /// <code> 
    /// dbContext.AddOrUpdate(someEntity, e => e.Id == 0); // Add if Id is 0, otherwise update 
    /// </code> 
    /// </remarks> 
    /// <seealso cref="DbContext"/> 
    /// <seealso cref="EntityState"/> 
    /// <seealso cref="Func{T, TResult}"/> 
    public static void AddOrUpdate<TEntity>(this DbContext context, TEntity entity, Func<TEntity, bool> addCondition)
    {
        if (entity != null)
            context.Entry(entity).State = addCondition(entity) ? EntityState.Added : EntityState.Modified;
    }
}