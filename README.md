![.NET Windows Service](https://github.com/gbrandtio/iot-platform-sensor-data/actions/workflows/dotnet.yml/badge.svg)
[![Windows Service Tests](https://github.com/gbrandtio/iot-platform-sensor-data/actions/workflows/dotnettests.yml/badge.svg)](https://github.com/gbrandtio/iot-platform-sensor-data/actions/workflows/dotnettests.yml)

# iot-platform-sensor-data
This repository includes applications that communicate with sensor open data APIs in order to request, receive, display and analyze environmental data from various sensors and locations.<br><br>
In order to collect the data from the sensors the following are used:<br>
- API:  https://github.com/opendata-stuttgart/meta/wiki/EN-APIs<br>
- Maps: https://maps.sensor.community/

## ./node-red
Inside node-red subfolder you can find the implementation of the specifications by using the node-red framework. Please read `/node-red/Documentation.pdf` for more information.<br>If you wish to setup the project locally, please follow the steps described inside `/node-red/Installation-Guide.pdf`.

## ./c#-win-service
Windows Service to collect the sensor data and periodically store them in a database. For installation steps and documentation read `./c#-win-service/README`.

## ./API
A NodeJS RESTful API that provides all the required endpoints to add, get, remove the environmental data retrieved from the sensors. For documentation read
`./API/README.md`.
