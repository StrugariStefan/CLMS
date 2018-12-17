# Courses/Labs Management System

# Prerequisites
- Install Docker for your OS: https://www.docker.com/products/docker-desktop
- Start Docker on your system
- Test your installation: https://docs.docker.com/docker-for-windows/#test-your-installation
- Read the following documentation in order to understand the development and runtime context:
    - https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/docker-application-development-process/docker-app-development-workflow
    - https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/data-driven-crud-microservice
    
# Project structure
- 


## Endpoints
### User Management
- api/v1/users POST (registers a user)
- api/v1/users/{id} GET, DELETE, PATCH/PUT
- api/v1/groups/{id} GET (get all users from group ex: I3A5)
- api/v1/groups GET
### Content Management
- endpoint 1
- endpoint 2
### etc.

### Development workflow
0. The first time you should clone the project locally. This step should be skipped in future iterations.
1. Checkout to master and pull the latest version from github locally before adding any other code. **This should be done each time you want to make changes starting from the latest master changes:** 
    1. `git branch` -> current branch
    2. `git checkout branch-name` -> move to branch `branch-name`
2. Checkout to a new branch with a name that describes the feature, following the standard **name/feature**: `git checkout -b andrei/add-animations`.
3. Make your desired changes to the code and make commits with those changes: `git commit -m "message that describes commit"`.
4. Push the changes to GitHub to a branch with the same name: `git push -u origin local-branch-name:desired-branch-name-on-github` (e.g., `git push -u origin andrei/add-animations:andrei/add-animations`).
5. Validate that the changes work as expected.
6. If your changes work as expected merge the new branch to the **master** branch. **Merge only if the changes work as expected**. This can be done from the GitHub UI.
