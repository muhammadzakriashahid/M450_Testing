# Deployment Environment
### Types of Environments
- Development Environment
- Testing Environment
- Staging Environment
- Production Environment
#### Development Environment
this is practically the workstation of a developer, where he develops the software and only he tests.
### Testing Environment
it's where the developers deploy the code, if it fails it notifies responsible developer, but if it passes the test environment/ci-framework that controls the tests, automatically moves the code to the next deployment environment.
### Staging Environment
also called stage, staging, pre-production environment, it's as close as possible to a production environment, and can be connected to certaing production-services like databases. 
some companies use this to give the customer a preview before changing anything in the production.
### Production Environment
also known as the live-environment, this is where the user interacts directly, also it's the most delicate step
## Explanation of the terms: Patch - Update - Upgrade
a `patch` is an update that fixes a problem in the software, so it affects only a part of the system.
an `update` brings the software up to date, fixes bugs or improves performance. however *the decisive feature of an update, however, is that it does not affect the range of functions or the functionality of the software*
an `upgrade` is when the software receives new stuff, thus leveling it up.
