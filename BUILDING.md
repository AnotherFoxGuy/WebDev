#### Required software:
* Visual Studio 2017/19
* Nodejs
* Yarn
* MySQL

# Install instructions:
### Frontend:
* Open a terminal in the *frontend* folder
* Install the packages with `yarn install`
* Build the project with `yarn build`

### Admin panel
* Import the *pokertournament.sql* into a MySQL database (With a tool like phpmyadmin, adminer etc...)
* Open the .sln file with VS and restore the Nuget packages
* Replace the *ConnectionString* in `server\Tres_poker_management_application\Models\Model.cs`
using this template:  
 `server=<Server IP>;user=<DB user>;database=<Database name>;port=3306;password=<DB password>`
* Close VS and open a terminal in the *server* folder
* Install the packages with `yarn install`
* Build the project with `msbuild /t:WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /p:publishUrl="../publish"`

### Running the app
* The *frontend/dist* folder can be hosted on almost anything (It is 100% static, no need for PHP or anything else)
* The *server/publish* folder needs to be hosted with IIS or XSP
* Start the deepstream by opening a terminal in the *server* folder and running `node server.js`
