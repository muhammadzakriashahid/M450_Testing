# Basics of Testing and Testing in Process Models
### why Tests?
- nowdays software is used in almost every device, machine or system. To ensure that the software is reliable and has good quality, we need to test it.
### Errors vs Mangel
- Errors: an error is when a requirement is not met (actual behaviour does not match what was specified in the requirements), you know what was output going to be.
- Mangel: a defect is when a requirement is not adequately (to satisfactory extent) met, for example a calculation is run correctly, but the result is not displayed correctly. here you don't know exactly, so something is missing.
- a better example for Mangel would be something missing, like it's not a exactly an error, but would be good to have.
### Error Masking
- occurs when one error/fault hides/prevents another underlying error from being detected. for example, in a calculator the function can divide by zero, but the input function does not allow the user to input zero, so the division by zero error is masked. another example would be if you save numbers as strings, and a you want to summ all those numbers. a test is something else then what i think.
### Criteria for good Tests
- since you cannot test everything, only some tests are executed. so you should write tests that:
    - cover much code as possible (code coverage).
    - don't test the same thing multiple times (redundant tests, for example testing the same function with the same inputs multiple times)
    - are independent from each other (if one test fails, it should not cause other tests to fail)
    - show a high probability of finding errors (for example, testing edge cases, for example, if a function takes an integer as input, test with negative numbers, zero, and very large numbers)
- you should also consider the level of damage that can be done, so if the damage is high, you should test more thoroughly.
### Testing in Process Models
- we will be looking at the 2 main process models, since they changed/influenced the type of testing that is done.
#### V-Model as a prototype for different types of tests
- the V-Model is an extension of the "waterfall model", where testing comes into play at the end of a project. Here Testing is equated with development
- so there are processes and tests, for each process there is a corresponding test.
- on the left side of the V there are:
    - Requirements definition: wishes and requirements of client
    - Functional system design: here requirements are mapped to functions and dialogs, in simple terms ...
    - Technical system design: here the technical implementation is designed (system is divided into components, interfaces are defined), but your not coding yet.
    - Component specification: where each subsystem is described in detail, for example ...
    - Programming: where each building block (module, class, etc.) is programmed in a programming language
- on the right branch there are:
    - Unit test checks whether each elementary software module meets its specifications.
    - Integration test checks whether groups of components interact correctly.
    - System test checks whether the system as a whole meets the requirements.
    - Acceptance test checks whether the system is accepted as correct by the customer.
- here testing is equivalent to development, as both are done in parallel and inform each other.
#### SCRUM as an example of iterative testing
- here the main idea is to improve the product trough each iteration of a software. the product is not developed in one pice, but in sequence of versions, wich makes development agile.