@echo off
set /p ImageTag=What tag you will build images?

echo Building App Checkin Image with %ImageTag% TAG

docker build -t docker.totvs.io/app-checkin/notifier:%ImageTag% .
docker tag docker.totvs.io/app-checkin/notifier:%ImageTag% docker.totvs.io/app-checkin/notifier:%ImageTag%

set /p PushImage= push images to docker.totvs.io/app-checkin with TAG %ImageTag%? (Y/N)

IF "%PushImage%"=="Y" (
    docker push docker.totvs.io/app-checkin/notifier:%ImageTag%
)

IF "%PushImage%"=="y" (
    docker push docker.totvs.io/app-checkin/notifier:%ImageTag%
)

echo Done!

pause
