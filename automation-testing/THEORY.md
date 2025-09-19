# Automation Testing
### Manual Testing vs Automation Testing
- **Manual Testing**: Tests are run manually, all tests are carried out
from the end user's perspective. It is time-consuming and prone to human 
error. But this is mainly done when the Effort to automate is more than the
effort to manually test.
- **Automation Testing**: Tests are run using automated tools and scripts.

| Automation Testing                                    | Manual Testing                                                                  |
| ----------------------------------------------------- | ------------------------------------------------------------------------------- |
| reliable (scripted)                                   | unreliable (human error)                                                        |
| Huge Effort at the start (worth it in the long run)   | Low Effort at the start, ROI (Return on Investment) is smaller in the long run. |
| Execution is simple (automated by trigger)            | requires a lot of resources                                                     |
| Exploratory (free to wander) testing **not** possible | Exploratory testing possible                                                    |
| Performance Testing is possible                       | Performance Testing is **not** possible                                         |
| White-Box                                             | Black-Box                                                                       |
### Key Points for Automation Testing
- Test Automation is useful to improve the following:
	- Efficiency
	- Test-Coverage (Test-Reports)
	- Execution Time (Performance)
- for successful automation-testing you need to:
	- choose a correct testing-tool
	- define and follow/stick to a testing process
	- coordinate manual and automated testing
### Steps to set up automated testing
![Steps](./resources/steps.png)
### Types of Tests
here are the most important types of tests:
	-Functional-Testing (does it work?): confirms a software's features and functions do what they're supposed to
	- Non-Functional-Testing (how well does it work?): checks how well the software performs those functions, evaluating aspects like speed, security, and usability
	- Smoke Testing: testing the most essential parts (no deep testing), to find underlying issues that prevent it to work at all.
	- Regression Testing: you just make sure that everything worked like it used to after a major code change. Such regressions always occur when Software features that previously worked correctly no longer work as intended. Regression testing is typically the largest testing effort in commercial Software development, as numerous details are reviewed in previous software functions.
	- Keyword-driven testing: This approach uses predefined keywords that represent test actions (e.g., “click,” “verify,” etc.) mapped to scripts or functions. based on keywords or predefined actions.
	- Data-driven testing: focuses on running the same set of test actions multiple times with different sets of input data, stored in files like spreadsheets or databases. in simple terms: validating the same logic with many different input values