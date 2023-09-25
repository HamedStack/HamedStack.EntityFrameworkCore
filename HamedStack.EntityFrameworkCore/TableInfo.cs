namespace HamedStack.EntityFrameworkCore;

/// <summary>
/// Represents information about a database table.
/// </summary>
public class TableInfo
{
    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    /// <value>The name of the table.</value>
    public string? TableName { get; internal set; }

    /// <summary>
    /// Gets or sets the schema of the table.
    /// </summary>
    /// <value>The schema of the table.</value>
    public string? TableSchema { get; internal set; }

    /// <summary>
    /// Gets or sets the columns associated with the table.
    /// </summary>
    /// <value>A list of <see cref="TableColumnInfo"/> representing the columns of the table.</value>
    public List<TableColumnInfo> Columns { get; internal set; } = new();
}