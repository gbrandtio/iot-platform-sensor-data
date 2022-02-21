# Sensor Data Service
This service is responsible for quering the sensor.community API periodically in order to fetch the environmental measurements from the sensors. It can be configured to handle the data storage via different methods, to collect data for different countries and can be parametrized for various configs such as log level, file formats etc.

## Contents
- Configurations
- Flow
- Architecture
- Installation

## Configurations
The main configuration of the application is located in `SensorData/app.config` or `SensorDataService.exe.config`.
### App Settings
- `LogPath`: The local path where the application logs will be generated. By default this is set to `SensorDataService/Logs`.
- `TimerInterval`: The period, in milliseconds, that specifies when a complete circle of request/parsing/storage of the data will occur. By default, this is set to 10 minutes. Based on the current API specification, this value can't be less than 5 minutes since the data provided from the API on each request cover a 5-minute period.
- `GEO_API_KEY`: The public API key for Geocode API. To generate your own key you can follow https://developers.google.com/maps/documentation/geocoding/get-api-key.
- `CountryCode`: The 2-letter country code that you want to collect data from.
- `SensorAPI`: The API that publishes the data from the sensors. Documentation: https://github.com/opendata-stuttgart/meta/wiki/EN-APIs
- `DataStorageMode`: The data storage method to be used. This value should equal to one of the `DataStorageMethod`.
- `DataStorageMethod`: The available data storage modes. This modes cannot be changed or extended - you can configure one of them in `DataStorageMode`.
- `FileStorageFormat`: If the `DataStorageMode` is set to `FILE`, this configuration specifies the file format of the file that will hold the data.
- `MaxFileSize`: If the `DataStorageMode` is set to `FILE`, this configuration specifies the maximum file size before a new file gets created.

### Connection Strings
`MsrmntsDBConnectionString`: The path (remote or local) where the Entity framework will create and update the database.
