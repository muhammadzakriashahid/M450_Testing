## Documentation
### Introduction
In this project, I mainly worked on a quiz-app where I used Angular for the frontend and C# .NET for the backend. I used the open trivia database API to fetch quiz questions and display them to users. So you would get random 5 questions from the API and answer them. After answering all the questions, you would get your score. I also implemented user authentication using JWT tokens to secure the API endpoints.
### Unit-Testing
I used Moq for mocking dependencies in my unit tests. like for the `UserService` and the `QuizService`.
the goal of the mock was to that the unit tests would not depend on the actual implementations of these services, making the tests more reliable and faster. For example, in the `UserService` tests, I mocked the `IUserRepository` to return predefined user data when certain methods were called.
like for registering a user, i used a mock setup where i just registered a user at the start of the tesst. here a code example: `mock.Setup(s => s.Register("user", "pw")).Returns(true);`
I had to use Callbase when i wanted the default behavior for non-mocked methods.
I also had an issue in the AuthController with testing the JWT, since i had never knew that i can test JWT tokens before this project, so i had to do some research on how to test JWT tokens in unit tests.
And I think that i Understood it, but actually i didn't. because the token is signed and contains timestamps and unique values, so the token string changes every run, making it impossible to have a fixed expected value for comparison in tests. so instead of spending a lot of time here i mainly focused on other stuff. I also did not test the `Program.cs` file because it mainly contains configuration code that is not typically unit tested.
### Class Diagram

## Test-konzept nach IEEE 829
#### Introduction 
Project: Quiz Web API (backend .NET, frontend Angular). Main features: user registration/login (JWT), fetch quiz questions from external API, filter field, provide own endpoints, answer quiz, store solved quizzes.

#### Test Items (what to test)
- AuthController: register, login (check responses and status codes)
- QuizController: get questions, submit answers, mark solved
- UserService/QuizService: logic for authentication, score, saving solved quizzes
- Error cases: invalid input, unauthorized access, external API failure

#### Out of scope (what not to test)
- External trivia API itself (mock responses)
- Program.cs (no unit tests here)
- Exact JWT token string (tokens keep freaking changing)

#### Test types and tools
- Unit tests: xUnit
- Mocking: Moq
- Coverage: coverlet, codecov
- CI: GitHub Actions to build, run tests and upload reports

#### approach
- Unit tests for controllers and services using mocked dependencies.
- Component tests for controller + service interactions with mocks.
- TDD: write failing test, implement code, refactor where practical.

#### Example test cases
1. Register new user -> returns 201 and user id.
2. Register existing user -> returns 400 with error message.
3. Login correct credentials -> returns 200 and an object containing a Token property.
4. Login wrong credentials -> returns 401.
5. Get questions -> returns 5 questions (mock external API).
6. Submit answers -> returns score and marks quiz solved for user.
7. Service handles external API timeout -> controller returns 503.

#### Pass / Fail criteria
- Unit tests: pass all tests.
- Major bugs: none open for tested features.
- Coverage: aim >= 60% on core logic (controllers + services).
- CI pipeline must fail on test failures.

#### Test deliverables
- Test code in /tests
- CI test report artifacts
- Full Coverage report (codecov)
- Short test report in repository while building

#### Environment needs
- .NET SDK 8.0 or above for backend
- Node/Angular/npm for frontend
- Internet for external API calls

#### Schedule (example)
- Weekend 1: write tests for QuizController and QuizService, add CI coverage
- Weekend 2: write unit tests for AuthController and UserService
- Weekend 3: fix issues, add small integration test if time

#### Responsibilities
- at least 3 PRs with tests and code review comments
- CI workflow and coverage setup

## Reflection
I never had to write this many unit tests before, and at certain point i was overwhelmed, i even thought about giving up, and making AI generate the test for me, but then i wouldn't learn much. so i started by doing TDD, writing tests first, and then the code. this was confusing since i didn't know what the code should look like yet, like i didn't have an idea yet. so i watched some videos about TDD wich helped a bit. I also had to learn how to use Moq, since i never used it before. I faced some issues with mocking the JWT token generation and validation, since the tokens are dynamic and signed, making it hard me to test it. The only downside about TDD is that the test fail at first, since the code is not implemented yet, and i don't like seeing error warnings. So i will be avoiding TDD as much as possible in the future. doing Code Reviews was nothing new, i already do that at my job, almost everyday. but setting up the pipeline was new and fun, since i wanted tolearn it but didn't find the time. i will be working on the .gitignore file more, since i failed to sset it up this time. overall i learned a lot about unit testing, mocking, and CI/CD pipelines.