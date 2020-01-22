Deploying the Database
-Database is built on MSSQL
-in order for it to be deployed use the CodeFirst approach
-It has two tables NPVPreviousRequests and NPVPreviousResult
-Change the connection string in appsettings.json

VRTest.Api
-You may run it using visual studio 2019
-Built on .Net Core 2.1
-This is the local url 
https://localhost:44389/api/npv
And here is the sample
Method : POST
Content-Type: application/json
In the request body: (Sample Request)
{
	"CashFlow":[500,1000,1500,2000,2500]
	,"UpperBoundDiscountRate":2.5
	,"LowerBoundDiscountRate":1.25
	,"Increment":0.25
	,"InitialCost":1000
}