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
            FactureViewJsonModel jsonmodel = new FactureViewJsonModel();
            jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<FactureViewJsonModel>(content);

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

                        // بررسی اینکه آیا اطلاعات ارسال شده مربوط به کاربر، وجود دارد یا خیر
                        if (RokhsarehClubDb.Users.Any(u => u.UserCode == jsonmodel.UserCode && u.MobileNumber == jsonmodel.UserMobile && u.FullName == jsonmodel.UserName))
                        {
                            Rokhsare.Models.User userdb = new User();
                            userdb.BusinessUnitId = 1;
                            userdb.NationalNumber = "";
                            userdb.RokhsarehUserId = jsonmodel.UserId;
                            userdb.UserCode = jsonmodel.UserCode;
                            userdb.FullName = jsonmodel.UserName;
                            userdb.MobileNumber = jsonmodel.UserMobile;
                            userdb.EmailConfirmed = false;
                            userdb.Active = true;
                            userdb.CreateDate = DateTime.Now;
                            userdb.MobileNumberConfirmed = false;
                            userdb.LockoutEnabled = false;
                            userdb.AccessFailedCount = 0;

                            RokhsarehClubDb.Users.Add(userdb);
                        }

                        // در جدول ClubFacture فیلدهای دریافتی را ذخیره میکنیم
                        // اضافه کردن اطلاعات در جدول Product
                        if (!RokhsarehClubDb.Products.Any(u => u.ProductName == jsonmodel.ProductName))
                        {
                            Product product = new Product();
                            product.BusinessUnitId = 1;
                            product.ProductTypeId = jsonmodel.ProductTypeId;
                            product.ProductName = jsonmodel.ProductName;
                            product.ProductCode = "";

                            RokhsarehClubDb.Products.Add(product);
                        }

                        // اضافه کردن اطلاعات در جدول  Product Group
                        if (!RokhsarehClubDb.ProductGroups.Any(u => u.ProductGroupName == jsonmodel.ProductGroupName && u.BusinessUnitId == 1))
                        {
                            ProductGroup productGroup = new ProductGroup();
                            productGroup.BusinessUnitId = 1;
                            productGroup.ProductGroupName = jsonmodel.ProductGroupName;

                            RokhsarehClubDb.ProductGroups.Add(productGroup);
                        }

                        RokhsarehClubDb.SaveChanges();

                        // دریافت اطلاعات کاربر 
                        var user = RokhsarehClubDb.Users.FirstOrDefault(u => u.UserCode == jsonmodel.UserCode && u.MobileNumber == jsonmodel.UserMobile && u.FullName == jsonmodel.UserName);

                        // ذخیره  سازی در جدول Club Facture
                        ClubFacture clubFacture = new ClubFacture();
                        clubFacture.BusinessUnitId = 1;
                        clubFacture.FactureId = jsonmodel.FactureId;
                        clubFacture.FactureTypeId = jsonmodel.FactureTypeId;
                        clubFacture.UserId = user.UserID;
                        clubFacture.FactureDate = jsonmodel.FactureDate.Value;
                        clubFacture.FacturePrice = jsonmodel.FacturePrice;
                        clubFacture.UserPayment = jsonmodel.UserPayment;
                        clubFacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == jsonmodel.ProductTypeId && u.ProductName == jsonmodel.ProductName).ProductId;
                        clubFacture.ProductPrice = jsonmodel.ProductPrice;
                        clubFacture.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == jsonmodel.ProductGroupName && u.BusinessUnitId == 1).ProductGroupId;
                        clubFacture.ProductName = jsonmodel.ProductName;
                        clubFacture.ProductCount = jsonmodel.ProductCount;
                        clubFacture.BranchId = 1;
                        clubFacture.Creator = jsonmodel.Creator.Value;
                        clubFacture.CreatorDate = DateTime.Now;

                        RokhsarehClubDb.ClubFactures.Add(clubFacture);

                        RokhsarehClubDb.SaveChanges();

                        // اضافه کردن اطلاعات در جدول Credit
                        Credit credit = new Credit();
                        credit.CreditAmount = (jsonmodel.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                        credit.ClubFactureId = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == jsonmodel.FactureId && u.BusinessUnitId == 1 && u.BranchId == 1).ClubFactureId;
                        credit.TotalCreditNow = RokhsarehClubDb.Credits.Sum(u => u.CreditAmount) + credit.CreditAmount;
                        credit.CreditTypeId = 1;
                        credit.CreditStatusId = 1;
                        credit.UserId = user.UserID;
                        credit.Creator = jsonmodel.Creator.Value;
                        credit.CreateDate = DateTime.Now;

                        RokhsarehClubDb.Credits.Add(credit);

                        try
                        {
                            RokhsarehClubDb.SaveChanges();

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
    }
}