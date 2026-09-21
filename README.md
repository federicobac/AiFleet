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
This repository contains 20 exercises basic tests. They vary in difficulty, and description. The further along you get, the less handrails will be present and the more you will have to lean on the previous exercises as examples.

### Exercises 1-5
These exercises are found in the BasicAiFleetServiceTests.cs and the logic should be implemented in the BasicAiFleetService.cs. The tests are written, but you will have to add the functions and logic to make the tests pass.
#### 1: Simple Token Addition
#### 2: Time Unit Conversion
#### 3: Boolean Threshold Check
#### 4: Basic Multiplication
#### 5: String Formatting

### Exercises 6-10
These exercises should be created in the AiFleetOperationsServiceTests.cs and the logic should be implemented in the AiFleetOperationsService.cs. These exercises will have the Test Header (Signature and Attributes). Your job is to fill in the test body and then write the code to make it pass.
#### 6: Asymmetric Token Cost Calculator
- Scenario: Reading prompt context is cheaper than generating new code tokens.
- Expected Handling: Input tokens cost $0.01 per 1,000 tokens. Output tokens cost $0.03 per 1,000 tokens. Return total cost as decimal

```csharp
[Theory]
[InlineData(100000, 50000, 2.50)] // 100k input ($1.00) + 50k output ($1.50)
[InlineData(0, 0, 0.00)]
public void CalculateTokenCost_CalculatesAsymmetricRates(int input, int output, decimal expected)
```
#### 7: Context Window Guard
- Scenario: Exceeding an LLM's context limit e.g, 128,000, tokens causes a Context Window Overflow crash.
- Expected Handling: Check if ```currentContextTokens + newPromptTokens <= maxContextLimit.``` Return true if it fits, false if it breaches.
```csharp
[Theory]
[InlineData(115000, 10000, 128000, true)]
[InlineData(115000, 20000, 128000, false)]
public void WillFitInContextWindow_ValidatesMaxLimit(int current, int prompt, int limit, bool expected)
```
#### 8: Token Rate Limiter
- Scenario: Providers enforce Tokens Per Minute (TPM) limits to prevent API abuse.
- Expected Handling: ```If tokensUsedThisMinute is >= 90% of tpmLimit```, return "WARNING: NEAR TPM LIMIT". Otherwise, return "OK".
```csharp
[Theory]
[InlineData(90000, 100000, "WARNING: NEAR TPM LIMIT")]
[InlineData(85000, 100000, "OK")]
public void GetRateLimitStatus_TriggersAtNinetyPercent(int used, int limit, string expected)
```
#### 9: Quick Character-to-Token Estimator
- Scenario: 1 token is roughly equal to 4 characters of code or text.
- Expected Handling: Divide string length by 4.0 and round up (Math.Ceiling). If the input string is null or empty (""), return 0. 
```csharp
[Theory]
[InlineData("Hello World 1234", 4)]  // 16 chars -> 4 tokens
[InlineData("Hello World 12345", 5)] // 17 chars -> 5 tokens (rounded up)
[InlineData("", 0)]
public void EstimateTokenCount_RoundsUpCorrectly(string text, int expected)
```
#### 10: Model Tier Selector
- Scenario: Route lightweight tasks to cheap models and complex tasks to heavy reasoning engines.
- Expected Handling: If difficultyRating is 1 to 3, return "MINI". If 4 or higher, return "REASONING". If <= 0, return "UNKNOWN".
```csharp
[Theory]
[InlineData(2, "MINI")]
[InlineData(5, "REASONING")]
[InlineData(0, "UNKNOWN")]
public void SelectModelTier_RoutesBasedOnDifficulty(int rating, string expected)
```
### Exercises 11-16
These exercises should be created in the AiFleetOperationsServiceTests.cs and the logic should be implemented in the AiFleetOperationsService.cs. Your job is to design test method signatures, assertions, logic, and test data entirely on your own.

#### 11: Prompt Cache Discount 
- Scenario: Re-using systemic prompts costs 50% less due to API prompt caching.
- Write ```CalculateCachedPromptCost(int totalTokens, bool isCached)```. Standard cost is $0.02 per 1,000 tokens. If isCached is true, apply a 50% discount ($0.01 per 1,000 tokens).
#### 12: Context Compression Trigger
- Scenario: When conversation history grows too large, the system must summarize older messages to save space.
- Write ```ShouldCompressContext(int currentTokens, int maxLimit)```. Return true if currentTokens reaches or exceeds 80% of maxLimit. Otherwise, return false.
#### 13: Parallel Tool Calling Allowance
- Scenario: Higher tier AI plans allow agents to execute multiple tool calls in parallel.
- Expected Handling: ```Write CanExecuteParallelTools(int requestedTools, string planTier)```.
- - Plan "ENTERPRISE" allows up to 10 tools.
- - Plan "PRO" allows up to 4 tools.
- - Plan "FREE" allows only 1 tool.
- - Return true if requestedTools is within the plan allowance, otherwise false.
#### 14: Agent Thermal Throttling
- Scenario: If an AI agent runs continuously without pausing, GPU server nodes heat up and require a cool-down delay.
- Expected Handling: Write ```GetCoolDownSeconds(int consecutiveExecutions)```. 
- - If consecutiveExecutions < 5, return 0. 
- - If between 5 and 10, return 30.
- - If > 10, return 120.
#### 15: Fine-Tuning Dataset Validator
- Scenario: To train a custom model checkpoint, a dataset must contain a minimum number of valid prompt-completion pairs.
- Expected Handling: Write ```IsValidDatasetSize(int sampleCount)```. Return true if sampleCount is between 100 and 10,000 (inclusive). If negative, throw an ArgumentException.
#### 16: System Prompt Injection Guard
- Scenario: Protect agents from malicious prompt injection attacks hidden in user code files.
- Expected Handling: Write ```ContainsForbiddenTokens(string promptText)```. If promptText contains "IGNORE PREVIOUS INSTRUCTIONS" or "SYSTEM PROMPT:" (case-insensitive), return true. Otherwise, return false. Make sure to return false if null or empty.

### Exercises 17-20
These exercises are difficult and requires knowledge about Stubs and Interfaces. Create ```AiFleetBillingService.cs```and Create ```AiFleetBillingServiceTests.cs```

#### 17: Batch API Night Discount
- Scenario: Overnight batch jobs (dispatched between 22:00 and 04:00 UTC) receive a 50% discount.
- Expected Handling: Write ```CalculateBatchJobCost(int totalTokens)``` in AiFleetBillingService.cs. Standard rate is $0.02 per 1,000 tokens. If clock hour is >= 22$ or $< 4$, apply a 50% discount.
- Test Requirement: Freeze stub at 23:00 UTC. Verify 100,000 tokens returns $1.00m (instead of $2.00).
#### 18: Weekend Standby Surcharge
- Scenario: Keeping dedicated GPU nodes reserved over the weekend incurs an infrastructure surcharge.
- Expected Handling: Update ```CalculateBatchJobCost```. If _clock.UtcNow.DayOfWeek is Saturday or Sunday, add a flat $10.00 standby surcharge (+ 10.00m) to the bill.
- Test Requirement: Freeze stub on a Saturday at 14:00 UTC. Verify 100,000 tokens returns $12.00m ($2.00 base + $10.00 fee).
#### 19: End-of-Month Token Rollover Expiration
- Scenario: Unused monthly prompt credits expire on the last day of the calendar month at midnight.
- Expected Handling: Write ```AreUnusedCreditsExpired()``` in AiFleetBillingService.cs. Return true if _clock.UtcNow.Day equals the last day of the current month.
- Test Requirement: Freeze stub on 2026-09-30 (September 30th) -> Expect true. Freeze on 2026-09-15 -> Expect false.
#### 20: Peak Hour Compute Surge Multiplier
- Scenario: High demand during peak business hours (14:00 to 18:00 UTC on weekdays) triggers a 1.5x pricing surge.
- Expected Handling: Write ```CalculateSurgeCost(decimal baseCost)``` in AiFleetBillingService.cs. If the day is Monday–Friday AND the hour is between 14:00 and 17:59 UTC, return baseCost * 1.5m. Otherwise, return baseCost.
- Test Requirement: Freeze stub on a Tuesday at 15:00 UTC. Pass $10.00m base cost $\rightarrow$ Expect $15.00m.
