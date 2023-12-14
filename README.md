# Sports Results Notifier

This project is a simple tool for scraping NBA sports results from a specified URL and sending them through email using the Outlook SMTP server.

## Features

- Web scraping of NBA sports results from [Basketball Reference](https://www.basketball-reference.com/boxscores/).
- Email notification of scraped sports results.
- Outlook SMTP server integration.

## Prerequisites

Before running the project, make sure you have the following:

- .NET SDK installed
- Outlook email account
- App Password generated for Outlook (if using two-factor authentication)

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/yourusername/sports-result-notifier.git
    ```

2. Navigate to the project directory:

    ```bash
    cd sports-result-notifier
    ```

3. Configure the project by updating the `Program.cs` file with your email and SMTP server details.

4. Build and run the project:

    ```bash
    dotnet run
    ```

## Configuration

Update the `Program.cs` file with your configuration:

```csharp
string password = "<your-email-password>";
SecureString securePassword = new();

foreach(char c in password)
{
    securePassword.AppendChar(c);
}

securePassword.MakeReadOnly();

MailSender mailSender = new MailSender(
    "smtp-mail.outlook.com", 587, 
    "Sports Results",
    mailBody.ToString(),
    "<sender-email>",
    new[] { "<receiver-email>" },
    securePassword
);

mailSender.SendEmail();

securePassword.Dispose();
