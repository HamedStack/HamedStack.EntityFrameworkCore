namespace HamedStack.EntityFrameworkCore;
/// <summary>
/// Represents information about a database table column.
/// </summary>
public class TableColumnInfo
{
    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    /// <value>The name of the column.</value>
    public string? ColumnName { get; internal set; }

    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    /// <value>The data type of the column.</value>
    public string? ColumnType { get; internal set; }
}