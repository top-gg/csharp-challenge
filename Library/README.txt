This is README file for explanations of the challenge.

* How to Test
1. Open the Library.sln file in Adminstrator and Build the solution.
	You may need to install Nancy and Nancy.Hosting.Self from NuGet.
2. Run the application.
3. Send GET request by using the program like Postman
ex) http://localhost:8000/?lat=68.6647&long=-147.6137&start_date=2021/10/06&end_date=2021/10/05




The program uses NancyFx(Http Server) to handle request and return the response(port:8000, localhost).
When the server is up, the program parses the csv file and 