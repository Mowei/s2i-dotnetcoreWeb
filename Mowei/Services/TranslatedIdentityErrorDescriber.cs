using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mowei.Services
{
    public class TranslatedIdentityErrorDescriber : IdentityErrorDescriber
    {
        private ResourceManager _ResourceManager;
        public TranslatedIdentityErrorDescriber()
        {
            _ResourceManager = new ResourceManager(typeof(Mowei.Resources.IdentityErrorDescriber));
        }

        public IdentityError GetResourceString(IdentityError method,string[] args = null)
        {

            var CustomDescription = _ResourceManager.GetString(method.Code);
            if (CustomDescription != null)
            {
                method.Description = CustomDescription;
            }
            if(args != null)
            {
                method.Description = string.Format(method.Description,args);
            }
            return method;
        }
        public override IdentityError DefaultError()
        {
            //發生錯誤
            return GetResourceString(base.DefaultError());
        }
        public override IdentityError DuplicateEmail(string email)
        {
            //$"重複電子信箱{email}"
            return GetResourceString(base.DuplicateEmail(email),new[] { email });
        }
        public override IdentityError DuplicateRoleName(string role)
        {
            //$"重複角色名稱 {role}
            return GetResourceString(base.DuplicateRoleName(role), new[] { role });
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            //$"重複使用者名稱 {userName}"
            return GetResourceString(base.DuplicateUserName(userName), new[] { userName });
        }
        public override IdentityError InvalidEmail(string email)
        {
            //$"無效電子信箱 {email}"
            return GetResourceString(base.InvalidEmail(email), new[] { email });
        }
        public override IdentityError InvalidRoleName(string role)
        {
            //$"無效角色名稱 {role}"
            return GetResourceString(base.InvalidRoleName(role), new[] { role });
        }
        public override IdentityError InvalidUserName(string userName)
        {
            //$"無效用戶名稱 {userName}"
            return GetResourceString(base.InvalidUserName(userName), new[] { userName });
        }
        public override IdentityError InvalidToken()
        {
            //驗證失敗
            return GetResourceString(base.InvalidToken());
        }
        public override IdentityError LoginAlreadyAssociated()
        {
            //已經登入
            return GetResourceString(base.LoginAlreadyAssociated());
        }
        public override IdentityError PasswordMismatch()
        {
            //密碼錯誤
            return GetResourceString(base.PasswordMismatch());
        }
        public override IdentityError PasswordRequiresDigit()
        {
            //密碼需包含數字 (0-9)
            return GetResourceString(base.PasswordRequiresDigit());
        }
        public override IdentityError PasswordRequiresLower()
        {
            //密碼需包含小寫 (a-z)
            return GetResourceString(base.PasswordRequiresLower());
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            //密碼需包含特殊符號 (!,@,#,$..等)
            return GetResourceString(base.PasswordRequiresNonAlphanumeric());
        }
        public override IdentityError PasswordRequiresUpper()
        {
            //密碼需包含大寫 (A-Z)
            return GetResourceString(base.PasswordRequiresUpper());
        }
        public override IdentityError PasswordTooShort(int length)
        {
            //Description = $"密碼最少需要 {length} 個字"
            return GetResourceString(base.PasswordTooShort(length), new[] { length.ToString() });
        }
        public override IdentityError UserAlreadyHasPassword()
        {
            //用戶已有密碼
            return GetResourceString(base.UserAlreadyHasPassword());
        }
        public override IdentityError UserAlreadyInRole(string role)
        {
            //$"用戶已有 {role} 角色"
            return GetResourceString(base.UserAlreadyInRole(role), new[] { role });
        }
        public override IdentityError UserLockoutNotEnabled()
        {
            //用戶鎖定未啟用
            return GetResourceString(base.UserLockoutNotEnabled());
        }
        public override IdentityError UserNotInRole(string role)
        {
            //$"用戶不存在 {role} 角色"
            return GetResourceString(base.UserNotInRole(role), new[] { role });
        }

    }
}
