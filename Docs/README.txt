========== R E A D M E ============

1. Pre-requesits for the development environment
------------------------------------------------
None.


2. Steps to configure the SQL Server connection
-----------------------------------------------
Run the 'Database Script\SqlServer\create_crossover.sql' to setup the 'Crossover' database.


3.Steps to prepare the source code to build properly
-----------------------------------------------------
1. Get the connection string of the Crossover database. 

2. Find the web config file of the api project in the following location
'Source\MessageLogger\MessageLogger.Api\Web.config'  

2.1. Update the connectionString(name='MessageLoggerEntities') to the new database connection string value.

2.2. Edit following in <system.diagnostics> to a valid folder in the hosted machine
<add name="textWriterListener" type="System.Diagnostics.TextWriterTraceListener" initializeData="{path_to_a_valid_folder}\log_message_logger_api.txt"/>

3. Build and publish 'MessageLogger.Api' and 'MessageLogger.Web' projects to IIS.

4. Only if the hosted url of the 'MessageLogger.Api' in IIS is not http://localhost:8080/
Locate and change the following 'appSetting' in Web.config file of the IIS hosted 'MessageLogger.Web' project
<add key="WebApiBaseAddress" value="http://{host:port}/v1"/>


4. Assumptions made and missing requirements
--------------------------------------------
1. Performance of the api is more important than loosing a client session occassionally.
2. Status Code 400:Bad request is set to clients not only when you receive an invalid json, but also when the data in the authorization header is in error(etc, not found, doesn't match expected pattern)


5.Feedback
-----------
Challenging because of the time frame.






