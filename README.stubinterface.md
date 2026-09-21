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
This repository contains 8 exercises related to stub - interface. They vary in difficulty, and description. The further along you get, the less handrails will be present and the more you will have to lean on the previous exercises as examples.

### Exercises 1-4
These exercises are found in the AiFleetIntegrationServiceTests.cs and the logic should be implemented in the AiFleetIntegrationService.cs. The tests are written, but you will have to add the functions and logic to make the tests pass.
#### 1: Deterministic Unique Key Generator
#### 2: Event Notification Spying Stub
#### 3: Configurable Model Feature Flag Evaluator
#### 4: Currency Conversion Rate Double

### Exercises 5-8
These exercises should be created in the AiFleetIntegrationServiceTests.cs and the logic should be implemented in the AiFleetIntegrationService.cs. These exercises will have the Test Header (Signature and Attributes). Your job is to fill in the test body, stub and interface, and then write the code to make it pass.