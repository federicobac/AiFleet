namespace AiFleet.Core.Models.Entities;

using LinqToDB.Mapping;

[Table(Name = "Agents")]
public class AgentEntity
{
    [PrimaryKey, Identity] public int Id { get; set; }
    [Column] public string Name { get; set; } = string.Empty;
    [Column] public string WorkspaceId { get; set; } = string.Empty;
    [Column] public string ModelTier { get; set; } = "MINI"; 
    [Column] public int TokensUsed { get; set; }
    [Column] public bool IsActive { get; set; } = true;
    [Column] public DateTime CreatedAtUtc { get; set; }
    [Column] public DateTime LastActiveUtc { get; set; }
}