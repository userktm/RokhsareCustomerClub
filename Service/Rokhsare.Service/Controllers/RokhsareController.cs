using Rokhsare.Cache;
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

            try
            {
                if(jsonmodel.Count == 0)
                {
                    resultmodel.Result = false;
                    resultmodel.Message = "رکوردی ارسال نشده است";
                }
                else
                {
                    if (header.Contains("Token"))
                    {
                        string token = header.GetValues("Token").First();

                        if (token == "42345")
                        {
                            // با توجه به اینکه تعداد رکورد ها ممکن است بیش از 1 باشد، و بعضی فیلدهای رکورد ها یکسان است
                            // ما اطلاعات یک فیلد را دریافت و برای مشخص شدن اطلاعاتی نظیر کاربر و مبلغ پرداختی استفاده میکنیم
                            var firstrecored = jsonmodel.First();

                            // بررسی club plan مربوط به business
                            if (RokhsarehClubDb.BusinesUnitClubPlans.Any(u => u.BusinesUnitId == firstrecored.ClubBusinessUnitID) || RokhsarehClubDb.DefaultClubPlans.Any(u => u.BusinessUnitId == firstrecored.ClubBranchID))
                            {
                                var businessunitclubplan = RokhsarehClubDb.BusinesUnitClubPlans.FirstOrDefault(u => u.BusinesUnitId == firstrecored.ClubBusinessUnitID).ClubPlan;
                                var clubplanid = RokhsarehClubDb.ClubPlanDetails.FirstOrDefault(u => u.ClubPlanDetailId == businessunitclubplan.ClubPlanDetailId);
                                var businessunit = RokhsarehClubDb.BusinessUnits.FirstOrDefault(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID);

                                // جهت بررسی موبایل کاربر ابتدا آن را کد میکنیم
                                string ebMobileNumber = GetEncrypt(firstrecored.UserMobile);
                                // بررسی اینکه آیا اطلاعات ارسال شده مربوط به کاربر، وجود دارد یا خیر
                                if (!RokhsarehClubDb.Users.Any(u => u.MobileNumber == ebMobileNumber && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                {
                                    Rokhsare.Models.User userdb = new User();
                                    userdb.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                    //userdb.NationalNumber = "";
                                    userdb.FullName = firstrecored.UserName;
                                    userdb.MobileNumber = ebMobileNumber;
                                    userdb.EmailConfirmed = false;
                                    userdb.Active = true;
                                    userdb.CreateDate = DateTime.Now;
                                    userdb.MobileNumberConfirmed = false;
                                    userdb.LockoutEnabled = false;
                                    userdb.AccessFailedCount = 0;
                                    userdb.UserTypeID = 1;

                                    RokhsarehClubDb.Users.Add(userdb);
                                    RokhsarehClubDb.SaveChanges();

                                    // بعد از ثبت کاربر نیاز است نقش او را در سیستم تعیین و ثبت کنیم
                                    UserRole userRole = new UserRole();
                                    userRole.UserId = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == ebMobileNumber && u.FullName == firstrecored.UserName).UserID;
                                    userRole.RoleId = 1;
                                    userRole.ExpireDate = null;

                                    RokhsarehClubDb.UserRoles.Add(userRole);
                                }

                                // در اینجا فرستنده را در پایگاه داده چک میکنیم
                                // جهت بررسی موبایل کاربر ابتدا آن را کد میکنیم
                                string enMobileNumber = GetEncrypt(firstrecored.CreatorMobile);
                                // با توجه به استثنایی که ممکن است مشتری خود کارمند باشد، ابتدا به عنوان مشتری فرستنده را چک میکنیم
                                if (!RokhsarehClubDb.Users.Any(u => u.MobileNumber == enMobileNumber && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                {
                                    // اگر فرستنده که نقش کارمند دارد در پایگاه داده وجود نداشت، ایجاد میکنیم
                                    Rokhsare.Models.User userdb = new User();
                                    userdb.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                    //userdb.NationalNumber = "";
                                    userdb.FullName = firstrecored.CreatorName;
                                    userdb.MobileNumber = enMobileNumber;
                                    userdb.EmailConfirmed = false;
                                    userdb.Active = true;
                                    userdb.CreateDate = DateTime.Now;
                                    userdb.MobileNumberConfirmed = false;
                                    userdb.LockoutEnabled = false;
                                    userdb.AccessFailedCount = 0;
                                    userdb.UserTypeID = 2;

                                    RokhsarehClubDb.Users.Add(userdb);
                                    RokhsarehClubDb.SaveChanges();

                                    // بعد از ثبت کاربر نیاز است نقش او را در سیستم تعیین و ثبت کنیم
                                    UserRole userRole = new UserRole();
                                    userRole.UserId = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                    userRole.RoleId = 2;
                                    userRole.ExpireDate = null;

                                    RokhsarehClubDb.UserRoles.Add(userRole);
                                }

                                // پیمایش جداول product و productgroup
                                foreach (var item in jsonmodel)
                                {
                                    // در جدول ClubFacture فیلدهای دریافتی را ذخیره میکنیم
                                    // اضافه کردن اطلاعات در جدول  Product Group
                                    if (!RokhsarehClubDb.ProductGroups.Any(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID) && (item.ProductGroupName != null && item.ProductGroupName != ""))
                                    {
                                        ProductGroup productGroup = new ProductGroup();
                                        productGroup.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                        productGroup.ProductGroupName = item.ProductGroupName;

                                        RokhsarehClubDb.ProductGroups.Add(productGroup);
                                        RokhsarehClubDb.SaveChanges();
                                    }

                                    // اضافه کردن اطلاعات در جدول Product
                                    if(!RokhsarehClubDb.Products.Any(u => u.ProductCode == item.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                    {
                                        if (!RokhsarehClubDb.Products.Any(u => u.ProductName == item.ProductName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                        {
                                            Product product = new Product();
                                            product.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                            product.ProductTypeId = item.ProductTypeId;
                                            product.ProductName = item.ProductName;
                                            product.ProductCode = item.ProductId;
                                            if (item.ProductGroupName != null && item.ProductGroupName != "")
                                                product.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID).ProductGroupId;

                                            RokhsarehClubDb.Products.Add(product);
                                        }
                                        else
                                        {
                                            var product = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductName == item.ProductName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                            if (item.ProductGroupName != null && item.ProductGroupName != "")
                                                product.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID).ProductGroupId;
                                            product.ProductCode = item.ProductId;

                                            RokhsarehClubDb.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        var product = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductCode == item.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                        if (item.ProductGroupName != null && item.ProductGroupName != "")
                                            product.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID).ProductGroupId;
                                        product.ProductName = item.ProductName;

                                        RokhsarehClubDb.SaveChanges();
                                    }
                                }

                                RokhsarehClubDb.SaveChanges();

                                // دریافت اطلاعات کاربر 
                                var user = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == ebMobileNumber && u.FullName == firstrecored.UserName);

                                // در اینجا فاکتور ها را ثبت میکنیم
                                // ابتدا بررسی میکنیم فاکتور وجود داشته است یا خیر
                                // در صورت وجود وضعیت آن را بررسی میکنیم
                                // اگر ویرایش شده بود، اطلاعات فاکتور را ویرایش و سپس credit را تغییر میدهیم
                                // اگر حذف شده بود، وضعیت فاکتور و credit را نیز تغییر میدهیم

                                // در اینجا credit ها را ذخیره میکنیم
                                int sumcreditamount = 0;

                                foreach (var item in jsonmodel)
                                {
                                    // بررسی فاکتور های ذخیره شده
                                    // ابتدا محصولی ثبت شده در پایگاه داده را پیدا میکنیم
                                    var product = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductName == item.ProductName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                    if (RokhsarehClubDb.ClubFactures.Any(u => u.FactureId == item.FactureId && u.ProductId == product.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                    {
                                        // در این جا مشخص شده این فاکتور وجود داشته و نیاز به ویرایش دارد
                                        // ابتدا بررسی میکنیم وضعیت آن به حالت حذف شده در آمده یا نه

                                        var updatedfacture = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == item.FactureId && u.ProductId == product.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                        if(updatedfacture.ClubFactureStatusId == 1 || updatedfacture.ClubFactureStatusId == 2)
                                        {
                                            if (item.IsDeleted == 1)
                                            {
                                                // در این جا مشخص شده است که وضعیت فاکتور به حالت حذف شده در آمده است
                                                updatedfacture.ClubFactureStatusId = 3;

                                                // بعد از تغییر فاکتور، credit آن را نیز تغییر میدهیم
                                                var facturecredit = RokhsarehClubDb.Credits.FirstOrDefault(u => u.ClubFactureId == updatedfacture.ClubFactureId);

                                                facturecredit.CreditStatusId = 3;
                                                facturecredit.ModifireDate = DateTime.Now;
                                                facturecredit.Modifire = user.UserID;
                                            }
                                            else
                                            {
                                                // در اینجا مشخص است وضعیت فاکتور به حالت تغییر یافته درآمده است
                                                // بعد از تغییر فاکنور credit آن را هم عوض میکنیم
                                                updatedfacture.FactureId = item.FactureId;
                                                updatedfacture.FactureTypeId = item.FactureTypeId;
                                                updatedfacture.FactureDate = item.FactureDate.Value;
                                                updatedfacture.FacturePrice = item.FacturePrice;
                                                updatedfacture.UserPayment = firstrecored.UserPayment;
                                                updatedfacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == item.ProductTypeId && u.ProductName == item.ProductName).ProductId;
                                                updatedfacture.ProductPrice = item.ProductPrice;
                                                updatedfacture.ProductCount = item.ProductCount;
                                                updatedfacture.BranchId = firstrecored.ClubBranchID;
                                                if (!string.IsNullOrEmpty(item.CreatorMobile))
                                                    updatedfacture.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                                updatedfacture.ClubFactureStatusId = 2;

                                                // بعد از تغییر فاکتور، credit آن را نیز تغییر میدهیم
                                                var facturecredit = RokhsarehClubDb.Credits.FirstOrDefault(u => u.ClubFactureId == updatedfacture.ClubFactureId);
                                                if (facturecredit == null)
                                                {
                                                    // اضافه کردن اطلاعات در جدول Credit
                                                    Credit credit = new Credit();
                                                    credit.CreditAmount = (item.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                                                    // دریافت مجموع اعتبار های دریافتی مشتری
                                                    sumcreditamount += credit.CreditAmount;

                                                    int productiditem = Convert.ToInt32(RokhsarehClubDb.Products.FirstOrDefault(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.ProductName == item.ProductName && u.ProductTypeId == item.ProductTypeId).ProductId);
                                                    credit.ClubFactureId = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == item.FactureId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.BranchId == firstrecored.ClubBranchID && u.ProductId == productiditem).ClubFactureId;

                                                    credit.CreditTypeId = 1;
                                                    credit.CreditStatusId = 1;
                                                    credit.UserId = user.UserID;
                                                    credit.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                                    credit.CreateDate = DateTime.Now;
                                                    credit.CreditStatusId = 1;

                                                    RokhsarehClubDb.Credits.Add(credit);
                                                }
                                                else
                                                {
                                                    facturecredit.CreditAmount = (item.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                                                    // دریافت مجموع اعتبار های دریافتی مشتری
                                                    sumcreditamount += facturecredit.CreditAmount;

                                                    facturecredit.CreditStatusId = 2;
                                                    facturecredit.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                                    facturecredit.ModifireDate = DateTime.Now;
                                                    facturecredit.Modifire = user.UserID;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(RokhsarehClubDb.ClubFactures.Any(u => u.ClubFactureStatusId == 3 && u.FactureId == item.FactureId && u.ProductId == product.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                                            {

                                            }
                                            else
                                            {
                                                // ذخیره  سازی در جدول Club Facture
                                                ClubFacture clubFacture = new ClubFacture();
                                                clubFacture.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                                clubFacture.FactureId = item.FactureId;
                                                clubFacture.FactureTypeId = item.FactureTypeId;
                                                clubFacture.UserId = user.UserID;
                                                clubFacture.FactureDate = item.FactureDate.Value;
                                                clubFacture.FacturePrice = item.FacturePrice;
                                                clubFacture.UserPayment = firstrecored.UserPayment;
                                                clubFacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == item.ProductTypeId && u.ProductName == item.ProductName).ProductId;
                                                clubFacture.ProductPrice = item.ProductPrice;
                                                clubFacture.ProductCount = item.ProductCount;
                                                clubFacture.BranchId = 1;
                                                if (!string.IsNullOrEmpty(item.CreatorMobile))
                                                    clubFacture.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID; ;
                                                clubFacture.CreatorDate = DateTime.Now;
                                                clubFacture.ClubFactureStatusId = 1;

                                                RokhsarehClubDb.ClubFactures.Add(clubFacture);
                                                RokhsarehClubDb.SaveChanges();

                                                // اضافه کردن اطلاعات در جدول Credit
                                                Credit credit = new Credit();
                                                credit.CreditAmount = (item.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                                                // دریافت مجموع اعتبار های دریافتی مشتری
                                                sumcreditamount += credit.CreditAmount;

                                                int productiditem = Convert.ToInt32(RokhsarehClubDb.Products.FirstOrDefault(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.ProductName == item.ProductName && u.ProductTypeId == item.ProductTypeId).ProductId);
                                                credit.ClubFactureId = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == item.FactureId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.BranchId == firstrecored.ClubBranchID && u.ProductId == productiditem).ClubFactureId;

                                                credit.CreditTypeId = 1;
                                                credit.CreditStatusId = 1;
                                                credit.UserId = user.UserID;
                                                credit.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                                credit.CreateDate = DateTime.Now;
                                                credit.CreditStatusId = 1;

                                                RokhsarehClubDb.Credits.Add(credit);
                                                RokhsarehClubDb.SaveChanges();
                                            }
                                        }

                                        RokhsarehClubDb.SaveChanges();
                                    }
                                    else
                                    {
                                        // ذخیره  سازی در جدول Club Facture
                                        ClubFacture clubFacture = new ClubFacture();
                                        clubFacture.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                        clubFacture.FactureId = item.FactureId;
                                        clubFacture.FactureTypeId = item.FactureTypeId;
                                        clubFacture.UserId = user.UserID;
                                        clubFacture.FactureDate = item.FactureDate.Value;
                                        clubFacture.FacturePrice = item.FacturePrice;
                                        clubFacture.UserPayment = firstrecored.UserPayment;
                                        clubFacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == item.ProductTypeId && u.ProductName == item.ProductName).ProductId;
                                        clubFacture.ProductPrice = item.ProductPrice;
                                        clubFacture.ProductCount = item.ProductCount;
                                        clubFacture.BranchId = 1;
                                        if (!string.IsNullOrEmpty(item.CreatorMobile))
                                            clubFacture.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID; ;
                                        clubFacture.CreatorDate = DateTime.Now;
                                        clubFacture.ClubFactureStatusId = 1;

                                        RokhsarehClubDb.ClubFactures.Add(clubFacture);
                                        RokhsarehClubDb.SaveChanges();

                                        // اضافه کردن اطلاعات در جدول Credit
                                        Credit credit = new Credit();
                                        credit.CreditAmount = (item.ProductPrice * clubplanid.PercentOFGiftCredit.Value) / 100;
                                        // دریافت مجموع اعتبار های دریافتی مشتری
                                        sumcreditamount += credit.CreditAmount;

                                        int productiditem = Convert.ToInt32(RokhsarehClubDb.Products.FirstOrDefault(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.ProductName == item.ProductName && u.ProductTypeId == item.ProductTypeId).ProductId);
                                        credit.ClubFactureId = RokhsarehClubDb.ClubFactures.FirstOrDefault(u => u.FactureId == item.FactureId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID && u.BranchId == firstrecored.ClubBranchID && u.ProductId == productiditem).ClubFactureId;

                                        credit.CreditTypeId = 1;
                                        credit.CreditStatusId = 1;
                                        credit.UserId = user.UserID;
                                        credit.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                        credit.CreateDate = DateTime.Now;
                                        credit.CreditStatusId = 1;

                                        RokhsarehClubDb.Credits.Add(credit);
                                        RokhsarehClubDb.SaveChanges();
                                    }
                                }

                                // در اینجا فاکتورها را ثبت میکنیم
                                // ابتدا آی دی های فاکتور را دریافت کرده، سپس بر اساس اقلام ارسالی بررسی میکنیم
                                // اگر فاکتور از قبل در باشگاه ذخیره شده نباشد مسیر اضافه کردن را جلو میبریم
                                // اگر این فاکتور قبلا موجود باشد به نوع isdelete آن نگاه میکنیم و خود فاکتور را حذف منطقی میکنیم
                                // اگر فاکتور موجود باشد و نوع isdelete آن تغییر نکرده باشد، به تعداد اقلام نگاه میکنیم
                                // در صورت ارسال نشدن قلم جنسی آن را حذف شده تلقی میکنیم
                                // بعد از بررسی اقلام فاکتور credit آن را بررسی میکنیم
                                var getfactureid = jsonmodel.Select(u => u.FactureId).Distinct().ToList();
                                foreach(var item in getfactureid)
                                {
                                    // در اینجا فاکتور های این آیتم را بررسی میکنیم
                                    if(RokhsarehClubDb.ClubFactures.Any(u => u.FactureId == item))
                                    {
                                        // در این حالت یعنی این شماره فاکتور رخساره قبلا در سیستم ثبت شده است و اکنون یا ویرایش و یا حذف شده است
                                        var jsonmodelitem = jsonmodel.Where(u => u.FactureId == item).ToList();
                                        var firstitem = jsonmodel.First();
                                        var productitem = jsonmodelitem.Select(u => u.ProductName).ToList();
                                        var clubproduct = RokhsarehClubDb.Products.Where(u => productitem.Contains(u.ProductName) && u.BusinessUnitId == firstitem.ClubBusinessUnitID).Select(u => u.ProductId).ToList();

                                        // اقلام فاکتوری که محصول آن دریافت نشده است را حذف میکنیم
                                        var customerclubfacture = RokhsarehClubDb.ClubFactures.Where(u => u.FactureId == item && !clubproduct.Contains(u.ProductId)).ToList();
                                        var customerclubfactureid = customerclubfacture.Select(u => u.ClubFactureId).ToList();
                                        var credits = RokhsarehClubDb.Credits.Where(u => customerclubfactureid.Contains(u.ClubFactureId.Value)).ToList();

                                        foreach(var subitem in customerclubfacture)
                                        {
                                            subitem.ClubFactureStatusId = 3;
                                        }

                                        foreach (var subitem in credits)
                                        {
                                            subitem.CreditStatusId = 3;
                                        }

                                        RokhsarehClubDb.SaveChanges();
                                    }
                                }


                                // ذخیره اطلاعات فعلی
                                RokhsarehClubDb.SaveChanges();

                                // کش کردن مقدار اعتبار کاربر
                                //var key = string.Format("totalcredit_{0}", user.UserID);
                                //int _totalcredit = CacheHelper.GetData<int>(key);
                                //CacheHelper.Remove(key);
                                //if(RokhsarehClubDb.Credits.Where(u => u.UserId == user.UserID && u.CreditStatusId < 3).Count() > 0)
                                //    CacheHelper.SetDataToCacheDay(RokhsarehClubDb.Credits.Where(u => u.UserId == user.UserID && u.CreditStatusId < 3).Sum(u => u.CreditAmount), key, 1);
                                //else
                                //    CacheHelper.SetDataToCacheDay(0, key, 1);


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
                                    string smsexception = "";
                                    if (smstemplatetokens.Count() == 3)
                                    {
                                        // جاگذاری اطلاعات مربوط به token
                                        string token1 = user.FullName;
                                        string token2 = "";
                                        if (sumcreditamount == 0)
                                            token2 = "0";
                                        else
                                            token2 = sumcreditamount.ToString("#,###");
                                        string token3 = "";
                                        int allamount = 0;
                                        if(GetUserAmount(ebMobileNumber, firstrecored.ClubBusinessUnitID.Value) > 0)
                                        {
                                            allamount = GetUserAmount(ebMobileNumber, firstrecored.ClubBusinessUnitID.Value);
                                        }
                                        if (allamount == 0)
                                            token3 = "0";
                                        else
                                            token3 = allamount.ToString("#,###");

                                        string url = "https://api.kavenegar.com/v1/" + businessunit.SmsApiKey
                                            + "/verify/lookup.json?receptor=" + firstrecored.UserMobile
                                            + "&token10=" + token1 + "&token=" + token2 + "&token2=" + token3 + "&template=" + smstemplate.SMSTemplateName;
                                        var client = new RestSharp.RestClient(url);
                                        RestSharp.RestRequest req = new RestSharp.RestRequest(RestSharp.Method.GET);
                                        var response = client.Execute(req);
                                        smsexception = response.Content;
                                    }

                                    resultmodel.Result = true;
                                    resultmodel.Message = "فاکتور با موفقیت ذخیره شد";
                                    resultmodel.Model = smsexception;
                                }
                                catch (Exception ex)
                                {
                                    resultmodel.Result = false;
                                    resultmodel.Message = "متاسفانه اختلالی در ثبت فاکتور بوجود آمده است. دقایقی دیگر مجددا امتحان کنید";
                                    resultmodel.Message += " " + ex.Message;
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
                }
            }
            catch(Exception ex)
            {
                resultmodel.Result = false;
                resultmodel.Message = ex.InnerException.InnerException.Message;
            }


            return Json(resultmodel);
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetUserAmount()
        {
            ResultModel resultmodel = new ResultModel();

            var re = Request;
            var header = re.Headers;
            var content = re.Content.ReadAsStringAsync().Result;

            content = content.Replace("\\", string.Empty);
            content = content.Trim('"');

            // دریافت ویو مربوط به ذخیره سازی فاکتور از سمت برنامه رخساره
            UserAmountJsonModel jsonmodel = new UserAmountJsonModel();
            jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAmountJsonModel>(content);

            if (header.Contains("Token"))
            {
                string token = header.GetValues("Token").First();

                if (token == "42345")
                {
                    string enMobileNumber = GetEncrypt(jsonmodel.UserMobile);

                    // بررسی وجود کاربر در سیستم
                    if (RokhsarehClubDb.Users.Any(u => u.MobileNumber == enMobileNumber))
                    {
                        // چنانچه کاربر وجود داشت ابتدا مقدار کش را فراخوانی میکنیم
                        // درصورتی که مقدار کش خالی باشد اطلاعات را از پایگاه داده دریافت میکنیم

                        GetUserAmountModel getUserAmountModel = new GetUserAmountModel();

                        var user = RokhsarehClubDb.Users.FirstOrDefault(u => (u.MobileNumber == enMobileNumber) && u.BusinessUnitId == jsonmodel.BusinesID);
                        var bussinesunit = RokhsarehClubDb.BusinessUnits.FirstOrDefault(u => u.BusinessUnitId == jsonmodel.BusinesID);
                        var key = string.Format("totalcredit_{0}", user.UserID);
                        CacheHelper.Remove(key);
                        object _totalcredit = CacheHelper.GetData<object>(key);

                        if(_totalcredit != null)
                        {
                            getUserAmountModel.TotalCredit = Convert.ToInt32(_totalcredit);
                            if (bussinesunit.LimitUseCreditResort.HasValue)
                            {
                                if(RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID && u.ClubFactureStatusId < 3).Count() > 0)
                                {
                                    if (RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID).Select(u => u.FactureId).Distinct().Count() >= bussinesunit.LimitUseCreditResort)
                                        getUserAmountModel.UsableTotalCredit = Convert.ToInt32(_totalcredit);
                                    else
                                        getUserAmountModel.UsableTotalCredit = 0;
                                }
                                else
                                {
                                    getUserAmountModel.UsableTotalCredit = 0;
                                }
                            }
                            getUserAmountModel.LimitUseCreditForce = bussinesunit.LimitUseCreditForce;
                            getUserAmountModel.LimitUseCreditResort = bussinesunit.LimitUseCreditResort;
                            getUserAmountModel.LimitUserCreditPercent = bussinesunit.LimitUserCreditPercent;

                            resultmodel.Result = true;
                            resultmodel.Model = getUserAmountModel;
                        }
                        else
                        {
                            if (RokhsarehClubDb.Credits.Any(u => u.UserId == user.UserID))
                            {
                                var lastcredit = RokhsarehClubDb.Credits.Where(u => u.UserId == user.UserID && u.CreditStatusId < 3).Sum(u => u.CreditAmount);

                                getUserAmountModel.TotalCredit = lastcredit;
                                if (bussinesunit.LimitUseCreditResort.HasValue)
                                {
                                    if (RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID && u.ClubFactureStatusId < 3).Count() > 0)
                                    {
                                        if (RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID).Select(u => u.FactureId).Distinct().Count() >= bussinesunit.LimitUseCreditResort)
                                            getUserAmountModel.UsableTotalCredit = Convert.ToInt32(lastcredit);
                                        else
                                            getUserAmountModel.UsableTotalCredit = 0;
                                    }
                                    else
                                    {
                                        getUserAmountModel.UsableTotalCredit = 0;
                                    }
                                }
                                getUserAmountModel.LimitUseCreditForce = bussinesunit.LimitUseCreditForce;
                                getUserAmountModel.LimitUseCreditResort = bussinesunit.LimitUseCreditResort;
                                getUserAmountModel.LimitUserCreditPercent = bussinesunit.LimitUserCreditPercent;

                                resultmodel.Result = true;
                                resultmodel.Model = getUserAmountModel;

                                CacheHelper.SetDataToCacheDay(lastcredit, key, 1);
                            }
                            else
                            {
                                getUserAmountModel.TotalCredit = 0;
                                getUserAmountModel.UsableTotalCredit = 0;
                                getUserAmountModel.LimitUseCreditForce = bussinesunit.LimitUseCreditForce;
                                getUserAmountModel.LimitUseCreditResort = bussinesunit.LimitUseCreditResort;
                                getUserAmountModel.LimitUserCreditPercent = bussinesunit.LimitUserCreditPercent;

                                resultmodel.Result = true;
                                resultmodel.Model = getUserAmountModel;

                                CacheHelper.SetDataToCacheDay(0, key, 1);
                            }
                        }
                    }
                    else
                    {
                        resultmodel.Result = false;
                        resultmodel.Message = "مشتری در سیستم وجود ندارد";
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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult UseCreditFactureViewSend()
        {
            ResultModel resultmodel = new ResultModel();

            var re = Request;
            var header = re.Headers;
            var content = re.Content.ReadAsStringAsync().Result;

            content = content.Replace("\\", string.Empty);
            content = content.Trim('"');

            // دریافت ویو مربوط به ذخیره سازی فاکتور از سمت برنامه رخساره
            UseCreditFactureJsonModel jsonmodel = new UseCreditFactureJsonModel();
            jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UseCreditFactureJsonModel>(content);

            if (header.Contains("Token"))
            {
                string token = header.GetValues("Token").First();

                if (token == "42345")
                {
                    // با توجه به اینکه تعداد رکورد ها ممکن است بیش از 1 باشد، و بعضی فیلدهای رکورد ها یکسان است
                    // ما اطلاعات یک فیلد را دریافت و برای مشخص شدن اطلاعاتی نظیر کاربر و مبلغ پرداختی استفاده میکنیم
                    var firstrecored = jsonmodel.Factures.First();

                    // بررسی club plan مربوط به business
                    if (RokhsarehClubDb.BusinesUnitClubPlans.Any(u => u.BusinesUnitId == firstrecored.ClubBusinessUnitID) || RokhsarehClubDb.DefaultClubPlans.Any(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                    {
                        var businessunitclubplan = RokhsarehClubDb.BusinesUnitClubPlans.FirstOrDefault(u => u.BusinesUnitId == firstrecored.ClubBusinessUnitID).ClubPlan;
                        var clubplanid = RokhsarehClubDb.ClubPlanDetails.FirstOrDefault(u => u.ClubPlanDetailId == businessunitclubplan.ClubPlanDetailId);
                        var businessunit = RokhsarehClubDb.BusinessUnits.FirstOrDefault(u => u.BusinessUnitId == firstrecored.ClubBusinessUnitID);

                        // جهت بررسی موبایل کاربر ابتدا آن را کد میکنیم
                        string ebMobileNumber = GetEncrypt(firstrecored.UserMobile);
                        // بررسی اینکه آیا اطلاعات ارسال شده مربوط به کاربر، وجود دارد یا خیر
                        if (!RokhsarehClubDb.Users.Any(u => u.MobileNumber == ebMobileNumber && u.BusinessUnitId == firstrecored.ClubBusinessUnitID))
                        {
                            Rokhsare.Models.User userdb = new User();
                            userdb.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                            //userdb.NationalNumber = "";
                            userdb.FullName = firstrecored.UserName;
                            userdb.MobileNumber = ebMobileNumber;
                            userdb.EmailConfirmed = false;
                            userdb.Active = true;
                            userdb.CreateDate = DateTime.Now;
                            userdb.MobileNumberConfirmed = false;
                            userdb.LockoutEnabled = false;
                            userdb.AccessFailedCount = 0;
                            userdb.UserTypeID = 1;

                            RokhsarehClubDb.Users.Add(userdb);
                            RokhsarehClubDb.SaveChanges();

                            // بعد از ثبت کاربر نیاز است نقش او را در سیستم تعیین و ثبت کنیم
                            UserRole userRole = new UserRole();
                            userRole.UserId = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == ebMobileNumber && u.FullName == firstrecored.UserName).UserID;
                            userRole.RoleId = 1;
                            userRole.ExpireDate = DateTime.Now.AddYears(1);

                            RokhsarehClubDb.UserRoles.Add(userRole);
                        }
                        else
                        {
                            // در اینجا بررسی میکنیم کاربر موجود چنانچه شماره موبایلش عوض شده باشد در پایگاه داده تغییر میدهیم
                            var defaultuser = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == firstrecored.UserMobile);

                            string EebMobileNumber = GetEncrypt(defaultuser.MobileNumber);
                            if (ebMobileNumber != EebMobileNumber)
                            {
                                defaultuser.MobileNumber = ebMobileNumber;
                                defaultuser.FullName = firstrecored.UserName;
                                RokhsarehClubDb.SaveChanges();
                            }
                        }

                        // در اینجا فرستنده را در پایگاه داده چک میکنیم
                        // جهت بررسی موبایل کاربر ابتدا آن را کد میکنیم
                        string enMobileNumber = GetEncrypt(firstrecored.CreatorMobile);
                        if (!RokhsarehClubDb.Users.Any(u => u.MobileNumber == enMobileNumber && u.BusinessUnitId == firstrecored.ClubBusinessUnitID  && u.FullName == firstrecored.CreatorName))
                        {
                            // اگر فرستنده که نقش کارمند دارد در پایگاه داده وجود نداشت، ایجاد میکنیم
                            Rokhsare.Models.User userdb = new User();
                            userdb.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                            //userdb.NationalNumber = "";
                            userdb.FullName = firstrecored.CreatorName;
                            userdb.MobileNumber = enMobileNumber;
                            userdb.EmailConfirmed = false;
                            userdb.Active = true;
                            userdb.CreateDate = DateTime.Now;
                            userdb.MobileNumberConfirmed = false;
                            userdb.LockoutEnabled = false;
                            userdb.AccessFailedCount = 0;
                            userdb.UserTypeID = 2;

                            RokhsarehClubDb.Users.Add(userdb);
                            RokhsarehClubDb.SaveChanges();

                            // بعد از ثبت کاربر نیاز است نقش او را در سیستم تعیین و ثبت کنیم
                            UserRole userRole = new UserRole();
                            userRole.UserId = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == ebMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                            userRole.RoleId = 2;
                            userRole.ExpireDate = DateTime.Now.AddYears(1);

                            RokhsarehClubDb.UserRoles.Add(userRole);
                        }

                        // پیمایش جداول product و productgroup
                        foreach (var item in jsonmodel.Factures)
                        {
                            // در جدول ClubFacture فیلدهای دریافتی را ذخیره میکنیم
                            // اضافه کردن اطلاعات در جدول  Product Group
                            if (!RokhsarehClubDb.ProductGroups.Any(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID) && (item.ProductGroupName != null && item.ProductGroupName != ""))
                            {
                                ProductGroup productGroup = new ProductGroup();
                                productGroup.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                productGroup.ProductGroupName = item.ProductGroupName;

                                RokhsarehClubDb.ProductGroups.Add(productGroup);
                                RokhsarehClubDb.SaveChanges();
                            }

                            // اضافه کردن اطلاعات در جدول Product
                            if (!RokhsarehClubDb.Products.Any(u => u.ProductCode == item.ProductId))
                            {
                                if (!RokhsarehClubDb.Products.Any(u => u.ProductName == item.ProductName))
                                {
                                    Product product = new Product();
                                    product.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                    product.ProductTypeId = item.ProductTypeId;
                                    product.ProductName = item.ProductName;
                                    product.ProductCode = item.ProductId;
                                    if (item.ProductGroupName != null && item.ProductGroupName != "")
                                        product.ProductGroupId = RokhsarehClubDb.ProductGroups.FirstOrDefault(u => u.ProductGroupName == item.ProductGroupName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID).ProductGroupId;

                                    RokhsarehClubDb.Products.Add(product);
                                }
                                else
                                {
                                    var product = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductName == item.ProductName && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                    product.ProductCode = item.ProductId;

                                    RokhsarehClubDb.SaveChanges();
                                }
                            }
                            else
                            {
                                var product = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductCode == item.ProductId && u.BusinessUnitId == firstrecored.ClubBusinessUnitID);
                                product.ProductName = item.ProductName;

                                RokhsarehClubDb.SaveChanges();
                            }
                        }

                        RokhsarehClubDb.SaveChanges();

                        // دریافت اطلاعات کاربر 
                        var user = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == ebMobileNumber && u.FullName == firstrecored.UserName);

                        var key = string.Format("totalcredit_{0}", user.UserID);
                        object _totalcredit = CacheHelper.GetData<object>(key);

                        if(_totalcredit == null)
                        {
                            var lastcredit = GetUserAmount(enMobileNumber, firstrecored.ClubBusinessUnitID.Value);
                            _totalcredit = lastcredit;
                        }

                        // این متغییر برای آن تعریف شده است که مطمئن شویم رخساره فاکتور تکراری برای باشگاه مشتریان ارسال نکرده باشد
                        bool iserror = false;
                        foreach (var item in jsonmodel.Factures)
                        {
                            if(!RokhsarehClubDb.ClubFactures.Any(u => u.FactureId == item.FactureId))
                            {
                                // ذخیره  سازی در جدول Club Facture
                                ClubFacture clubFacture = new ClubFacture();
                                clubFacture.BusinessUnitId = firstrecored.ClubBusinessUnitID.Value;
                                clubFacture.FactureId = item.FactureId;
                                clubFacture.FactureTypeId = item.FactureTypeId;
                                clubFacture.UserId = user.UserID;
                                clubFacture.FactureDate = item.FactureDate.Value;
                                clubFacture.FacturePrice = item.FacturePrice;

                                // این خط بررسی میکند credit بیشتری از سمت رخساره برای کاربر ثبت نشود
                                if (jsonmodel.CreditPrice > Convert.ToInt32(_totalcredit))
                                {
                                    jsonmodel.CreditPrice = Convert.ToInt32(_totalcredit);
                                }

                                if (jsonmodel.CreditPrice < firstrecored.UserPayment)
                                    clubFacture.UserPayment = firstrecored.UserPayment - jsonmodel.CreditPrice;
                                else
                                {
                                    clubFacture.UserPayment = 0;
                                }

                                clubFacture.ProductId = RokhsarehClubDb.Products.FirstOrDefault(u => u.ProductTypeId == item.ProductTypeId && u.ProductName == item.ProductName).ProductId;
                                clubFacture.ProductPrice = item.ProductPrice;
                                clubFacture.ProductCount = item.ProductCount;
                                clubFacture.BranchId = 1;
                                if (!string.IsNullOrEmpty(item.CreatorMobile))
                                    clubFacture.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                                clubFacture.CreatorDate = DateTime.Now;
                                clubFacture.ClubFactureStatusId = 1;
                                RokhsarehClubDb.ClubFactures.Add(clubFacture);
                            }
                            else
                            {
                                iserror = true;
                            }
                        }

                        RokhsarehClubDb.SaveChanges();

                        // کسر مقدار credit استفاده شده
                        if (jsonmodel.CreditPrice > 0 && iserror == false)
                        {
                            // اضافه کردن اطلاعات در جدول Credit
                            Credit credit = new Credit();
                            credit.CreditAmount = -1 * (jsonmodel.CreditPrice);

                            credit.ClubFactureId = null;

                            credit.CreditTypeId = 1;
                            credit.CreditStatusId = 1;
                            credit.UserId = user.UserID;
                            credit.Creator = RokhsarehClubDb.Users.FirstOrDefault(u => u.MobileNumber == enMobileNumber && u.FullName == firstrecored.CreatorName).UserID;
                            credit.CreateDate = DateTime.Now;

                            RokhsarehClubDb.Credits.Add(credit);

                            // کش کردن مقدار اعتبار کاربر
                            CacheHelper.Remove(key);
                            CacheHelper.SetDataToCacheDay(Convert.ToInt32(_totalcredit) - jsonmodel.CreditPrice, key, 1);
                        }


                        // آماده سازی سرویس کاوه نگار برای ارسال پیامک
                        Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(businessunit.SmsApiKey);

                        //دریافت اطلاعات پایگاه داده برای خواندن Template های سرویس
                        // افزایش اعتبار پنل
                        var smstemplatetype = RokhsarehClubDb.SMSTemplateTypes.FirstOrDefault(u => u.SMSTemplateTypeId == 2);
                        var smstemplate = RokhsarehClubDb.SMSTemplates.FirstOrDefault(u => u.ClubPlanId == businessunitclubplan.ClubPlanId && u.SMSTemplateTypeId == smstemplatetype.SMSTemplateTypeId);
                        var smstemplatetokens = RokhsarehClubDb.SMSTemplateTokens.Where(u => u.SMSTemplateId == smstemplate.SMSTemplateId);

                        try
                        {
                            RokhsarehClubDb.SaveChanges();

                            if(iserror == false)
                            {
                                if (smstemplatetokens.Count() == 3)
                                {
                                    // جاگذاری اطلاعات مربوط به token
                                    string token1 = user.FullName;
                                    string token2 = jsonmodel.CreditPrice.ToString("#,###");
                                    string token3 = "";
                                    int allamount = GetUserAmount(ebMobileNumber, firstrecored.ClubBusinessUnitID.Value);
                                    if (allamount == 0)
                                        token3 = "0";
                                    else
                                        token3 = allamount.ToString("#,###");

                                    // Send by restsharp
                                    string url = "https://api.kavenegar.com/v1/" + businessunit.SmsApiKey
                                        + "/verify/lookup.json?receptor=" + firstrecored.UserMobile 
                                        + "&token10=" + token1 + "&token=" + token2 + "&token2=" + token3 + "&template=" + smstemplate.SMSTemplateName;
                                    var client = new RestSharp.RestClient(url);
                                    RestSharp.RestRequest req = new RestSharp.RestRequest(RestSharp.Method.GET);
                                    var response = client.Execute(req);
                                }

                                resultmodel.Result = true;
                                resultmodel.Message = "فاکتور با موفقیت ذخیره شد";
                            }
                            else
                            {
                                resultmodel.Result = false;
                                resultmodel.Message = "فاکتور تکراری ارسال شده است";
                            }
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

            return response.Content.Replace('"', ' ').Trim();
        }

        public int GetUserAmount(string enMobileNumber, int BusinesID)
        {
            if (RokhsarehClubDb.Users.Any(u => u.MobileNumber == enMobileNumber))
            {
                var user = RokhsarehClubDb.Users.FirstOrDefault(u => (u.MobileNumber == enMobileNumber) && u.BusinessUnitId == BusinesID);
                var bussinesunit = RokhsarehClubDb.BusinessUnits.FirstOrDefault(u => u.BusinessUnitId == BusinesID);

                if (bussinesunit.LimitUseCreditResort.HasValue)
                {
                    if (RokhsarehClubDb.Credits.Any(u => u.UserId == user.UserID))
                    {
                        var lastcredit = RokhsarehClubDb.Credits.Where(u => u.UserId == user.UserID && u.CreditStatusId < 3).Sum(u => u.CreditAmount);

                        if (RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID && u.ClubFactureStatusId < 3).Count() > 0)
                        {
                            int listcount = RokhsarehClubDb.ClubFactures.Where(u => u.UserId == user.UserID).Select(u => u.FactureDate).Distinct().Count();
                            if (listcount >= bussinesunit.LimitUseCreditResort)
                                return lastcredit;
                            else
                                return 0;
                        }
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
        #endregion
    }
}