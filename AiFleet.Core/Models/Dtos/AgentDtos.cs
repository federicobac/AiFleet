namespace AiFleet.Core.Models.Dtos;

using System;

public record CreateAgentDto(string Name, string WorkspaceId, string ModelTier);
public record AgentDetailsDto(int Id, string Name, string WorkspaceId, int TokensUsed, DateTime CreatedAtUtc);
public record RecordActivityDto(int AgentId);