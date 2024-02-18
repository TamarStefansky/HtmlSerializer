using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Practicode2
{
    internal class HtmlHelper
    {

        private readonly static HtmlHelper _instance = new HtmlHelper("Files/AllTags.json", "Files/SelfClosingTags.json");

        public static HtmlHelper Instance => _instance;


        public string[] AllHtmlTags { get; private set; }
        public string[] SelfClosingHtmlTags { get; private set; }

        private HtmlHelper(string allTagsFilePath, string selfClosingTagsFilePath)
        {

            try
            {
                // קריאת קובץ JSON המכיל את כל התגיות ב-HTML
                string allTagsJson = File.ReadAllText(allTagsFilePath);
                AllHtmlTags = JsonSerializer.Deserialize<string[]>(allTagsJson);

                // קריאת קובץ JSON המכיל את התגיות שלא דורשות סגירה
                string selfClosingTagsJson = File.ReadAllText(selfClosingTagsFilePath);
                SelfClosingHtmlTags = JsonSerializer.Deserialize<string[]>(selfClosingTagsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading HTML tags: {ex.Message}");
            }
        }

        public bool IsSelfClosingTag(string tag)
        {
            return this.SelfClosingHtmlTags.Contains(tag);
        }

        public bool isHtmlTag(string tag)
        {
            return this.AllHtmlTags.Contains(tag);
        }
    }
}
























