Deploying the Database
-Database is built on MSSQL
-in order for it to be deployed use the CodeFirst approach
-It has two tables NPVPreviousRequests and NPVPreviousResult
-Change the connection string in appsettings.json of project VRTest.Api

VRTest.Api (https://localhost:44397/api/values)
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

VRTestWeb.Razor (https://localhost:44376/)
  - The web is built on Razor
  - This web connects to VRTest.Api (if there's a problem in connecting to the API, please check the appsettings.json Endpoints:vrtest.api.npv to counter validate if the url is valid)
  -  To input values for Cash Flows you may input the values separated by commas i.e 1000, 2000, 3000, 4000

Disclaimer:
-No validation on the UI
-No additional layer of validation on the server side


vrtest.angular.app (https://localhost:44355/npv)
  - The web is built on angular 5.2
  - This web connects to VRTest.Api (if there's a problem in connecting to the API, please check the appsettings.json Endpoints:vrtest.api.npv to counter validate if the url is valid)
  - Calculate button is disabled until correct value in the fields have been supplied. Cash Flows multiline text box shouldn't be empty, the rest of the fields must be in numerical values
    Upper Bound Discount Rate must be greater than Lower Bound Discount Rate.


-The solution is set to run multiple projects (VRTest.Api, VRTestWeb.Razor, vrtest.angular.app)
-Validation on the server side if the increment is correct with respect to the Upper Bound Discount Rate. The VRTest.Api throws an error if the value is incorrect.
-Run the ng serve before running the solution
