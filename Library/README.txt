This is README file for explanations of the challenge.

* How to Test
1. Open the Library.sln file in Adminstrator and Build the solution.
	You may need to install Nancy and Nancy.Hosting.Self from NuGet.
2. Run the application.
3. Send GET request by using the program like Postman
ex) http://localhost:8000/?lat=-5.0888&long=146.3141&start_date=2021/10/05&end_date=2021/10/08
4. The result would be in json.

* Description
The program uses NancyFx(Http Server) to handle request and return the response(port:8000, localhost).
When the server is up, the program parses the csv file per each line and convert the line to EQData which holds data we interest(time, lat, long, mag, place).
The collection I used is a dictionary with id as key(eqMap).
To save time, I put the ids in a dictionary by date(eqIdsByEventDate).

When the program gets a query, it computes the range of date and search the endpoints by each day of the date.
If the event matches, add to the list to return and then sort from newest to oldest date when return the list.
