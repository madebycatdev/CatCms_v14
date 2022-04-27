using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using EuroCMS.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;


namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,UserCreator")]
    public class AccountController : BaseController
    {

        const string EMAIL_REGEX_PATTERN = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        const string USERNAME_REGEX_PATTERN = @"^([a-z0-9]{3,10})$";

        int pageSize = 5000;
        int totalUsers;
        int currentPage = 1;


        public ActionResult Index(int? page,
            string letter,
            string searchBy, string keyword,
            bool? isApproved, bool? isOnline, bool? isLockedOut, string role)
        {
            currentPage = page ?? 1;

            ViewData["isApproved"] = isApproved ?? false;
            ViewData["isOnline"] = isOnline ?? false;
            ViewData["isLockedOut"] = isLockedOut ?? false;

            MembershipUserCollection users;


            if (!string.IsNullOrEmpty(role))
            {
                users = FindUsersByRole(new string[] { role });
            }
            else if (!string.IsNullOrEmpty(letter))
            {
                users = Membership.Provider.FindUsersByName(letter + "%", currentPage - 1, pageSize, out totalUsers);
            }
            else if (!string.IsNullOrEmpty(keyword) && searchBy.Equals("email"))
            {
                users = Membership.FindUsersByEmail(keyword, currentPage - 1, pageSize, out totalUsers);
            }
            else if (!string.IsNullOrEmpty(keyword) && searchBy.Equals("userName"))
            {
                users = Membership.FindUsersByName(keyword, currentPage - 1, pageSize, out totalUsers);
            }

            else
            {
                users = Membership.GetAllUsers(currentPage - 1, pageSize, out totalUsers);
            }
            var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
            var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(users);

            if (isOnline != null)
            {
                usersList = usersList.Where(t => t.IsOnline == isOnline);
            }

            if (isApproved != null)
            {
                usersList = usersList.Where(t => t.IsApproved == isApproved);
            }

            if (isLockedOut != null)
            {
                usersList = usersList.Where(t => t.IsLockedOut == isLockedOut);
            }

            var resultAsPagedList = new StaticPagedList<MembershipUser>(usersList, currentPage, pageSize, totalUsers);

            return View(resultAsPagedList);
        }

        public static MembershipUserCollection FindUsersByRole(string[] roles)
        {
            MembershipUserCollection msc = new MembershipUserCollection();

            roles.Select(role => Roles.GetUsersInRole(role))
            .Aggregate((a, b) => a.Union(b).ToArray())
            .Distinct()
            .Select(user => Membership.GetUser(user))
            .ToList().ForEach(user => msc.Add(user));

            return msc;
        }

        [AllowAnonymous]
        public ActionResult AccountLockedOut()
        {
            TempData.Clear();

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            TempData.Clear();

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password, bool? remember, string returnUrl)
        {
            
            bool userHasSystemRole = true;

            //if (Roles.IsUserInRole(userName, "Author") || Roles.IsUserInRole(userName, "Editor") || Roles.IsUserInRole(userName, "PowerUser") || Roles.IsUserInRole(userName, "Administrator"))
            //{
            //    userHasSystemRole = true;
            //}

            if (Request.IsAuthenticated && userHasSystemRole)
            {
                return RedirectToAction("Index", "Home");
            }


            if (!ValidateLogOn(userName, password) || !userHasSystemRole)
            {
                return View();
            }

            bool _remember = remember ?? false;

            FormsAuthentication.SetAuthCookie(userName, _remember);

            if (_remember)
            {
                int timeout = 525600; // Timeout in minutes, 525600 = 365 days.
                var ticket = new FormsAuthenticationTicket(userName, _remember, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Domain = "*";
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true; // cookie not available in javascript.
                Response.Cookies.Add(cookie);
            }

            Session["userName"] = userName;
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LOGIN, this));
            Session["userName"] = null;

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LOGOUT, this));

            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            TempData.Clear();

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string emailorusername, string answer)
        {

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_FORGOT_PASSWORD, this));

            MembershipUser user = Membership.GetUser(emailorusername);

            if (user == null)
            {
                string theUser = Membership.GetUserNameByEmail(emailorusername);
                if (!string.IsNullOrEmpty(theUser))
                    user = Membership.GetUser(theUser);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Sorry, The username or email does not exists in our database.");
                return View();
            }

            string newPassword = "";

            try
            {
                newPassword = user.GetPassword();
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "an error occurred when password generated. please try again later.", true);
                ModelState.AddModelError("", "an error occurred when password generated. please try again later.");
                return View();
            }

            // send mail
            try
            {

                using (SmtpClient client = new SmtpClient())
                {
                    MailSender.Send(user.Email, "EuroCMS [New Password]", "<b>Your new password is:</b>" + newPassword);
                }

                return RedirectToAction("ForgotPasswordSuccess");
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "an error occurred when sending email. please try again later.", true);
                ModelState.AddModelError("", "an error occurred when sending email. please try again later.");

                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordSuccess()
        {

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,Author,ContentManager,UserCreator")]
        public ActionResult ChangeProfile()
        {
            var user = Membership.GetUser(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,Author,ContentManager,UserCreator")]
        public ActionResult ChangeProfile(FormCollection collection)
        {
            TempData.Clear();

            var user = Membership.GetUser(User.Identity.Name);

            try
            {
                string email = HttpUtility.HtmlDecode(collection["email"]);

                if (!Regex.IsMatch(email, EMAIL_REGEX_PATTERN))
                {
                    //ModelState.AddModelError("email", "You must specify a valid email.");
                    //return View();
                    throw new Exception("You must specify a valid email.");
                }

                user.Email = email;
                Membership.UpdateUser(user);

                bool _RemoveAvatar = false;
                bool.TryParse(collection["RemoveAvatar"], out _RemoveAvatar);

                MemoryStream target = new MemoryStream();
                HttpPostedFileBase avatarFile = Request.Files["avatar"] as HttpPostedFileBase;
                byte[] data = new byte[0];

                if (!_RemoveAvatar && avatarFile != null)
                {
                    avatarFile.InputStream.CopyTo(target);
                    data = target.ToArray();

                    if (avatarFile.ContentLength > 5242880)
                        throw new ApplicationException("The avatar file size is too large");

                    if (!(".jpg,.png,.gif".Split(',').Any(t => t == Path.GetExtension(avatarFile.FileName))))
                        throw new ApplicationException("The avatar file is not allowed extension.");
                }

                ProfileBase profile = ProfileBase.Create(User.Identity.Name, true);
                profile["System.FullName"] = HttpUtility.HtmlDecode(collection["System.FullName"]);
                if (data.Length > 0)
                    profile["System.Avatar"] = data;
                else if (_RemoveAvatar)
                    profile["System.Avatar"] = null;

                profile.Save();

                TempData["Message"] = "Your profile has been successfully updated";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                ModelState.AddModelError("_FORM", ex.Message);
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_CHANGE_USER_PROFILE, this));

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,Author,ContentManager,UserCreator")]
        public RedirectToRouteResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            TempData.Clear();

            ViewData["PasswordLength"] = Membership.Provider.MinRequiredPasswordLength;


            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                TempData["Message"] = "The password supplied is incorrect.";
                TempData["HasError"] = true;
                return RedirectToAction("ChangeProfile");
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_CHANGE_PASSWORD, this));

            try
            {
                MembershipUser currentUser = Membership.Provider.GetUser(User.Identity.Name, true /* userIsOnline */);

                if (currentUser.ChangePassword(currentPassword, newPassword))
                {
                    TempData["Message"] = "Your password has been successfully updated.";
                    return RedirectToAction("ChangeProfile");
                }
                else
                {
                    //ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    TempData["Message"] = "The current password is incorrect or the new password is invalid.";
                    TempData["HasError"] = true;
                    return RedirectToAction("ChangeProfile");
                }

            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                CmsHelper.SaveErrorLog(ex, "The current password is incorrect or the new password is invalid.", true);
                TempData["Message"] = "The current password is incorrect or the new password is invalid.";
                TempData["HasError"] = true;
                return RedirectToAction("ChangeProfile");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            ViewData["PasswordLength"] = Membership.Provider.MinRequiredPasswordLength;

            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword, string fullname, string answer)
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_REGISTER, this));

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus status;
                MembershipUser user = Membership.CreateUser(userName, password, email, null, null, false, null, out status);

                if (status == MembershipCreateStatus.Success)
                {
                    ProfileBase profile = ProfileBase.Create(userName, true);
                    profile["System.FullName"] = fullname;
                    profile.Save();

                    Membership.UpdateUser(user);

                    return RedirectToAction("RegisterSuccess", "Account");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(status));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
        [HttpGet]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public ActionResult Create()
        {
            ViewData["PasswordLength"] = Membership.Provider.MinRequiredPasswordLength;
            if(User.IsInRole("Administrator"))
            {
                ViewBag.isAdmin = true;
            }
            else
            {
                ViewBag.isAdmin = false;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public ActionResult Create(string userName, string email, string password, string confirmPassword, string fullname, string role, string comment)
        {
            if (ValidateRegistration(userName, email, password, confirmPassword, role))
            {
                // Attempt to register the user
                MembershipCreateStatus status;
                MembershipUser user = Membership.CreateUser(userName, password, email, null, null, true, null, out status);

                if (status == MembershipCreateStatus.Success)
                {
                    ProfileBase profile = ProfileBase.Create(userName, true);
                    profile["System.FullName"] = fullname;
                    profile.Save();

                    user.Comment = HttpUtility.HtmlDecode(comment);
                    Membership.UpdateUser(user);

                    Roles.AddUserToRole(userName, role);

                    //FormsAuthentication.SetAuthCookie(userName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(status));
                }
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_REGISTER, this));

            // If we got this far, something failed, redisplay form
            return View();
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public ViewResult Details(string id)
        {
            var user = Membership.GetUser(id);
            if (User.IsInRole("Administrator"))
            {
                ViewBag.userRole = true;
            }
            else
            {
                ViewBag.userRole = false;
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult Delete(string id)
        {
            TempData.Clear();

            try
            {

                if (id != User.Identity.Name)
                    Membership.DeleteUser(id, true);

                //var user = Membership.GetUser(id, false);
                //user.IsApproved = false;

                //if (id != User.Identity.Name)
                //    Membership.UpdateUser(user);


                TempData["Message"] = "The user has been deleted";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_DELETE, this));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult ChangeApproval(string id, bool isApproved)
        {
            TempData.Clear();

            try
            {
                var user = Membership.GetUser(id, false);
                user.IsApproved = isApproved;

                if (id != User.Identity.Name)
                    Membership.UpdateUser(user);

                TempData["Message"] = "The user status has been changed";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_CHANGE_APPROVAL, this));

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult Unlock(string id)
        {
            TempData.Clear();

            try
            {
                if (id != User.Identity.Name)
                    Membership.Provider.UnlockUser(id);

                TempData["Message"] = "The user has been unlocked";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_UNLOCK, this));

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult ResetPassword(string id, string answer)
        {
            TempData.Clear();

            try
            {

                string newPassword = Membership.Provider.ResetPassword(id, answer);

                MembershipUser user = Membership.GetUser(id);

                ProfileBase profile = ProfileBase.Create(user.UserName.ToString());
                profile.SetPropertyValue("isResetPassword", "1");
                profile.Save();

                try
                {
                    MailSender.Send(user.Email, "Reset Password Notification [EuroCMS]", "<b>Your new password:</b>: " + newPassword);
                }
                catch (Exception ex)
                {
                    //Mail gönderilemedi - ama işleme devam et
                    CmsHelper.SaveErrorLog(ex, "Cannot Send Mail", true);
                }
                // MailSender.Send("ramazan.donmez@euromsg.com", user.Email, "Reset Password Notification [EuroCMS]", "<b>Your new password:</b>: " + newPassword);

                TempData["Message"] = "The user password has been changed. New password is: <span>" + newPassword + "</span>";

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_RESET_PASSWORD, this));

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult AddToRole(string id, FormCollection collection)
        {
            TempData.Clear();
            string role = collection["roleName"];

            try
            {
                if (!Roles.IsUserInRole(id, role))
                {
                    Roles.AddUserToRole(id, role);
                    TempData["Message"] = "The role successfully added.";
                }
                else
                {
                    TempData["Message"] = "You already have this role(s).";
                }


            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_ADD_TO_ROLE, this));

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult RemoveFromRole(string id, FormCollection collection)
        {
            TempData.Clear();
            string role = collection["roleName"];

            try
            {
                string userRoles = string.Join(",", Roles.GetRolesForUser(id));

                if (Roles.IsUserInRole(id, role))
                {
                    string userRolesNew = userRoles.Replace(role, "").Trim();
                    if (userRolesNew.Contains("PowerUser") || userRolesNew.Contains("Author") || userRolesNew.Contains("Administrator") || userRolesNew.Contains("Editor") || userRolesNew.Contains("ContentManager") || userRolesNew.Contains("ContentEntry") || userRolesNew.Contains("UserCreator") ) 
                    {
                        Roles.RemoveUserFromRole(id, role);
                        TempData["Message"] = "The role successfully removed";
                    }
                    else
                    {
                        TempData["Message"] = "You can not remove system roles.";
                    }
                }
                else
                {
                    TempData["Message"] = "You can not remove system roles.";
                }


                //if (!role.Equals("Administrator") && !role.Equals("PowerUser") && !role.Equals("Editor") && !role.Equals("Author") && Roles.IsUserInRole(id, role))
                //{
                //    Roles.RemoveUserFromRole(id, role);
                //    TempData["Message"] = "The role successfully removed";
                //}
                //else
                //{
                //    TempData["Message"] = "You can not remove system roles. ";
                //}



            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_REMOVE_FROM_ROLE, this));

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult ChangeUserProfile(string id, FormCollection collection)
        {
            TempData.Clear();

            try
            {
                MembershipUser user = Membership.GetUser(id);
                user.Email = HttpUtility.HtmlDecode(collection["email"]);
                user.Comment = HttpUtility.HtmlDecode(collection["comment"]);
                Membership.UpdateUser(user);

                ProfileBase profile = ProfileBase.Create(id, true);
                profile["System.FullName"] = HttpUtility.HtmlDecode(collection["System.FullName"]);
                profile.Save();

                TempData["Message"] = "User profile has been successfully updated.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_CHANGE_USER_PROFILE, this));

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult SaveRole(string name)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_SAVE_ROLE, this));

            try
            {

                if (!Roles.RoleExists(name))
                    Roles.CreateRole(name);

                TempData["Message"] = "The role successfully saved.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("ManageRoles", new { role = name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public RedirectToRouteResult DeleteRole(string name)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.USER_DELETE_ROLE, this));

            try
            {
                if (Roles.RoleExists(name))
                    Roles.DeleteRole(name, true);


                TempData["Message"] = "The role successfully deleted";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("ManageRoles", new { role = name });
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public ActionResult ManageRoles(int? page)
        {
            string[] roles = Roles.GetAllRoles();

            return View(roles);
        }

        #region AccessRules
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public ActionResult AccessRules(int? id, int? page, string contentType, string mode)
        {
            BaseDbContext context = new BaseDbContext();
            var model = context.GetAllRules(contentType);
            var paged = model.ToPagedList(page ?? 1, 25);

            ViewBag.ContentType = contentType;
            ViewBag.RuleId = "";
            ViewBag.RuleName = "";

            ViewBag.ContentId = "";
            ViewBag.Roles = "";
            ViewBag.Users = "";
            ViewBag.ContentItemName = "";
            ViewBag.Permissions = "";

            if (mode == "Edit")
            {
                var item = model.Where(t => t.RuleId == id).FirstOrDefault();

                ViewBag.RuleId = item.RuleId;
                ViewBag.RuleName = item.RuleName;
                ViewBag.ContentType = item.ContentType;
                ViewBag.ContentId = item.ContentId;
                ViewBag.Roles = item.Roles;
                ViewBag.Users = item.Users;
                ViewBag.ContentItemName = item.ContentItemName;
                ViewBag.Permissions = item.Permissions;
            }

            return View(paged);
        }

        [HttpPost]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccessRule(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ACCESS_RULE_SAVE, this));

            var ruleId = collection["RuleId"];
            var ruleName = collection["ruleName"];
            var contentId = collection["contentId"];
            var contentType = collection["contentType"];
            var roleList = collection["roles"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); ;
            //var userList = collection["users"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); ;
            var roles = string.Join(",", roleList);
            //var users = string.Join(",", userList);

            var permissionList = collection["permissions"].Replace("true", "").Replace("false", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var permissions = string.Join(",", permissionList);

            try
            {
                ViewBag.RuleName = ruleName;
                ViewBag.ContentId = contentId;
                ViewBag.ContentType = contentType;
                ViewBag.Roles = roles;
                //ViewBag.Users = users;
                ViewBag.Permissions = permissions;

                if (string.IsNullOrEmpty(ruleName))
                    throw new Exception("Rule name is required field!");

                if (string.IsNullOrEmpty(contentId))
                    throw new Exception("Please select a content item!");

                if (string.IsNullOrEmpty(contentType))
                    throw new Exception("Content Type is required field!");

                if (string.IsNullOrEmpty(roles))
                    throw new Exception("Please select a role or user!");

                //if (string.IsNullOrEmpty(permissions))
                //    throw new Exception("Please select a permission");

                BaseDbContext context = new BaseDbContext();

                if (!string.IsNullOrEmpty(ruleId))
                    context.UpdateAccessRule(Convert.ToInt32(ruleId), ruleName, contentId, contentType, roles, "", permissions, User.Identity.Name);
                else
                    context.CreateAccessRule(ruleName, contentId, contentType, roles, "", permissions, User.Identity.Name);

                TempData["Message"] = "The access rule successfully saved";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("AccessRules", new { contentType = contentType });
        }

        [HttpPost]
        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccessRule(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ACCESS_RULE_DELETE, this));

            try
            {
                BaseDbContext context = new BaseDbContext();
                context.DeleteAccessRule(id);

                TempData["Message"] = "The access rule successfully deleted";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("AccessRules");
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetRolesAutoComplete(string term)
        {
            var roles = Roles.GetAllRoles().Where(t => t.ToLowerInvariant().Contains(term.ToLowerInvariant()));
            var query = from u in roles
                        select new
                        {
                            key = u,
                            value = u
                        };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetUsersAutoComplete(string term)
        {
            int total = 0;
            var users = Membership.Provider.FindUsersByName(term + "%", 0, 999999, out total);
            var converter = new EnumerableToEnumerableTConverter<MembershipUserCollection, MembershipUser>();
            var usersList = converter.ConvertTo<IEnumerable<MembershipUser>>(users);
            var query = from u in usersList
                        select new
                        {
                            key = u.UserName,
                            value = u.UserName
                        };

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetSitesAutoComplete(string term)
        {
            SiteService _siteService = new SiteService(new SiteRepository());

            var sites = _siteService.Find(term);

            var query = from u in sites
                        select new
                        {
                            key = u.Id,
                            value = u.Name
                        };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetZoneGroupsAutoComplete(string term)
        {
            CmsDbContext dbContext = new CmsDbContext();

            term = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(term));
            term = term.Trim().ToLower();
            var zoneGroups = dbContext.ZoneGroups.ToList();
            zoneGroups = zoneGroups.Where(zg => HttpUtility.UrlDecode(HttpUtility.HtmlDecode(zg.Name)).Trim().ToLower().Contains(term)).ToList();

            List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();

            foreach (ZoneGroup zoneGroup in zoneGroups.ToList())
            {
                zoneGroup.Name = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(zoneGroup.Name));
                listZoneGroup.Add(zoneGroup);
            }

            var query = from u in listZoneGroup
                        select new
                        {
                            key = u.Id,
                            value = u.Name + " - " + u.Id.ToString()
                        };

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetZonesAutoComplete(string term)
        {
            ZoneDbContext context = new ZoneDbContext();
            var zones = context.Zones.Where(z => z.zone_name.Contains(term)).ToList();
            var query = from u in zones
                        select new
                        {
                            key = u.zone_id,
                            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(u.zone_name)) + " - " + u.zone_id.ToString()
                        };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "Administrator,UserCreator")]
        public JsonResult GetArticlesAutoComplete(string term)
        {
            CmsDbContext dbContext = new CmsDbContext();
            var articles = dbContext.vArticlesZonesFulls.Where(vaz => vaz.Headline.Contains(term) || vaz.MenuText.Contains(term)).ToList();
            //ArticleDbContext context = new ArticleDbContext();
            //var articles = context.SelectArticles().Where(a => a.headline.Contains(term) || a.menu_text.Contains(term));
            var query = from u in articles
                        select new
                        {
                            key = u.ArticleID,
                            value = u.Headline + " (" + u.ArticleID + ") - " + u.SiteName + "/" + HttpUtility.UrlDecode(HttpUtility.HtmlDecode(u.ZoneGroupName)) + "/" + HttpUtility.UrlDecode(HttpUtility.HtmlDecode(u.ZoneName))
                        };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < Membership.Provider.MinRequiredPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         Membership.Provider.MinRequiredPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }

            var ldapStatus = System.Configuration.ConfigurationManager.AppSettings["LDAPStatus"];
            var ldapServer = System.Configuration.ConfigurationManager.AppSettings["LDAPServer"];

            if (ldapStatus == "on" && !string.IsNullOrEmpty(ldapServer))
            {
                using (DirectoryEntry directoryEntry = new DirectoryEntry(ldapServer, userName, password))
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
                    }

                }
               
            }
            else
            {
                if (!Membership.ValidateUser(userName, password))
                {
                    ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
                }
            }           

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            return ValidateRegistration(userName, email, password, confirmPassword, "dummy");
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword, string role)
        {
            if (String.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("role", "You must specify a role.");
            }
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (!Regex.IsMatch(userName, USERNAME_REGEX_PATTERN))
            {
                ModelState.AddModelError("username", "You must specify a valid username. The Username can contain minimum 3 characters and maximum 10 characters, Also must contain alphanumeric characters");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (!Regex.IsMatch(email, EMAIL_REGEX_PATTERN))
            {
                ModelState.AddModelError("email", "You must specify an valid email address.");
            }
            if (password == null || password.Length < Membership.Provider.MinRequiredPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         Membership.Provider.MinRequiredPasswordLength));
            }

            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. You must specify minimum 6 characters and minimum 1 special character. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        public ActionResult LogoutUser(string id)
        {
            try
            {
                MembershipUser user = Membership.GetUser(id);

                ProfileBase profile = ProfileBase.Create(user.UserName.ToString());
                profile.SetPropertyValue("isLoggedOut", "1");
                profile.Save();

                TempData["Message"] = user.UserName + " is logged out";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Error Logging Out a User", true);
            }

            return RedirectToAction("Index", "Account");
        }
    }
}
