using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mowei.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        /*
        public override string FormatErrorMessage(string name)
        {
            ResourceManager rm = new ResourceManager("Mowei.Validation.validation", typeof(Program).Assembly);
            return string.Format(CultureInfo.CurrentCulture, rm.GetString(ErrorMessageString) , new object[] { name });
        }
        */
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

           
            this.ErrorMessageResourceType = typeof(Mowei.Resources.ValidationResources);
            this.ErrorMessageResourceName = "EmailAddressAttribute";
            if (value != null)
            {
                var strValue = value.ToString().ToUpper();
                
                bool flag = Regex.IsMatch(strValue,
              @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                if (!flag)
                {
                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }

    public class BirthdayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var flag = false;
            var maxDate = DateTime.Now.AddYears(-20);
            if (value != null)
            {
                var birthday = (DateTime)value;
                if (birthday <= maxDate)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                return new ValidationResult(FormatErrorMessage("生日必須滿20歲"));
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

    public class PersonAccount : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var strValue = value.ToString().ToUpper();
                var nameValid = new UsernameIdentity();
                bool flag = nameValid.IsValidID(strValue);

                if (!flag)
                {
                    return new ValidationResult(FormatErrorMessage("無效身分證號"));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }

    public class CompanyAccount : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var strValue = value.ToString().ToUpper();
                var nameValid = new UsernameIdentity();
                bool flag = nameValid.IsValidEIN(strValue);

                if (!flag)
                {
                    return new ValidationResult(FormatErrorMessage("無效統一編號"));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }

    public class UsernameIdentity
    {
        public bool IsValidEIN(string value)
        {
            bool flag = false;
            if (Regex.IsMatch(value, @"^\d{8}$"))
            {
                // 檢查統編
                int[] multiple = { 1, 2, 1, 2, 1, 2, 4, 1 };
                int sum = 0, tmp = 0, tmp2 = 0;
                for (int i = 0; i < multiple.Length; i++)
                {
                    //依不同位置進行不同數字的運算
                    int num = Convert.ToInt32(value.Substring(i, 1));
                    tmp = num * multiple[i];
                    tmp2 = ((tmp / 10) + (tmp % 10));
                    sum = sum + tmp2;
                }
                if (sum % 10 == 0)
                {
                    flag = true;
                }
                else if (value.Substring(6, 1) == "7" && sum % 10 == 9)
                {
                    flag = true;
                }
            }
            return flag;
        }
        public bool IsValidID(string value)
        {
            bool flag = false;
            if (Regex.IsMatch(value, @"^[A-Z]{1}[1-2]{1}[0-9]{8}$"))
            {
                // 檢查身分證
                var idnum = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                var b = new int[11];
                b[1] = idnum[(value[0]) - 65] % 10;
                var c = b[0] = idnum[(value[0]) - 65] / 10;
                for (var i = 1; i <= 9; i++)
                {
                    b[i + 1] = value[i] - 48;
                    c += b[i] * (10 - i);
                }
                if (((c % 10) + b[10]) % 10 == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}
