using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class EmailContentBuilder
    {
        public static string BuildRegistrationConfirmationEmail(string confirmationLink, string fullName)
        {
            return $@"
                        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}

                .container {{
                    max-width: 600px;
                    margin: 40px auto;
                    background-color: #ffffff;
                    border-radius: 8px;
                    border: 2px solid #000000; /* Black border added */
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                    overflow: hidden;
                }}

                .header {{
                    background: linear-gradient(90deg, #4CAF50 0%, #43A047 100%);
                    color: #ffffff;
                    text-align: center;
                    padding: 20px 0;
                }}

                .header h1 {{
                    margin: 0;
                    font-size: 24px;
                }}

                .content {{
                    padding: 30px 20px;
                    line-height: 1.6;
                    color: #333333;
                }}

                .content p {{
                    margin: 16px 0;
                }}

                .button {{
                    display: inline-block;
                    padding: 12px 25px;
                    font-size: 16px;
                    color: #ffffff;
                    background-color: #4CAF50;
                    text-decoration: none;
                    border-radius: 6px;
                    transition: background-color 0.3s ease;
                }}

                .button:hover {{
                    background-color: #45a049;
                }}

                .content p:last-child {{
                    margin-bottom: 0;
                }}
            </style>
        </head>
        <body>
            <div class=""container"">
                <div class=""header"">
                    <h1>Welcome to Our Service!</h1>
                </div>
                <div class=""content"">
                    <p>Dear <strong>{fullName}</strong>,</p>
                    <p>Thank you for registering with us. Please confirm your account by clicking the button below:</p>
                    <p>
                        <a href=""{confirmationLink}"" class=""button"">
                            Confirm Your Email
                        </a>
                    </p>
                    <p>If you did not register for this account, please ignore this email.</p>
                    <p>Best regards,<br/><strong>Mystery Minis</strong></p>
                </div>
            </div>
        </body>
        </html>
        ";
        }

        public static string BuildPaymentConfirmationEmail(string fullName, int orderId)
        {
            return $@"
                <!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"">
  <title>Payment Confirmation</title>
  <style>
    /* Base styles for the email */
    body {{
      font-family: Arial, sans-serif;
      background-color: #f4f4f4;
      margin: 0;
      padding: 0;
    }}
    .container {{
      max-width: 600px;
      margin: 40px auto;
      background-color: #ffffff;
      border: 2px solid #000000; /* Black border */
      border-radius: 8px;
      box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
      overflow: hidden;
      text-align: center;
    }}
    .header {{
      background: linear-gradient(90deg, #4CAF50 0%, #43A047 100%);
      color: #ffffff;
      padding: 20px 0;
    }}
    .header h1 {{
      margin: 0;
      font-size: 24px;
    }}
    .content {{
      padding: 30px 20px;
      line-height: 1.6;
      color: #333333;
    }}
    /* Update the h1 styling for the “Thank you” message */
    .content h1 {{
      color: #000000;         /* Black text */
      font-size: 28px;
      margin-bottom: 20px;
      font-weight: bold;      /* Bold text */
    }}
    .content p {{
      font-size: 16px;
      margin: 16px 0;
    }}
    .highlight {{
      color: #5563DE;
      font-weight: bold;
    }}
    .footer {{
      padding: 20px;
      font-size: 14px;
      color: #777777;
      background-color: #f0f0f0;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <h1>Payment Confirmation</h1>
    </div>
    <div class=""content"">
      <h1>Thank you for your purchase, <strong>{fullName}</strong>!</h1>
      <p>Your payment for order <strong class=""highlight"">{orderId}</strong> was successful.</p>
      <p>We appreciate your business and hope you enjoy your purchase.</p>
      <p>Best regards,<br/><strong>Mystery Minis</strong></p>
    </div>
    <div class=""footer"">
      &copy; 2025 <strong>Mystery Minis</strong>. All rights reserved.
    </div>
  </div>
</body>
</html>
";
        }
    }
}
