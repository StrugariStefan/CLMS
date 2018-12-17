# Courses/Labs Management System

# Project structure
- The **clms** folder will contain all the project code
- Each microservice will have its coresponding folder/project of the form **clms/microservice-name**
    - Users.API: http://locahost:5001/swagger/index.html
    - Notifications.API: http://locahost:5002/swagger/index.html

# Running the project
- Follow the instructions from if you are not able to start the project by default: https://www.youtube.com/watch?v=N6U_3Dxxkks
Notes:
- Each service will be exposed at **http://localhost:500x/**
- You can access Swagger at: **http://localhost:500x/swagger/index.html**
- Determining the IP adress of a specific service: **clms/microservice-name/Properties/launchSettings.json**
- When adding a new project, make sure to follow the current convention of increasing port numbers: 5001, 5002, etc.    

## Endpoints
### User Management
- http://host:port/swagger/index.html
- api/v1/users POST (registers a user)
- api/v1/users/{id} GET, DELETE, PATCH/PUT
- api/v1/groups/{id} GET (get all users from group ex: I3A5)
- api/v1/groups GET
### Content Management
- endpoint 1
- endpoint 2
### etc.

## Calling a service from another container
- Determine the service URL
- Use the following code logic in order make HTTP calls
```
    public async Task<ActionResult<string>> doSomeWork()
    {
        HttpClient client = new HttpClient();
        return await client.GetStringAsync("http://localhost:<desired-service-port>/api/values/4");
    }
```

## Using databases

Relational databases
- Use the Database as a Service provided by FII
- Login in your account at FII: https://students.info.uaic.ro/db/
- Create a DB for the service you are developing
- Use the credentials specified in the UI in order to connect to the newly created DB: https://github.com/StrugariStefan/CLMS/blob/master/clms/Users.API/Startup.cs

NoSQL databases
- Determine if there is available a free service somehwere that exposes an API to use a NoSQL DB (e.g., MongoDB, etc.); to use the NoSQL DB just as we are using the MySQL DB from the faculty
- ??? To investigate if a container can be used

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
