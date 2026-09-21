namespace AiFleet.UnitTests;

using System;
using System.Linq;
using Xunit;
using LinqToDB;
using AiFleet.Core.Models.Dtos;
using AiFleet.Core.Models.Entities;
using AiFleet.Core.Services;
using AiFleet.UnitTests.Fixtures;
using AiFleet.UnitTests.Stubs;

public class AiFleetPersistenceServiceTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public AiFleetPersistenceServiceTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    // Ex 1: Time-Stamped Agent Inserter
    [Fact]
    public void CreateAgent_ValidDto_StampsClockAndPersistsToSqlite()
    {
        using var db = _fixture.CreateConnection();
        var fixedTime = new DateTime(2026, 9, 21, 10, 0, 0, DateTimeKind.Utc);
        var service = new AiFleetPersistenceService(db, new FixedAiFleetClockStub(fixedTime));

        int id = service.CreateAgent(new CreateAgentDto("AlphaAgent", "WS-01", "REASONING"));
        var entity = db.GetTable<AgentEntity>().FirstOrDefault(a => a.Id == id);

        Assert.NotNull(entity);
        Assert.Equal("AlphaAgent", entity.Name);
        Assert.Equal(fixedTime, entity.CreatedAtUtc);
    }

    // Ex 2: Fetch Agent Details
    [Fact]
    public void GetAgentById_ExistingAgent_ProjectsToAgentDetailsDto()
    {
        using var db = _fixture.CreateConnection();
        int id = Convert.ToInt32(db.InsertWithIdentity(new AgentEntity { Name = "BetaAgent", WorkspaceId = "WS-01", TokensUsed = 5000 }));
        var service = new AiFleetPersistenceService(db, new FixedAiFleetClockStub(DateTime.UtcNow));

        var dto = service.GetAgentById(id);

        Assert.NotNull(dto);
        Assert.Equal("BetaAgent", dto.Name);
        Assert.Equal(5000, dto.TokensUsed);
    }

    // Ex 3: Overnight Registration Credit
    [Fact]
    public void RegisterBatchAgent_OvernightClock_AppliesTenThousandBonusCredit()
    {
        using var db = _fixture.CreateConnection();
        var overnightTime = new DateTime(2026, 9, 21, 23, 0, 0, DateTimeKind.Utc);
        var service = new AiFleetPersistenceService(db, new FixedAiFleetClockStub(overnightTime));

        int id = service.RegisterBatchAgent(new CreateAgentDto("NightOwl", "WS-02", "MINI"));
        var entity = db.GetTable<AgentEntity>().FirstOrDefault(a => a.Id == id);

        Assert.NotNull(entity);
        Assert.Equal(-10000, entity.TokensUsed);
    }

    // Ex 4: Activity Timestamp Updater
    [Fact]
    public void RecordActivity_ExistingAgent_UpdatesLastActiveUtcToClockTime()
    {
        using var db = _fixture.CreateConnection();
        var fixedTime = new DateTime(2026, 9, 21, 14, 30, 0, DateTimeKind.Utc);
        int id = Convert.ToInt32(db.InsertWithIdentity(new AgentEntity { Name = "RunnerAgent" }));
        var service = new AiFleetPersistenceService(db, new FixedAiFleetClockStub(fixedTime));

        bool updated = service.RecordActivity(new RecordActivityDto(id));
        var entity = db.GetTable<AgentEntity>().FirstOrDefault(a => a.Id == id);

        Assert.True(updated);
        Assert.Equal(fixedTime, entity!.LastActiveUtc);
    }

    // Ex 5: Workspace Active Counter
    [Fact]
    public void GetActiveAgentCount_WorkspaceWithMixedStatuses_ReturnsOnlyActiveCount()
    {
        using var db = _fixture.CreateConnection();
        db.Insert(new AgentEntity { Name = "A1", WorkspaceId = "WS-CNT", IsActive = true });
        db.Insert(new AgentEntity { Name = "A2", WorkspaceId = "WS-CNT", IsActive = false });
        var service = new AiFleetPersistenceService(db, new FixedAiFleetClockStub(DateTime.UtcNow));

        int count = service.GetActiveAgentCount("WS-CNT");
        Assert.Equal(1, count);
    }   

    // Ex 6: Basic Agent Search Facet
    [Fact]
    public void SearchAgents_FiltersByProvidedFacetProperties()
    {
    }

    // Ex 7: Token Usage Threshold Filter Facet
    [Theory]
    [InlineData("WS-TH1", 50000, 1)]
    [InlineData("WS-TH1", 100000, 0)]
    public void GetHighUsageAgents_ReturnsOnlyAgentsMeetingThreshold(string wsId, int minTokens, int expectedCount)
    {
    }

    // EX 8: Weekend Token Update Multiplier
    [Fact]
    public void AddTokens_AppliesMultiplierOnWeekend()
    {
    }

    // Ex 9: Workspace Usage Aggregation
    [Fact]
    public void GetWorkspaceSummary_AggregatesTokensAndActiveCount()
    {
    }

    // Ex 10: Workspace Capacity Guard
    [Theory]
    [InlineData(4, true)]
    [InlineData(5, false)]
    public void CheckWorkspaceCapacity_ValidatesMaxFiveAgentLimit(int currentActiveCount, bool expectedCanAdd)
    {
    }
}