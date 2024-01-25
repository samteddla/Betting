####  scaffold the database

dotnet tool install --global dotnet-ef --version 7.0.0-*

dotnet tool update --global dotnet-ef 

dotnet tool update -g Dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design   
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design  

dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BetWise;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -o Model -c BetContext --context-dir Context -f --force

// defaultConnections must be added
services.AddDbContext<BetContext>(options => options.UseSqlServer(defaultConnections.DefaultConnection));

dotnet ef migrations add Initial -o Migrations -c BettingContext   
dotnet ef database update -c BettingContext
dotnet ef migrations script > create.sql
Then run the script in the database to create the tables.
after that you can make changes to code then you can update the database by running the following command
dotnet ef migrations add Second-Changes -o Migrations -c BettingContext
###
other commands
dotnet ef migrations add [name]	        Create a new migration with the specific migration name.
dotnet ef migrations remove	            Remove the latest migration.
dotnet ef database update	            Update the database to the latest migration.
dotnet ef database update [name]	    Update the database to a specific migration name point.
dotnet ef migrations list	            Lists all available migrations.
dotnet ef migrations script	            Generates a SQL script for all migrations.
dotnet ef migrations has-pending-model-changes	Check if there is any model changes since the last migration.
dotnet ef database drop	                Drop the database.

### the application code 
dotnet new webapi -o API
dotnet new sln -o SAM_TEDLA
dotnet sln SAM_TEDLA.sln add API
dotnet sln SAM_TEDLA.sln add Model

### add the project reference
dotnet add reference ../Model/Model.csproj
### create web project 
dotnet new webapi -o SportBet.Api -f net7.0
dotnet new classlib -o SportBet.Domain -f net7.0
dotnet new classlib -o SportBet.Infrastructure -f net7.0
dotnet new classlib -o SportBet.Application -f net7.0
dotnet new classlib -o SportBet.Contracts -f net7.0
dotnet new classlib -o SportBet.Common -f net7.0
###
dotnet new sln
dotnet sln SportBet.sln add SportBet.Api
dotnet sln SportBet.sln add SportBet.Domain
dotnet sln SportBet.sln add SportBet.Infrastructure
dotnet sln SportBet.sln add SportBet.Application
dotnet sln SportBet.sln add SportBet.Contracts
dotnet sln SportBet.sln add SportBet.Common
###
dotnet add reference ../SportBet.Domain/SportBet.Domain.csproj --project SportBet.Infrastructure/SportBet.Infrastructure.csproj
dotnet add reference ../SportBet.Infrastructure/SportBet.Infrastructure.csproj --project SportBet.Application/SportBet.Application.csproj
dotnet add reference ../SportBet.Application/SportBet.Application.csproj --project SportBet.Contracts/SportBet.Contracts.csproj
###
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation.AspNetCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt

###
Vue.js

C:\App\jdk-21.0.1\bin\java.exe -jar C:\bin\swagger-codegen-cli.jar generate -i http://localhost:5002/swagger/v1/swagger.yaml -l typescript-axios -o .\SportBet.Web\src\api --flatten-inline-schema true

 C:\App\jdk-21.0.1\bin\java.exe -jar C:\bin\swagger-codegen-cli.jar generate -i http://localhost:5002/swagger/v1/swagger.yaml -l typescript-axios -o .\SportBet.Web\src\api --disable-examples --flatten-inline-schema true -t C:\src\codegenerator

npm install
npm i rimraf --save-dev
npm i typescript --save-dev
npx tsc

To use your library in another project, you can install it locally:
copy tsconfig.json and package.json to the root folder div directory i.e
 .\SportBet.Web\src\api6\dist\
then run : 
npm install .\SportBet.Web\src\api6\dist\

 <v-select 
    v-model="selectionId" 
    :items="selectionIds" 
    item-text="name" 
    item-value="matchSelectionId"
    label="Select a match" outlined dense return-object>
</v-select>
const selectionIds = ref([1, 2, 3]);

<v-footer
      app
      name="footer"
    >
      <v-btn
        class="mx-auto"
        variant="text"
        @click="print('footer')"
      >
        Get data
      </v-btn>
    </v-footer>
  </v-app>
const print = (key: string) => {
    console.log(key)
  }

/*
/*
watch(selectedChoices, (val: any) => {
    var length = store.match.matches.length;
    var machIds = store.match.matches.map((m: any) => m.matchId);
    var selectedMatchIds = val.map((c: any) => c.matchId);
    var found = machIds.filter(function(item: number){
        return selectedMatchIds.indexOf(item)==-1;
    });

    if (found.length === 0) {
        if(val.length < length){
            canBuy.value = false;
        }else{
            canBuy.value = true;
        }
        if(val.length >= length){
            console.log('halfandFullTime : ', selectionId.value);
            costs.value = Math.pow(2,val.length -length);
            costs.value *= selectionId.value;
            console.log('pow,half,cost, selectedId : ', Math.pow(2,val.length -length), halfandFullTime.value, costs.value, selectionId.value);
            
        }
    }else{
        canBuy.value = false;
        costs.value = 0;
    }
});*/
 */

  <p>match:
            <pre>{{ store.match }}</pre>
            </p>

// 1. TODO: matchtype full time is should be considered here!
// b.BetMatchType } equals new { r.BetMatchType } into br -- is not right!
// get matchtype from betcard then
// get betresult by matchtype
// then loop through matchId then assign loss or win


// 2. TODO : Create a worker instance update prize to each betcard if 
// the betresult is updated ... always loop from 
// lastupdated date on betresult as key.

// 3. TODO : Create a prize-tabel which contains number of matches won with prize money.

CREATE TABLE [BetCard] (
    [BetCardId] int NOT NULL IDENTITY,
    [PersonId] int NOT NULL,
    [BetDate] datetime2 NOT NULL,
    BetEndDate datetime2 NOT NULL,
    Constraint [PK_BetCard] PRIMARY KEY ([BetCardId]),
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE Payment (
  PaymentId INT PRIMARY KEY,
  BetCardId INT,
  Amount DECIMAL(10, 2) NOT NULL,
  PaymentDate datetime2 NOT NULL,
  CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentId]),
  FOREIGN KEY (BetCardId) REFERENCES BetCard(BetCardId)
);

CREATE TABLE Reward (
  RewardId INT PRIMARY KEY,
  BetCardId INT,
  Amount DECIMAL(10, 2) NOT NULL,
  RewardDate DATE NOT NULL,
  Constraint [PK_Reward] PRIMARY KEY ([RewardId]),
  FOREIGN KEY (BetCardId) REFERENCES BetCard(BetCardId)
);

// TODO: matchtype full time is should be considered here!
        // b.BetMatchType } equals new { r.BetMatchType } into br -- is not right!
        // get matchtype from betcard then
        // get betresult by matchtype
        // then loop through matchId then assign loss or win


git remote add origin git@github.samteddla:samteddla/Betting.git
git branch -M main
git push -u origin main