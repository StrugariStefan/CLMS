# Courses/Labs Management System

# Prerequisites
- Install Docker for your OS: https://www.docker.com/products/docker-desktop
- Start Docker on your system
- Test your installation: https://docs.docker.com/docker-for-windows/#test-your-installation
- Read the following documentation in order to understand the development and runtime context:
    - https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/docker-application-development-process/docker-app-development-workflow
    - https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/data-driven-crud-microservice
    
# Project structure
- The **clms** folder will contain all project code
    - The **docker-compose.yml** file contains the services that compose the project
    - Otherwise said: the containers that make up the Labs Management System project
- Each microservice will have its coresponding folder/project of the form **clms/microservice-name**
    - Each microservice/project will contain a Dockerfile (that should be generated automatically) that describes the images that will be created for that service/microservice

# Running the project
## CLI
- When the **docker-compose.yml** file is run using **docker-compose up --build** command from the location where it is located, Docker will start each image name specified in this file and the service will eventually become up and running; the --build option forces the container to be built again from the sources and incorporate new changes
## IDE
- Follow the instructions from: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/docker-application-development-process/docker-app-development-workflow 

Notes:
- In both cases you might need to see the logs to determine the appropriate IP and/or port at which the services are exposed
- Determining the IP adress of a specific container:
    - docker network inspect <network-name>: this will list the IP addressed for each container
    - docker ps: lists info about the running containers, including the container id
    - docker inspect <container-id>: this will provide container information, including the IP address -- towards the end
- After you manage to determine the IP address, you can use it to access the service and interact with it, most probably through Swagger: http://<ip-address>/swagger/index.html
    

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
- Determine the name of the service that exposes the desired functionality from the docker-compose.yml file: https://github.com/StrugariStefan/CLMS/blob/master/clms/docker-compose.yml
- Determine the service URL: http://<service-name>/api
- Create an utility Class that contains the HttpClient creation logic and obtain a referance to use for generating HTTP requests    
    ```
    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get(int id)
    {
        var client = GetHttpClient();
        return await client.GetStringAsync("http://users.api/api/values/5");
    }

    private static HttpClient GetHttpClient()
    {
        var handler = GetHttpClientHandler();
        HttpClient client = new HttpClient(handler);
        return client;
    }

    private static HttpClientHandler GetHttpClientHandler()
    {
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        return handler;
    }

```
## Using databases

Relational databases
- Use the Database as a Service provided by FII
- Login in your account at FII: https://students.info.uaic.ro/db/
- Create a DB for the service you are developing
- Use the credentials specified in the UI in order to connect to the newly created DB: https://github.com/StrugariStefan/CLMS/blob/master/clms/Users.API/Startup.cs#L38

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
