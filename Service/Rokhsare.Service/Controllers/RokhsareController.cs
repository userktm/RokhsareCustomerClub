using Rokhsare.Control.Base.Controllers;
using Rokhsare.Models;
using Rokhsare.Service.JsonModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Rokhsare.Service.Controllers
{
    public class RokhsareController : BaseController
    {
        // GET: Rokhsare
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetFactureViewSend()
        {
            ResultModel resultmodel = new ResultModel();

            var re = Request;
            var header = re.Headers;
            var content = re.Content.ReadAsStringAsync().Result;

            content = content.Replace("\\", string.Empty);
            content = content.Trim('"');

            // دریافت ویو مربوط به ذخیره سازی فاکتور از سمت برنامه رخساره
            List<FactureViewJsonModel> jsonmodel = new List<FactureViewJsonModel>();
            jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FactureViewJsonModel>>(content);

            if (header.Contains("Token"))
            {
                string token = header.GetValues("Token").First();

                if (token == "42345")
                {

                    // بررسی club plan مربوط به business
                    if (RokhsarehClubDb.BusinesUnitClubPlans.Any(u => u.BusinesUnitId == 1) || RokhsarehClubDb.DefaultClubPlans.Any(u => u.BusinessUnitId == 1))
                    {
                        var businessunitclubplan = RokhsarehClubDb.BusinesUnitClubPlans.FirstOrDefault(u => u.BusinesUnitId == 1).ClubPlan;
                        var clubplanid = RokhsarehClubDb.ClubPlanDetails.FirstOrDefault(u => u.ClubPlanDetailId == businessunitclubplan.ClubPlanDetailId);
                        var businessunit = RokhsarehClubDb.BusinessUnits.FirstOrDefault(u => u.BusinessUnitId == 1);

                        // با توجه به اینکه تعداد رکورد ها ممکن است بیش از 1 باشد، و بعضی فیلدهای رکورد ها یکسان است
                        // ما اطلاعات یک فیلد را دریافت و برای مشخص شدن اطلاعاتی نظیر کاربر و مبلغ پرداختی استفاده میکنیم

                        var firstrecored = jsonmodel.First();
                        // بررسی اینکه آیا اطلاعات ارسال شده مربوط به کاربر، وجود دارد یا خیر
                        if (!RokhsarehClubDb.Users.Any(u => u.UserCode == firstrecored.UserCode && u.MobileNumber == firstrecored.UserMobile && u.FullName == firstrecored.UserName))
                        {
                            Rokhsare.Models.User userdb = new User();
                            userdb.BusinessUnitId = 1;
                            userdb.NationalNumber = "";
                            userdb.RokhsarehUserId = firstrecored.UserId;
                            userdb.UserCode = firstrecored.UserCode;
                            userdb.FullName = firstrecored.UserName;
                            userdb.MobileNumber = GetEncrypt(firstrecored.UserMobile);
                            userdb.EmailConfirmed = false;
                            userdb.Active = true;
                            userdb.CreateDate = DateTime.Now;
                            userdb.MobileNumberConfirmed = false;
                            userdb.LockoutEnabled = false;
                            userdb.AccessFailedCount = 0;
                            userdb.UserTypeID = 1;

                            RokhsarehClubDb.Users.Add(userdb);
                        }

                        // پیمایش جداول product و productgroup
                        foreach(var item in jsonmodel)
                        {
                            // در جدول ClubFacture فیلدهای دریافتی را ذخیره میکنیم
                            // اضافه کردن اطلاعات در جدول Product
                            if (!RokhsarehClubDb.Products.Any(u => u.ProductName == item.ProductName))
                            {
                                Product product = new Product();
                                product.BusinessUnitId = 1;
                                product.ProductTypeId = item.ProductTypeId;
                                product.ProductName = item.ProductName;
                                product.ProductCode = "";

                                RokhsarehClubDb.Products.Add(product);
                            }

                            // اضافه کردن اطلاعات در جدول  Product Group
                            if (!RokhsarehClubDb.ProductGroups.Any(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == 1) && item.ProductGroupName != null)
                            {
                                ProductGroup productGroup = new ProductGroup();
                                productGroup.BusinessUnitId = 1;
                                productGroup.ProductGroupName = item.ProductGroupName;

                                RokhsarehClubDb.ProductGroups.Add(productGroup);
                            }
                        }

                        RokhsarehClubDb.SaveChanges();

                        // دریافت اطلاعات کاربر 
                        var user = RokhsarehClubDb.Users.FirstOrDefault(u => u.UserCode == firstrecored.UserCode && u.MobileNumber == firstrecored.UserMobile && u.FullName == firstrecored.UserName);

                        foreach(var item in jsonmodel)
                        {
                            // ذخیره  سازی در جدول Club Facture
                            ClubFacture clubFacture = new ClubFacture();
                            clubFacture.BusinessUnitId = 1;
                            clubFacture.FactureId = item.FactureId;
                            clubFacture.FactureTypeId = item.FactureTypeId;
                            clubFacture.UserId = user.UserID;
                            clubFacture.FactureDate = item.FactureDate.Value;
                            clubFacture.FacturePrice = item.FacturePrice;
                            clubFacture.UserPayment = firstrecored.UserPayment;
                            clubFacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == item.ProductTypeId && u.ProductName == item.ProductName).ProductId;
                            clubFacture.ProductPrice = item.ProductPrice;
                            if (item.ProductGroupName != null)
                                clubFacture.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == 1).ProductGroupId;
                            clubFacture.ProductName = item.ProductName;
                            clubFacture.ProductCount = item.ProductCount;
                            clubFacture.BranchId = 1;
                            if (item.Creator.HasValue)
                                clubFacture.Creator = item.Creator.Value;
                            clubFacture.CreatorDate = DateTime.Now;

                            RokhsarehClubDb.ClubFactures.Add(clubFacture);
                        }

                        RokhsarehClubDb.SaveChanges();

                        int sumcreditamount = 0;
                        foreach(var item in jsonmodel)
                        {
                            // اضافه کردن اطلاعات در جدول Credit
                            Credit credit = new Credit();
                            credit.CreditAmount = (item.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                            // دریافت مجموع اعتبار های دریافتی مشتری
                            sumcreditamount += credit.CreditAmount;

                            credit.ClubFactureId = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == item.FactureId && u.BusinessUnitId == 1 && u.BranchId == 1).ClubFactureId;
                            credit.TotalCreditNow = RokhsarehClubDb.Credits.Sum(u => u.CreditAmount) + credit.CreditAmount;
                            credit.CreditTypeId = 1;
                            credit.CreditStatusId = 1;
                            credit.UserId = user.UserID;
                            credit.Creator = item.Creator.Value;
                            credit.CreateDate = DateTime.Now;

                            RokhsarehClubDb.Credits.Add(credit);
                        }

                        // آماده سازی سرویس کاوه نگار برای ارسال پیامک
                        Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(businessunit.SmsApiKey);

                        //دریافت اطلاعات پایگاه داده برای خواندن Template های سرویس
                        // افزایش اعتبار پنل
                        var smstemplatetype = RokhsarehClubDb.SMSTemplateTypes.FirstOrDefault(u => u.SMSTemplateTypeId == 1);
                        var smstemplate = RokhsarehClubDb.SMSTemplates.FirstOrDefault(u => u.ClubPlanId == businessunitclubplan.ClubPlanId && u.SMSTemplateTypeId == smstemplatetype.SMSTemplateTypeId);
                        var smstemplatetokens = RokhsarehClubDb.SMSTemplateTokens.Where(u => u.SMSTemplateId == smstemplate.SMSTemplateId);

                        try
                        {
                            RokhsarehClubDb.SaveChanges();

                            if(smstemplatetokens.Count() == 3)
                            {
                                // جاگذاری اطلاعات مربوط به token
                                string token1 = user.UserName;
                                string token2 = sumcreditamount.ToString() + " تومان";
                                string token3 = RokhsarehClubDb.Credits.Sum(u => u.CreditAmount).ToString() + " تومان";
                                api.VerifyLookup(firstrecored.UserMobile, token1, token2, token3, businessunitclubplan.CreditEnhanceSMSTemplate);
                            }

                            resultmodel.Result = true;
                            resultmodel.Message = "فاکتور با موفقیت ذخیره شد";
                        }
                        catch (Exception)
                        {
                            resultmodel.Result = false;
                            resultmodel.Message = "متاسفانه اختلالی در ثبت فاکتور بوجود آمده است. دقایقی دیگر مجددا امتحان کنید";
                        }
                    }
                    else
                    {
                        resultmodel.Result = false;
                        resultmodel.Message = "برای واحد ارسال شده پلن مشتری تعریف نشده است";
                    }
                }
                else
                {
                    resultmodel.Result = false;
                    resultmodel.Message = "توکن اشتباه است";
                }
            }
            else
            {
                resultmodel.Result = false;
                resultmodel.Message = "لطفا توکن را ارسال کنید";
            }

            return Json(resultmodel);
        }

        #region FUNC
        public string GetEncrypt(string Mobile)
        {
            var client = new RestSharp.RestClient("https://sourceit.ir/SendSMS/encryptmobile?Mobile=" + Mobile);
            RestSharp.RestRequest req = new RestSharp.RestRequest(RestSharp.Method.GET);
            req.RequestFormat = RestSharp.DataFormat.Json;

            var response = client.Execute(req);

            return response.Content.ToString();
        }
        #endregion
    }
}