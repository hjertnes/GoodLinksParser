using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoodLinksParser
{
    class Link
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("starred")]
        public bool Starred { get; set; }
    }
    
    class Program
    {
        static void Help()
        {
            Console.WriteLine("GoodLinksParse - a utility to parse GoodLinks export files");
            Console.WriteLine("Currently only support to show starred links as a markdown list");
            Console.WriteLine("Usage: ");
            Console.WriteLine("  goodlinksparser [filename]");
        }

        static List<Link>? Parse(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                return JsonSerializer.Deserialize<List<Link>>(r.ReadToEnd());

            }
        }

        static void PrintAsMarkdownList(List<Link> links)
        {
            foreach (var link in links)
            {
                Console.WriteLine($"- [{link.Title}]({link.Url})");
            }
        }
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No filename specified");
                Help();
                return;
            }
            
            var filename = args[0];
            if (!File.Exists(filename))
            {
                Console.WriteLine("Specified file doesn't exist");
                Help();
                return;
            }

            var data = Parse(filename);
            if (data == null)
            {
                Console.WriteLine("Specified file has invalid data");
                Help();
                return;
            }
            
            PrintAsMarkdownList(data.Where(x => x.Starred).ToList());
        }
    }
}