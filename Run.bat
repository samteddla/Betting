docker-compose -f .\docker-compose.yaml -f .\docker-compose-override.yaml down

docker rmi sportbet/sportbet-app-1.0
docker rmi sportbet/api

cd SportBet.Web.Vue
echo "Building Vue app image - sportbet/vue-app"
docker build -t sportbet/vue-app-1.0 .  
pause
cd ..
echo "Create docker apps"
pause
docker-compose -f .\docker-compose.yaml -f .\docker-compose-override.yaml build
docker-compose -f .\docker-compose.yaml -f .\docker-compose-override.yaml up -d

echo "Start database migration"
pause

cd SportBet.Xstart

echo "Run database migration"
dotnet run database create

echo "Run database seed"
pause
dotnet run database sample