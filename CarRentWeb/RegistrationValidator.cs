using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
namespace CarRentWeb
{
    public static class RegistrationValidator
    {
        public static bool IsValidEmailFormat(string email, Label lbl)
        {
            lbl.Text = "Invalid email format.";
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsPasswordLengthValid(string password, int minLength, Label lbl)
        {
            lbl.ForeColor = Color.Red; 
            lbl.Text = "Password should have at least 8 characters.";
            return password.Length >= minLength;
        }

        public static bool ArePasswordsMatching(string password, string confirmPassword, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Passwords do not match.";
            return password == confirmPassword;
        }

        public static bool ContainsNumber(string password, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Password should contain at least one number.";
            return password.Any(char.IsDigit);
        }

        public static bool ContainsSymbol(string password, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Password should contain at least one symbol.";
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public static bool ContainsUpperCase(string password, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Password should contain at least one uppercase letter.";
            return password.Any(char.IsUpper);
        }

        public static bool ContainsLowerCase(string password, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Password should contain at least one lowercase letter.";
            return password.Any(char.IsLower);
        }

        public static bool IsAnswerProvided(string answer, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Please provide an answer.";
            return !string.IsNullOrWhiteSpace(answer);
        }
        public static bool IsValidName(string name,Label lbl)
        {
            lbl.ForeColor = Color.Red; 
            lbl.Text = "Name should contain only letters ";
            return name.All(c => char.IsLetter(c) || c == ' ');
        }
        public static bool IsValidMobileNumber(string phoneNumber, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Invalid mobile number format.";

            // Check if the phone number contains only digits and is of valid length
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 10 && phoneNumber.Length <= 12;
        }
        public static bool IsitFilled(string field, Label lbl)
        {
            lbl.ForeColor = Color.Red;
            lbl.Text = "Please fill all the fields";
            return !string.IsNullOrEmpty(field);
        }


    }
}
