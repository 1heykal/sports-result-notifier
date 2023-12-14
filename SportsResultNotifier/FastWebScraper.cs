using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsResultNotifier
{
    public class FastWebScraper
    {
        public List<Result> GetResults(string url) 
        {
            List<Result> results = new List<Result>();
            
            HtmlWeb web = new HtmlWeb();

            var doc = web.Load(url);

            var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div[position() > 0]/table[1]/tbody");

            foreach ( var node in nodes ) 
            {
                var result = new Result
                {
                    FirstTeam = node.SelectSingleNode("tr[1]/td[1]/a").InnerText,
                    FirstTeamPoints = int.Parse(node.SelectSingleNode("tr[1]/td[2]").InnerText),
                    SecondTeam = node.SelectSingleNode("tr[2]/td[1]/a").InnerText,
                    SecondTeamPoints = int.Parse(node.SelectSingleNode("tr[2]/td[2]").InnerText)              
                };

                results.Add(result);
            
            }

            return results;
        }
    }
}
