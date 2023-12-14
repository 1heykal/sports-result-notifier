using System.Security;
using System.Text;

namespace SportsResultNotifier
{
    class Program
    {
        public static void Main(string[] args) 
        {
            List<Result> results = new List<Result>();

            var url = "https://www.basketball-reference.com/boxscores/";

            FastWebScraper scraper = new FastWebScraper();

            results = scraper.GetResults(url);

            StringBuilder mailBody = new StringBuilder();

            mailBody.AppendLine("<img src=\"https://cdn.freebiesupply.com/images/large/2x/nba-logo-transparent.png\"><h1> Today's NBA Results </h1> ");

            foreach (Result result in results) 
            {
                mailBody.AppendLine("<table><tr><th>Team</th><th>Points</th></tr>");
                mailBody.AppendLine($"<tbody><tr><td>{result.FirstTeam}</td><td> {result.FirstTeamPoints}</td></tr>");
                mailBody.AppendLine($"<tr><td>{result.SecondTeam}</td><td> {result.SecondTeamPoints}</td></tr></tbody></table>");
                mailBody.AppendLine("<br>");
                
            }
            mailBody.AppendLine("<style> table, tr { border: 1px solid;} img { width: 50px; height: 100px;}</style>");

            string password = "<your-email-password>";
            SecureString securePassword = new();

            foreach(char c in password)
            {
                securePassword.AppendChar(c);
            }

            securePassword.MakeReadOnly();
           
            MailSender mailSender = new MailSender("smtp-mail.outlook.com", 587, "Sports Results", mailBody.ToString(), "<sender-email>", new[] { "<receiver-email>" }, securePassword);
            mailSender.SendEmail();

            securePassword.Dispose();

        }
    }
}

