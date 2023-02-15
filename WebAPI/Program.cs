using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HelloWorld
{
    class Books
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }

        [JsonProperty("released")]
        public string Released { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepos();
        }

        private static async Task ProcessRepos()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("There are 10 books in the shelf. Give me a number from 1 to 10. I will show you the book's information. Press Enter without writing a name to quit program.");
                    var bookUrl = Console.ReadLine();
                    if (string.IsNullOrEmpty(bookUrl))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://www.anapioficeandfire.com/api/books/" + bookUrl);
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var book = JsonConvert.DeserializeObject<Books>(resultRead);

                    Console.WriteLine("___");
                    Console.WriteLine("Name: " + book.Name);
                    Console.WriteLine("Number of pages: " + book.NumberOfPages);
                    Console.WriteLine("Released date: " + book.Released);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Please enter a number from 1 to 10!");
                }

            }
        }
    }
}