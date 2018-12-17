services=( "Users.API" "Notifications.API" )

for service in "${services[@]}"; do
	echo starting: $service
	cd $service
	dotnet run 
	cd ..
done