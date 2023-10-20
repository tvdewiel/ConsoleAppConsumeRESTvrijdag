using System.Net;
using System.Net.Http.Headers;

namespace ConsoleAppConsumeRESTvrijdag
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            client.BaseAddress = new Uri("http://localhost:5035/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var v = await GetVisitorAsync("api/visitor/2");
            //v.Name = "jane II";
            //var res=await UpdateVisitorAsync(v);
            //var v = await GetVisitorsAsync("api/visitor");
            //Visitor visitor = new Visitor();
            //visitor.Birthday = DateTime.Now;
            //visitor.Name = "inge";
            //var l = await CreateVisitorAsync(visitor);
            var res =await DeleteVisitorAsync("2");
        }
        static async Task<Visitor> GetVisitorAsync(string path)
        {
            try
            {
                Visitor visitor = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    visitor = await response.Content.ReadAsAsync<Visitor>();
                }
                return visitor;
            }
            catch (Exception ex) { Console.WriteLine(ex); return null; }
        }
        static async Task<List<Visitor>> GetVisitorsAsync(string path)
        {
            try
            {
                List<Visitor> visitors = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    visitors = await response.Content.ReadAsAsync<List<Visitor>>();
                }
                return visitors;
            }
            catch (Exception ex) { Console.WriteLine(ex); return null; }
        }
        static async Task<Uri> CreateVisitorAsync(Visitor visitor)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/visitor", visitor);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
        static async Task<Visitor> UpdateVisitorAsync(Visitor visitor)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/visitor/{visitor.Id}", visitor);
            response.EnsureSuccessStatusCode();
            visitor=await response.Content.ReadAsAsync<Visitor>();
            return visitor;
        }
        static async Task<HttpStatusCode> DeleteVisitorAsync(string id)
        {
            HttpResponseMessage response=await client.DeleteAsync($"api/visitor/{id}");
            return response.StatusCode;
        }
    }
}