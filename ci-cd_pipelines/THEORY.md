# Automatisiertes Testen und Deployen
### Terms to learn/understand:
- **Pipeline**: Top-Level component where you declares `Stages` and `Jobs`
- **Stage**: Describes each phase in the pipeline, it consists of multiple `Jobs`
- **Jobs**: a Process inside of a `Stage`, an example would be *compiling the code*
- **Runner**: open-source application that runs each individual `Jobs` 
### Pipeline setup Tutorial
#### Step 1
requirements: gitlab/github project and a SSH Key.
#### Step 2
after clicking on setup ci/cd a `yaml` file is created, wich describes the pipeline steps.
**the file name is `.gitlab-cy.yml`, and if you create it locally you need to watch the naming and the path, since it's supposed to be in the main directory of the repository**.
here a generated template:
```yml
stages:          # List of stages for jobs, and their order of execution
  - build
  - test
  - deploy
build-job:       # This job runs in the build stage, which runs first.
  stage: build
  script:
    - echo "Compiling the code..."
    - echo "Compile complete."
unit-test-job:   # This job runs in the test stage.
  stage: test    # It only starts when the job in the build stage completes successfully.
  script:
    - echo "Running unit tests... This will take about 60 seconds."
    - sleep 60
    - echo "Code coverage is 90%"
Optional:
lint-test-job:   # This job also runs in the test stage.
  stage: test    # It can run at the same time as unit-test-job (in parallel).
  script:
    - echo "Linting code... This will take about 10 seconds."
    - sleep 10
    - echo "No lint issues found."
deploy-job:      # This job runs in the deploy stage.
  stage: deploy  # It only runs when *both* jobs in the test stage complete successfully.
  environment: production
  script:
    - echo "Deploying application..."
    - echo "Application successfully deployed."
```
#### Step 3
make sure you have a build-tool in your project, for example `gradle` or `maven`