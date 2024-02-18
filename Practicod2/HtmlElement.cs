using Practicod2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicode2
{
    internal class HtmlElement
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement current = queue.Dequeue();
                yield return current;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = this;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }

        public HashSet<HtmlElement> FindElementsBySelector(Selector selector)
        {
            HashSet<HtmlElement> resultSet = new HashSet<HtmlElement>();
            FindElementsBySelectorRecursive(this, selector, resultSet);
            return resultSet;
        }

        private void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> resultSet)
        {
            if (selector == null)
            {
                return;
            }

            IEnumerable<HtmlElement> descendants = element.Descendants();
            foreach (HtmlElement descElement in descendants)
            {

                if (MatchesSelector(descElement, selector))
                {
                    resultSet.Add(descElement);

                    if (selector.Child != null)
                    {
                        FindElementsBySelectorRecursive(descElement, selector.Child, resultSet);
                    }
                }
            }
        }

        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            if (selector.TagName != null && selector.TagName != "" && element.Name != selector.TagName)
            {
                return false;
            }

            if (selector.Id != null && element.Id != selector.Id)
            {
                return false;
            }

            if (selector.Classes != null && element.Classes != null && !selector.Classes.All(c => element.Classes.Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}
    




















