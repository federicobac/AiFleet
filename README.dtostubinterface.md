# **Test-Driven Development (TDD)**. 
TDD is a Software Engineering methodology where software developers write automated tests before writing the logical or functional implementation. 

Instead of writing production code and testing it later, we follow the **Red-Green-Refactor** cycle:
1. **Red:** Write a unit test *first* and watch it fail (or fail to compile).
2. **Green:** Write the minimal code necessary to make the test pass.
3. **Refactor:** Clean up, optimize, and organize the code structure while ensuring all tests continue to pass.

### Why we do it
TDD prevents software regressions, forces modular system design, creates clear system documentation, and reduces debugging time by catching edge-case bugs early in the cycle.

*See:* [The Six Benefits of Test-Driven Development](https://www.conformiq.com/resources/blog-the-six-benefits-of-test-driven-development-05-16-2023) 

### Why it helps AI Agents
Why It Helps AI Agents: AI coding agents excel at writing implementations when given rigid deterministic guardrails. Enforcing TDD gives agents an objective feedback loop—they can repeatedly run tests, identify failures, and self-correct their generated code without human intervention or hallucinations going unnoticed. 

*See:* [Agentic Coding Handbook - TDD Workflow](https://tweag.github.io/agentic-coding-handbook/WORKFLOW_TDD/)

---  

## Exercises
This repository contains 15 exercises related to dtostubinterface. They vary in difficulty, and description. The further along you get, the less handrails will be present and the more you will have to lean on the previous exercises as examples.

### Exercises 1-5
These exercises are found in the AiFleetPersistenceServiceTest.cs and the logic should be implemented in the AiFleetPersistenceService.cs. The tests are written, but you will have to add the functions and logic to make the tests pass.
#### 1: Time-Stamped Agent Inserter
#### 2: Fetch Agent Details
#### 3: Overnight Registration Credit
#### 4: Activity Timestamp Updater
#### 5: Workspace Active Counter

### Exercises 6-10
These exercises should be created in the AiFleetPersistenceServiceTest.cs and the logic should be implemented in the AiFleetPersistenceService.cs. These exercises will have the Test Header (Signature and Attributes). Your job is to fill in the test body and then write the code to make it pass.

#### 6: Basic Agent Search Facet
- Scenario: Dynamically search SQLite records based on non-null properties in a filter DTO.
- DTOs:
```csharp 
public record AgentSearchFacetDto(string? WorkspaceId, string? ModelTier, bool? OnlyActive);
public record AgentSearchResultDto(int Id, string Name, string ModelTier, bool IsActive);
```
- Expected Handling: Write ```SearchAgents(AgentSearchFacetDto facet)```. 
Query db.GetTable<AgentEntity>().AsQueryable(), dynamically append .Where(...) clauses for non-null facet fields, project, and return List<AgentSearchResultDto>.
```csharp
[Fact]
public void SearchAgents_FiltersByProvidedFacetProperties()
```
#### 7: Token Usage Threshold Filter Facet
- Scenario: Filter workspace agents that have surpassed a specific token usage limit.
- DTOs:
```csharp 
public record TokenThresholdFacetDto(string WorkspaceId, int MinTokensUsed);
```
- Expected Handling: Write ```GetHighUsageAgents(TokenThresholdFacetDto facet)```. Query SQLite for rows matching WorkspaceId == facet.WorkspaceId and TokensUsed >= facet.MinTokensUsed. 
Return List<AgentDetailsDto>.
```csharp
[Theory]
[InlineData("WS-TH1", 50000, 1)]
[InlineData("WS-TH1", 100000, 0)]
public void GetHighUsageAgents_ReturnsOnlyAgentsMeetingThreshold(string wsId, int minTokens, int expectedCount)
```
#### 8: Weekend Token Update Multiplier
- Scenario: Apply a 1.5x bonus multiplier to tokens added on Saturday or Sunday.
- DTOs:
```csharp 
public record AddTokensDto(int AgentId, int TokensToAdd);
```
- Expected Handling: Write ```AddTokens(AddTokensDto dto)```. Check _clock.UtcNow.DayOfWeek. If weekend, multiply TokensToAdd by 1.5. Update SQLite TokensUsed total and return new token balance.
```csharp
[Fact]
public void AddTokens_AppliesMultiplierOnWeekend()
```
#### 9: Workspace Usage Summary
- Scenario: Aggregate aggregate workspace usage statistics from SQLite into a summary DTO.
- DTOs:
```csharp 
public record WorkspaceSummaryDto(string WorkspaceId, int TotalTokensUsed, int ActiveAgentCount);
```
- Expected Handling: Write ```GetWorkspaceSummary(string workspaceId)```. Query SQLite for agents in workspaceId, compute total token sum and active count, and return WorkspaceSummaryDto.
```csharp
[Fact]
public void GetWorkspaceSummary_AggregatesTokensAndActiveCount()
```
#### 10: Workspace Capacity Guard
- Scenario: Verify whether a workspace has reached its maximum quota of 5 active agents.
- DTO:
```csharp 
public record CapacityCheckDto(string WorkspaceId, bool CanAddMore, int CurrentCount);
```
- Expected Handling: Write ```CheckWorkspaceCapacity(string workspaceId)```. Count active agents in SQLite for workspaceId. Return CapacityCheckDto where CanAddMore is true if count =< 5.
```csharp
[Theory]
[InlineData(4, true)]
[InlineData(5, false)]
public void CheckWorkspaceCapacity_ValidatesMaxFiveAgentLimit(int currentActiveCount, bool expectedCanAdd)
```
### Exercises 11-16
These exercises should be created in the AiPersistenceServiceTests.cs and the logic should be implemented in the AiPersistenceService.cs. Your job is to design test method signatures, assertions, logic, DTO's, and test data entirely on your own.

#### 11: Calculated Billing Invoice DTO
- Scenario: Calculate workspace invoice cost from stored SQLite tokens at $0.02 per 1,000 tokens.
- Expected Handling: Write ```CalculateWorkspaceBill(string workspaceId)```. Query total TokensUsed in SQLite for workspaceId, calculate TotalCost, and return WorkspaceBillDto.

#### 12: Top Token Consumer Query
- Scenario: Retrieve the agent consuming the highest number of tokens within a workspace
- Expected Handling: Write ```GetTopConsumer(string workspaceId)```. Query SQLite ordered by TokensUsed descending, project top row to AgentDetailsDto, and return null if workspace is empty.

#### 13: Model Tier Downgrade Batch
- Scenario: Bulk downgrade unused reasoning models (TokensUsed == 0) to the lightweight "MINI" tier.
- Expected Handling: Write ```DowngradeUnusedAgents(string workspaceId)```. Execute batch update in SQLite setting ModelTier = "MINI" where ModelTier == "REASONING" and TokensUsed == 0. Return DowngradeResultDto.

#### 14: Fine-Tuning Sample Qualifier
- Scenario: Query active agents that have accumulated enough telemetry tokens for fine-tuning (=> 50,000$).
- Expected Handling: Write ```GetFineTuningQualifiers()```. Query SQLite for active agents where TokensUsed >= 50000 and return List<AgentDetailsDto>.

#### 15: Duplicate Name Guard
- Scenario: Prevent creating multiple agents with duplicate names inside the same workspace.
- Expected Handling: Write ```SafeCreateAgent(CreateAgentDto dto)```. Check SQLite for matching Name and WorkspaceId. If exists, throw InvalidOperationException; otherwise insert and return generated ID.

