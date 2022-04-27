using EuroCMS.Plugin.DExpert.Models;
using EuroCMS.Plugin.DExpert.RandevuService;
using EuroCMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using DefaultValueAttribute = System.ComponentModel.DefaultValueAttribute;

namespace EuroCMS.Plugin.DExpert
{
    /// <summary>
    /// Summary description for plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        //bytes
        int maxFileSize = 6000000;
        string fileExtensions = ".pdf,.docx,.doc";
        string _dateFormat = "dd/MM/yyyy";

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string result = "", plugin = "";

            if (!string.IsNullOrEmpty(context.Request.Form["plugin"]))
            {
                plugin = context.Request.Form["plugin"].ToLower().Trim();
            }
            else
            {
                result = jsSerializer.Serialize(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }

            switch (plugin)
            {
                case "cancelappointment":
                    result = CancelAppointment(context);
                    break;

                case "checkotpstatus":
                    result = CheckOtpStatus();
                    break;

                case "createappointment":
                    result = CreateAppointment(context);
                    break;

                case "createcontactform":
                    result = CreateContactForm(context);
                    break;

                case "createsurvey":
                    result = CreateSurvey(context);
                    break;

                case "getappointment":

                    result = GetAppointment(context);
                    break;

                case "getavailabledays":
                    result = GetAvailableDays(context);
                    break;

                case "getavailablehours":
                    result = GetAvailableHours(context);
                    break;

                case "getcitieswithbranches":
                    result = GetCitiesWithBranches();
                    break;

                case "getcontactchannels":
                    result = GetContactChannels();
                    break;

                case "getexpertiseaim":
                    result = GetExpertiseAim();
                    break;

                case "getproductswithdetails":
                    result = GetProductsWithDetails();
                    break;
                case "sendsms":
                    result = SendSms(context);
                    break;
                case "updateappointment":
                    result = UpdateAppointment(context);
                    break;
            }

            context.Response.Write(result);
        }

        /// <summary>
        /// Anket sorusu
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CreateSurvey(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                string AppointmentId = "", contactChannelId = "", otherContact = "";
                ValueMapper(context, respond, ref otherContact, nameof(otherContact));

                if (!ValueMapper(context, respond, ref contactChannelId, nameof(contactChannelId), "Lütfen bir İletişim Kanalı seçiniz.") ||
                    !ValueMapper(context, respond, ref AppointmentId, nameof(AppointmentId), "Randevu Numarası bulunamadı."))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    respond = client.CreateSurvey(new RequestCreateSurvey
                    {
                        appointmentFormId = AppointmentId,
                        contactChannelId = contactChannelId,
                        otherContact = otherContact
                    });
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "CreateSurvey Failed", false);

                respond.resultCode = 3;
                respond.Message = "Cevabınız gönderilirken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }
        
        /// <summary>
        /// Beni arayın fromu
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CreateContactForm(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                RequestContactForm contact = new RequestContactForm();

                if (!ModelMapper(contact, context, respond))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    respond = client.CreateContactForm(contact);
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "CreateContactForm Failed", false);

                respond.resultCode = 3;
                respond.Message = "İletişim formu gönderilirken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// randevu iptal
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CancelAppointment(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                string AppointmentId = "", SmsCode = "", Phone = "";
                bool SmsAllowed = false;

                if (!ValueMapper(context, respond, ref AppointmentId, nameof(AppointmentId), "Randevu Numarası bulunamadı.") ||
                    !ValueMapper(context, respond, ref SmsCode, nameof(SmsCode), "Sms Kodu boş gönderilemez.") ||
                    !ValueMapper(context, respond, ref SmsAllowed, nameof(SmsAllowed)) ||
                    !ValueMapper(context, respond, ref Phone, nameof(Phone), "Telefon Numarası boş gönderilemez."))
                {
                    return jsSerializer.Serialize(respond);
                }

                RequestControlSms smsRequest = new RequestControlSms
                {
                    phoneNumber = "90" + Phone.Replace(" ", string.Empty),
                    oneTimePassword = SmsCode
                };


                using (ServiceClient client = new ServiceClient())
                {
                    if (!SmsAllowed || client.ControlSms(smsRequest))
                    {
                        respond = client.CancelAppointment(new RequestCancelAppointment { formGlobalId = AppointmentId });
                    }
                    else
                    {
                        respond.resultCode = 3;
                        respond.Message = "Sms kodu doğrulanamadı.";
                    }
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "CancelAppointment Failed", false);

                respond.resultCode = 3;
                respond.Message = "Randevu iptal edilirken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Randevu bilgilerini al
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetAppointment(HttpContext context)
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;
            
            try
            {
                string appointmentId = "";

                if (!ValueMapper(context, respond, ref appointmentId, nameof(appointmentId), "Randevu Numarası bulunamadı."))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = ResponseToAppointmentMapper(client.GetAppointment(appointmentId));

                    if (respond.Object == null)
                    {
                        respond.resultCode = 3;
                        respond.Message = "Randevu bulunamadı.";
                    }
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetAppointment Failed", false);

                respond.resultCode = 3;
                respond.Message = "Randevu bilgileri getirilirken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Randevu oluştur
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CreateAppointment(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                AppointmentModel appointment = new AppointmentModel();

                if (!ModelMapper(appointment, context, respond))
                {
                    return jsSerializer.Serialize(respond);
                }

                RequestControlSms smsRequest = new RequestControlSms
                {
                    phoneNumber = "90" + appointment.Phone.Replace(" ", string.Empty),
                    oneTimePassword = appointment.SmsCode
                };

                using (ServiceClient client = new ServiceClient())
                {
                    if (!appointment.SmsAllowed || client.ControlSms(smsRequest))
                    {
                        var request = AppointmentToRequestMapper(appointment);
                        respond = client.CreateAppointment(request);
                    }
                    else
                    {
                        respond.resultCode = 3;
                        respond.Message = "Sms kodu doğrulanamadı.";
                    }
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "CreateAppointment Failed", false);

                respond.resultCode = 3;
                respond.Message = "Randevu oluşturulurken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz...";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Randevu Güncelle
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string UpdateAppointment(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                UpdateModel appointment = new UpdateModel();

                if (!ModelMapper(appointment, context, respond))
                {
                    return jsSerializer.Serialize(respond);
                }

                RequestControlSms smsRequest = new RequestControlSms
                {
                    phoneNumber = "90" + appointment.Phone.Replace(" ", string.Empty),
                    oneTimePassword = appointment.SmsCode
                };

                using (ServiceClient client = new ServiceClient())
                {
                    if (!appointment.SmsAllowed || client.ControlSms(smsRequest))
                    {
                        respond = client.UpdateAppointment(UpdateToRequestMapper(appointment));
                    }
                    else
                    {
                        respond.resultCode = 3;
                        respond.Message = "Sms kodu doğrulanamadı.";
                    }
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "UpdateAppointment Failed", false);

                respond.resultCode = 3;
                respond.Message = "Randevu güncellenirken hata alınmıştır. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Müsait günleri al
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetAvailableDays(HttpContext context)
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;

            try
            {
                List<DateTime> dayList = null;

                string branchCode = "";
                int productTime = 0;

                if (!ValueMapper(context, respond, ref branchCode, nameof(branchCode), "Lütfen bir Şube seçiniz.") ||
                    !ValueMapper(context, respond, ref productTime, nameof(productTime), "Lütfen bir Ürün seçiniz."))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    dayList = client.getSuitableDates(branchCode, productTime, 15, 30)
                        .Select(x => ParseDate(x))
                        .Distinct()
                        .ToList();
                }

                respond.Object = PrepareAvailableDays(dayList);
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetAvailableDays Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Müsait saatleri al
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetAvailableHours(HttpContext context)
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;

            try
            {
                string branchCode = "";
                int productTime = 0;
                DateTime appointmentDate = new DateTime();

                if (!ValueMapper(context, respond, ref branchCode, nameof(branchCode), "Lütfen bir Şube seçiniz.") ||
                    !ValueMapper(context, respond, ref productTime, nameof(productTime), "Lütfen bir Ürün seçiniz.") ||
                    !ValueMapper(context, respond, ref appointmentDate, nameof(appointmentDate), "Lütfen Randevu Tarihini seçiniz."))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.getSuitableHours(appointmentDate, branchCode, productTime, 15)
                        //.Where(x => ParseDate(x) == appointmentDate)
                        .Select(x => ParseHour(x))
                        .Distinct()
                        .ToList();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetAvailableHours Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Ürünleri ve içeriklerini getir
        /// </summary>
        /// <returns></returns>
        public string GetProductsWithDetails()
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;

            try
            {
                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.GetProducts().Select(x => new
                    {
                        Id = x.ID,
                        ProductDetails = client.GetProductDetail(x.productCode).Select(y => new
                        {
                            ProductDetailTitle = y.prodcutDetail,
                            ProductDetailContent = y.propertyDetails
                        }).ToList(),
                        DiscountPrice = x.discountPrice,
                        DiscountRate = x.discountRate,
                        ProductCode = x.productCode,
                        ProductName = x.productName,
                        ProductPrice = x.productPrice,
                        ProductTime = x.productTime
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetProductsWithDetails Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Şehirleri, ilçeleri ve şubeleri getir
        /// </summary>
        /// <returns></returns>
        public string GetCitiesWithBranches()
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;

            try
            {
                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.GetCities().Select(city => new
                    {
                        Code = city.cityCode,
                        Name = city.cityName,
                        Branches = client.GetTowns(city.cityCode).Select(branch => new
                        {
                            Code = branch.branchCode,
                            Name = branch.branchName,
                            TownCode = branch.townCode,
                            TownName = branch.townName,
                            MapUrl = branch.coordinateURL
                        }).ToList()

                    }).ToList();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetCitiesWithBranches Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Bizi nereden duydunuz seçenekleri
        /// </summary>
        /// <returns></returns>
        public string GetContactChannels()
        {
            Respond2 respond = new Respond2();

            try
            {
                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.GetContactChannels();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetContactChannels Failed", false);

                respond.resultCode  = 3;
                respond.Message = "Anket yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// Ekspertiz amaçları listesi
        /// </summary>
        /// <returns></returns>
        public string GetExpertiseAim()
        {
            Respond2 respond = new Respond2();
            respond.resultCode = 1;

            try
            {
                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.GetExpertiseAim().Select(x => new
                    {
                        AimId = x.aimID,
                        AimName = x.aimName,
                        Products = client.GetProductsByAim(x.aimID).Select(y => y.productCode).ToList()
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "GetExpertiseAim Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// SMS açık mı?
        /// </summary>
        /// <returns></returns>
        public string CheckOtpStatus()
        {
            Respond2 respond = new Respond2();

            try
            {
                using (ServiceClient client = new ServiceClient())
                {
                    respond.Object = client.GetOTPStatus();
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "CheckOtpStatus Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return jsSerializer.Serialize(respond);
        }

        /// <summary>
        /// SMS gönder
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string SendSms(HttpContext context)
        {
            Respond2 respond = new Respond2();

            try
            {
                string phoneNumber = "";
                string name = "";
                respond.Object = true;

                if (!ValueMapper(context, respond, ref phoneNumber, nameof(phoneNumber), "Telefon Numarası bulunamadı.") ||
                    !ValueMapper(context, respond, ref name, nameof(name), "Müşteri Adı bulunamadı."))
                {
                    return jsSerializer.Serialize(respond);
                }

                using (ServiceClient client = new ServiceClient())
                {
                    RequestSendSms request = new RequestSendSms
                    {
                        phoneNumber = "90" + phoneNumber.Replace(" ", string.Empty),
                        customerNameSurname = name
                    };

                    respond = client.SendSms(request);
                }
            }
            catch (Exception e)
            {
                CmsHelper.SaveErrorLog(e, "SendSms Failed", false);

                respond.resultCode = 3;
                respond.Message = "Üzgünüz, bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
                respond.Object = false;
            }

            return jsSerializer.Serialize(respond);
        }

        //Hold session eklenecek

        private string CombineExceptionMessages(Exception e)
        {
            string message = "";

            do
            {
                message += (e.Message + " | ");
                e = e.InnerException;

            } while (e != null);

            return message;
        }
        

        private bool ModelMapper<T>(T model, HttpContext context, Respond2 respond) where T : class
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType == typeof(ExtensionDataObject)) continue;

                var param = context.Request.Form[propertyInfo.Name];


                if (string.IsNullOrEmpty(param))
                {
                    var attributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);

                    if (attributes.Length > 0)
                    {
                        var defaultAttr = (DefaultValueAttribute)attributes[0];
                        propertyInfo.SetValue(model, Convert.ChangeType(defaultAttr.Value, propertyInfo.PropertyType));

                        continue;
                    }

                    respond.Message = string.Format("{0} boş gönderilemez.", propertyInfo.Name);
                    respond.resultCode = 2;

                    return false;
                }

                param = WebUtility.HtmlDecode(param).Trim();
                propertyInfo.SetValue(model, Convert.ChangeType(param, propertyInfo.PropertyType));
            }

            return true;
        }
        
        
        private bool ValueMapper<T>(HttpContext context, Respond2 respond, ref T variable, string variableName, string validationMessage = "")
        {
            string param = context.Request.Form[variableName];

            if (string.IsNullOrEmpty(param))
            {
                respond.Message = validationMessage;
                respond.resultCode = 2;

                return false;
            }
            else if (param == null)
            {
                return true;
            }

            param = WebUtility.HtmlDecode(param).Trim();
            variable = (T)Convert.ChangeType(param, typeof(T));

            return true;
        }


        private string ParseHour(string suitableDate)
        {
            return Convert.ToDateTime(suitableDate.Split('-')[0], CultureInfo.InvariantCulture).ToShortTimeString() +
                   " - " +
                   Convert.ToDateTime(suitableDate.Split('-')[1], CultureInfo.InvariantCulture).ToShortTimeString();
        }

        private DateTime ParseDate(string suitableDate)
        {
            return Convert.ToDateTime(suitableDate.Split(' ')[0], CultureInfo.InvariantCulture);//.ToString("dd/MM/yyyy");
        }

        private DateModel PrepareAvailableDays(List<DateTime> dayList)
        {
            DateModel availableDays = new DateModel();

            for (DateTime i = DateTime.Today; i <= dayList.LastOrDefault(); i = i.AddDays(1))
            {
                if (dayList.Contains(i)) continue;

                availableDays.DisabledDayList.Add(i.ToString(_dateFormat));
            }

            availableDays.LastEnabledDay = dayList.LastOrDefault().ToString(_dateFormat);
            availableDays.FirstEnabledDay = dayList.FirstOrDefault().ToString(_dateFormat);

            return availableDays;
        }

        private RequestCreateAppointment AppointmentToRequestMapper(AppointmentModel appointment)
        {
            string[] time = appointment.AppointmentTime.Split('-');
            string[] start = time[0].Trim().Split(':');
            string[] end = time[1].Trim().Split(':');

            return new RequestCreateAppointment
            {
                name = appointment.FirstName,
                surname = appointment.LastName,
                mail = appointment.Email,
                phoneNumber = "90" + appointment.Phone.Replace(" ", string.Empty),
                expertiseAim = appointment.ExpertiseAimName,
                expertisePackage = appointment.ExpertisePackageName,
                appointmentDate = appointment.AppointmentDate,
                startHour = start[0].Trim(),
                startMinute = start[1].Trim(),
                finishHour = end[0].Trim(),
                finishMinute = end[1].Trim(),
                branchCode = appointment.BranchCode,
                city = appointment.CityName,
                isSendMail = 1,
                isSendSms = 1,
                isEtrateAccept = Convert.ToInt32(appointment.EtradeSubmit),
                isKVKKAccept = Convert.ToInt32(appointment.KvkkSubmit),
                isEmailSmsSubmitAccept = Convert.ToInt32(appointment.EmailSmsSubmit)
            };
        }

        private object ResponseToAppointmentMapper(ResponseGetAppointment response)
        {

            return response == null ? null : new
            {
                FirstName = WebUtility.HtmlDecode(response.txtCustomerName),
                LastName = WebUtility.HtmlDecode(response.txtSurname),
                Email = WebUtility.HtmlDecode(response.mail),
                Phone = response.phone.TrimStart('9', '0'),
                ExpertiseAimName = WebUtility.HtmlDecode(response.aimName),
                ExpertisePackageName = WebUtility.HtmlDecode(response.packageName),
                //AppointmentDate = ParseDate(response.appointmentDate).ToString(_dateFormat).Replace('.', '/'),
                AppointmentDate = response.appointmentDate,
                AppointmentTime = string.Format("{0}:{1} - {2}:{3}", response.startHour, response.startMinute, response.finishHour.Trim(), response.finishMinute.Trim()),
                BranchCode = response.branchCode,
                CityName = WebUtility.HtmlDecode(response.txtcity),
                EtradeSubmit = true,
                KvkkSubmit = true,
                BranchName = response.branchName,
                SurveyStatus = string.IsNullOrEmpty(response.hearInfo),
                AppointmentStatus = response.appointmentStatus != "İptal Edildi"
            };
        }

        private RequestUpdateAppointment UpdateToRequestMapper(UpdateModel appointment)
        {
            return new RequestUpdateAppointment
            {
                formGlobalId = appointment.AppointmentId,
                newProductName = appointment.ExpertisePackageName,
                newAppointmentDate = appointment.AppointmentDate,
                newBranchCode = appointment.BranchCode,
                newAppointmentHour = appointment.AppointmentTime,
                newAim = appointment.ExpertiseAimName
            };
        }
    }
}