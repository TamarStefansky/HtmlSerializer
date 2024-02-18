using Practicode2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practicod2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector FromQueryString(string selectorString)
        {
            if (string.IsNullOrWhiteSpace(selectorString))
                throw new ArgumentException("Selector string cannot be null or empty.");

            Selector selector = new Selector(), root = null, temp = null;
            List<string> parts = selectorString.Split(' ').ToList();

            foreach (string part in parts)
            {
                var attributes = new Regex("^(.*?)?(#.*?)?(\\..*?)?$").Matches(part);
                for (int i = 1; i < attributes[0].Groups.Count; i++)
                {
                    if (HtmlHelper.Instance.AllHtmlTags.Contains(attributes[0].Groups[i].Value))
                        selector.TagName = attributes[0].Groups[1].Value;
                    else if (attributes[0].Groups[i].Value.StartsWith('#'))
                        selector.Id = attributes[0].Groups[i].Value.Substring(1);
                    else if (attributes[0].Groups[i].Value.StartsWith('.'))
                    {
                        selector.Classes = new List<string>();
                        selector.Classes = attributes[0].Groups[i].Value.Split('.').ToList();
                        selector.Classes.Remove("");
                    }
                }
                if (root == null)
                {
                    root = selector;
                    root.Parent = null;
                    temp = root;

                }
                else
                {
                    temp.Child = selector;
                    selector.Parent = temp;
                    temp = selector;
                }
                selector = new Selector();
            }
            return root;
        }

    }
}




