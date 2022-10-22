using Rokhsare.Models;
using Rokhsare.Control.Base;
using Rokhsare.Service.JsonModel;
using Rokhsare.Service.Models;
using Rokhsare.Utility;
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

namespace Farhang.Service.Controllers
{
    //public class FarhangController : BaseController
    //{
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult Login()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        LoginJsonModel jsonmodel = new LoginJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var user = FarhangDb.AppUsers.FirstOrDefault(u => u.NationalId == jsonmodel.Username);

    //                var validate = Farhang.Control.Base.Membership.FarhangMembership.ValidateUserPassword(user, jsonmodel.Password);

    //                if (FarhangDb.AppUserInRoles.Any(u => u.UserId == user.Id))
    //                {
    //                    if (validate)
    //                    {
    //                        user.LastLogin = DateTime.Now;
    //                        user.UserIsOnline = true;

    //                        Log log = new Log();
    //                        log.LogName = "ورود به سیستم";
    //                        log.UserId = user.Id;
    //                        log.Action = "کاربر " + user.FullName + " به سیستم وارد شد.";
    //                        log.CreateDate = DateTime.Now;
    //                        log.IPAddress = "MOBILEAPP";
    //                        log.Browser = "MOBILEAPP";
    //                        log.UserAgent = "MOBILEAPP";

    //                        FarhangDb.Logs.Add(log);

    //                        FarhangDb.SaveChanges();

    //                        LoginResult result = new LoginResult();
    //                        result.UserId = user.Id;
    //                        result.Username = user.NationalId;
    //                        result.Firstname = user.Firstname;
    //                        result.Lastname = user.Lastname;
    //                        result.PositionJob = user.PositionJob;
    //                        result.Proficiency = user.Proficiency;
    //                        result.AboutMe = user.AboutMe;
    //                        result.ImageId = user.ImageId;

    //                        resultmodel.Result = true;
    //                        resultmodel.Model = result;
    //                        resultmodel.Message = "کاربر " + user.FullName + " با موفقیت به سیستم وارد شد.";
    //                    }
    //                    else
    //                    {
    //                        user.AccessFailedCount++;
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = "نام کاربری و یا کلمه عبور اشتباه می باشد.";
    //                    }
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "متاسفانه کاربری شما هنوز تایید نشده است.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    #region AppUser
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetAppUsers()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var users = FarhangDb.AppUsers.ToList();

    //                List<LoginResult> finalresult = new List<LoginResult>();
    //                foreach (var user in users)
    //                {
    //                    LoginResult result = new LoginResult();
    //                    result.UserId = user.Id;
    //                    result.Username = user.NationalId;
    //                    result.Firstname = user.Firstname;
    //                    result.Lastname = user.Lastname;
    //                    result.PositionJob = user.PositionJob;
    //                    result.Proficiency = user.Proficiency;
    //                    result.AboutMe = user.AboutMe;
    //                    result.ImageId = user.ImageId;
    //                    finalresult.Add(result);
    //                }

    //                resultmodel.Result = true;
    //                resultmodel.Model = finalresult;
    //                resultmodel.Message = "لیست کاربران با موفقیت دریافت شد.";
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetUser()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        GetUserJsonModel jsonmodel = new GetUserJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var user = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                LoginResult result = new LoginResult();
    //                result.UserId = user.Id;
    //                result.Username = user.NationalId;
    //                result.Firstname = user.Firstname;
    //                result.Lastname = user.Lastname;
    //                result.PositionJob = user.PositionJob;
    //                result.Proficiency = user.Proficiency;
    //                result.AboutMe = user.AboutMe;
    //                result.ImageId = user.ImageId;

    //                resultmodel.Result = true;
    //                resultmodel.Model = result;
    //                resultmodel.Message = "کاربر مورد نظر با موفقیت دریافت شد.";
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region AppWorking
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult StartAppWorking()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        StartAppWorkingJsonModel jsonmodel = new StartAppWorkingJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<StartAppWorkingJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                DateTime nowtime = DateTime.Now;
    //                long Calenderid = 0;

    //                if (FarhangDb.Calenders.Any(u => u.Today == nowtime.Date))
    //                    Calenderid = FarhangDb.Calenders.FirstOrDefault(u => u.Today == nowtime.Date).Id;
    //                else
    //                {
    //                    Calender calen = new Calender();
    //                    calen.Today = nowtime.Date;

    //                    FarhangDb.Calenders.Add(calen);
    //                    FarhangDb.SaveChanges();

    //                    Calenderid = FarhangDb.Calenders.FirstOrDefault(u => u.Today == nowtime.Date).Id;
    //                }

    //                DateTime StartTime = new DateTime(nowtime.Year, nowtime.Month, nowtime.Day, nowtime.Hour, nowtime.Minute, nowtime.Second);

    //                AppWorking worktime = new AppWorking();
    //                worktime.UserId = jsonmodel.UserId;
    //                worktime.CalenderId = Calenderid;
    //                worktime.StartTime = StartTime;
    //                worktime.IsStop = false;

    //                FarhangDb.AppWorkings.Add(worktime);
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                try
    //                {
    //                    FarhangDb.SaveChanges();

    //                    LogSave(curentuser, "شروع کار", "استارت شروع کار توسط کاربر " + curentuser.FullName + " زده شد.", DateTime.Now);

    //                    resultmodel.Result = true;
    //                    resultmodel.Message = "استارت شروع کار توسط کاربر " + curentuser.FullName + " زده شد.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = ex.Message;
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult PauseAppWorking()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        PauseAppWorkingJsonModel jsonmodel = new PauseAppWorkingJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<PauseAppWorkingJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                DateTime nowtime = DateTime.Now;
    //                long Calenderid = 0;

    //                Calenderid = FarhangDb.Calenders.FirstOrDefault(u => u.Today == nowtime.Date).Id;

    //                DateTime EndTime = new DateTime(nowtime.Year, nowtime.Month, nowtime.Day, nowtime.Hour, nowtime.Minute, nowtime.Second);

    //                var appworking = FarhangDb.AppWorkings.FirstOrDefault(u => u.CalenderId == Calenderid && u.UserId == jsonmodel.UserId && !u.EndTime.HasValue);
    //                appworking.EndTime = EndTime;
    //                appworking.WorkDescription = jsonmodel.PauseDescription;

    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                try
    //                {
    //                    FarhangDb.SaveChanges();

    //                    LogSave(curentuser, "توقف کار", "کاربر " + curentuser.FullName + " به علت " + jsonmodel.PauseDescription + " کار را متوقف کرد.", DateTime.Now);

    //                    resultmodel.Result = true;
    //                    resultmodel.Message = "کاربر " + curentuser.FullName + " کار را متوقف کرد.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = ex.Message;
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult StopAppWorking()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        StopAppWorkingJsonModel jsonmodel = new StopAppWorkingJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<StopAppWorkingJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                DateTime nowtime = DateTime.Now;
    //                long Calenderid = 0;

    //                Calenderid = FarhangDb.Calenders.FirstOrDefault(u => u.Today == nowtime.Date).Id;

    //                DateTime EndTime = new DateTime(nowtime.Year, nowtime.Month, nowtime.Day, nowtime.Hour, nowtime.Minute, nowtime.Second);

    //                var appworking = FarhangDb.AppWorkings.FirstOrDefault(u => u.CalenderId == Calenderid && u.UserId == jsonmodel.UserId && !u.EndTime.HasValue);
    //                var starttime = FarhangDb.AppWorkings.FirstOrDefault(u => u.CalenderId == Calenderid && u.UserId == jsonmodel.UserId);
    //                appworking.EndTime = EndTime;
    //                appworking.IsStop = true;
    //                appworking.WorkDescription = jsonmodel.WorkDescription;
    //                appworking.WorkedTyped = jsonmodel.WorkedTyped;

    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                try
    //                {
    //                    FarhangDb.SaveChanges();

    //                    LogSave(curentuser, "پایان کار", "کاربر " + curentuser.FullName + " پایان روز کاری خود را با توضیح " + jsonmodel.WorkDescription + " به پایان برد.", DateTime.Now);

    //                    #region TimeResult
    //                    TimeSpan Alled = new TimeSpan();
    //                    var allreport = FarhangDb.ReportWorks.Where(u => u.UserId == jsonmodel.UserId && u.CalenderId == Calenderid).ToList();
    //                    foreach (var item in allreport)
    //                    {
    //                        Alled += item.EndTime.Value - item.StartTime.Value;
    //                    }

    //                    TimeSpan timework = new TimeSpan();
    //                    var timeworkuser = FarhangDb.AppWorkings.Where(u => u.UserId == jsonmodel.UserId && u.CalenderId == Calenderid).ToList();
    //                    foreach (var item in timeworkuser)
    //                    {
    //                        if (item.EndTime.HasValue)
    //                        {
    //                            timework += item.EndTime.Value - item.StartTime;
    //                        }
    //                        else
    //                        {
    //                            timework = appworking.EndTime.Value - item.StartTime;
    //                        }
    //                    }

    //                    if (allreport.Count > 0)
    //                    {
    //                        timework = timework - Alled;
    //                    }

    //                    string starttimefinal = "";
    //                    string endtimefinal = "";

    //                    TimeSpan predaf = new TimeSpan(12, 0, 0);
    //                    if (starttime.StartTime.TimeOfDay > predaf)
    //                        starttimefinal = starttime.StartTime.Hour + ":" + starttime.StartTime.Minute + " ب.ظ";
    //                    else
    //                        starttimefinal = starttime.StartTime.Hour + ":" + starttime.StartTime.Minute + " ق.ظ";

    //                    if (appworking.EndTime.Value.TimeOfDay > predaf)
    //                        endtimefinal = appworking.EndTime.Value.Hour + ":" + appworking.EndTime.Value.Minute + " ب.ظ";
    //                    else
    //                        endtimefinal = appworking.EndTime.Value.Hour + ":" + appworking.EndTime.Value.Minute + " ق.ظ";

    //                    TimeSpan startFilter = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == jsonmodel.UserId && u.CalenderId == Calenderid).StartTime.TimeOfDay;
    //                    TimeSpan endFilter = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == jsonmodel.UserId && u.CalenderId == Calenderid && u.IsStop == true).EndTime.Value.TimeOfDay;

    //                    int MinSectime = 0;
    //                    int MaxSectime = 0;
    //                    if (startFilter.Hours != 12 && startFilter.Hours != 00)
    //                    {
    //                        MinSectime = startFilter.Hours * 5;
    //                        MinSectime = MinSectime + (startFilter.Minutes / 12);
    //                    }
    //                    else
    //                    {
    //                        MinSectime = (startFilter.Minutes / 12);
    //                    }

    //                    if (endFilter.Hours != 12 && endFilter.Hours != 00)
    //                    {
    //                        MaxSectime = endFilter.Hours * 5;
    //                        MaxSectime = MaxSectime + (endFilter.Minutes / 12);
    //                    }
    //                    else
    //                    {
    //                        MaxSectime = (endFilter.Minutes / 12);
    //                    }
    //                    #endregion

    //                    StopWorkingResult result = new StopWorkingResult();
    //                    result.Time = timework.ToString();
    //                    result.StartTimeFinal = starttimefinal;
    //                    result.EndTimeFinal = endtimefinal;
    //                    result.MinHourtime = startFilter.Hours;
    //                    result.MinMintime = startFilter.Minutes;
    //                    result.MaxHourtime = endFilter.Hours;
    //                    result.MaxMintime = endFilter.Minutes;
    //                    result.MinSectime = MinSectime;
    //                    result.MaxSectime = MaxSectime;

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = result;
    //                    resultmodel.Message = "کاربر " + curentuser.FullName + " پایان روز کاری خود را با توضیح " + jsonmodel.WorkDescription + " به پایان برد.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = ex.Message;
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region FileManager
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public async Task<IHttpActionResult> MediaUpload()
    //    {
    //        ResultModel resultmodel = new ResultModel();
    //        try
    //        {
    //            // Check if the request contains multipart/form-data.  
    //            if (!Request.Content.IsMimeMultipartContent())
    //            {
    //                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
    //            }

    //            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
    //            //access form data  

    //            NameValueCollection formData = provider.FormData;
    //            //access files  
    //            IList<HttpContent> files = provider.Files;
    //            int UserId = Int32.Parse(formData.GetValues("UserId").First());

    //            HttpContent file1 = files[0];
    //            var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');

    //            string typecontent = "";
    //            if (file1.Headers.ContentType.ToString() == "image/jpeg")
    //            {
    //                typecontent = ".jpg";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "image/png")
    //            {
    //                typecontent = ".png";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "audio/mpeg")
    //            {
    //                typecontent = ".mp3";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
    //            {
    //                typecontent = ".docx";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "application/pdf")
    //            {
    //                typecontent = ".pdf";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "application/vnd.openxmlformats-officedocument.presentationml.presentation")
    //            {
    //                typecontent = ".pptx";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "image/webp")
    //            {
    //                typecontent = ".webp";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "image/gif")
    //            {
    //                typecontent = ".gif";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
    //            {
    //                typecontent = ".xlsx";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "video/mp4")
    //            {
    //                typecontent = ".mp4";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "audio/ogg")
    //            {
    //                typecontent = ".opus";
    //            }
    //            else if (file1.Headers.ContentType.ToString() == "application/zip" || file1.Headers.ContentType.ToString() == "application/x-zip-compressed")
    //            {
    //                typecontent = ".zip";
    //            }

    //            var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == UserId);

    //            string filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + typecontent.ToUpper().ToString();
    //            string finallfilename = filename;
    //            Stream input = await file1.ReadAsStreamAsync();
    //            string directoryName = GetDirectory(curentuser);
    //            string URL = String.Empty;
    //            string tempDocUrl = ConfigReader.ConfigReader.AdminURL;

    //            var path = HttpRuntime.AppDomainAppPath;
    //            directoryName = System.IO.Path.Combine(path, directoryName);
    //            filename = @"C:\Inetpub\vhosts\familyfarhang.com\httpdocs\Images\UploadRoot\" + curentuser.Id + @"\" + filename;
    //            //filename = @"H:\PMS project\Source\FarhangPMS\FarhangPMS\UI\Farhang.UI\Images\UploadRoot\" + curentuser.Id + @"\" + filename;

    //            //Deletion exists file  
    //            if (File.Exists(filename))
    //            {
    //                File.Delete(filename);
    //            }

    //            string DocsPath = tempDocUrl;
    //            URL = DocsPath + thisFileName;

    //            using (Stream file = File.OpenWrite(filename))
    //            {
    //                input.CopyTo(file);
    //                UploadResult uploadResult = UploadFiles(UserId, finallfilename, thisFileName, Convert.ToInt32(file1.Headers.ContentLength), file1.Headers.ContentType.ToString(), filename);
    //                file.Close();

    //                resultmodel.Result = uploadResult.Result;
    //                resultmodel.Message = uploadResult.Message;
    //                resultmodel.Model = uploadResult.FileId;
    //            }

    //            return Json(resultmodel);
    //        }
    //        catch (Exception ex)
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = ex.Message;

    //            return Json(resultmodel);
    //        }
    //    }

    //    public UploadResult UploadFiles(long UserId, string FileName, string MediaName, int ContentLength, string ContentType, string FilePath)
    //    {
    //        var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == UserId);
    //        UploadResult result = new UploadResult();
    //        if (UserId > 0)
    //        {
    //            var filemanager = new FilesManager();

    //            var fileNames = Path.GetFileName(FilePath);
    //            var strFileExtension = Path.GetExtension(FilePath).ToUpper();

    //            string directory = GetDirectory(curentuser);

    //            filemanager.FileName = FileName;

    //            string addressupload = GetDirectory(curentuser);
    //            filemanager.Address = "~/Images/UploadRoot/" + UserId + "/" + FileName;
    //            filemanager.MediaName = MediaName;
    //            filemanager.ContentLength = ContentLength;
    //            filemanager.ContnetType = ContentType;
    //            filemanager.CategoryId = 0;

    //            filemanager.FileExtention = strFileExtension;
    //            filemanager.UserId = UserId;

    //            FarhangDb.FilesManagers.Add(filemanager);

    //            try
    //            {
    //                FarhangDb.SaveChanges();

    //                int fileid = FarhangDb.FilesManagers.FirstOrDefault(u => u.Address == filemanager.Address).Id;

    //                result.Result = true;
    //                result.Message = "فایل بدرستی در هاست ذخیره شد.";
    //                result.FileId = 0;
    //                return result;
    //            }
    //            catch (Exception ex)
    //            {
    //                result.Result = false;
    //                result.Message = Newtonsoft.Json.JsonConvert.SerializeObject(filemanager) + Newtonsoft.Json.JsonConvert.SerializeObject(ex);
    //                return result;
    //            }
    //        }
    //        else
    //        {
    //            result.Result = false;
    //            result.Message = "هیچ کاربری وارد نشده است.";
    //            return result;
    //        }
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetAllFile()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        GetAllFileJsonModel jsonmodel = new GetAllFileJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllFileJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var files = FarhangDb.FilesManagers.Where(u => u.UserId == jsonmodel.UserId);

    //                if (files.Count() > 0)
    //                {
    //                    List<GetFileResult> getfiles = new List<GetFileResult>();
    //                    foreach (var item in files)
    //                    {
    //                        GetFileResult subitem = new GetFileResult();
    //                        subitem.UserId = item.UserId;
    //                        subitem.FileId = item.Id;
    //                        subitem.AddressFile = "https://familyfarhang.com/Images/UploadRoot/" + item.UserId + item.FileName;

    //                        getfiles.Add(subitem);
    //                    }

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = getfiles;
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "فایل یافت نشد.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetFile()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        GetFileJsonModel jsonmodel = new GetFileJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<GetFileJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var files = FarhangDb.FilesManagers.FirstOrDefault(u => u.Id == jsonmodel.FileId);

    //                if (files != null)
    //                {
    //                    GetFileResult subitem = new GetFileResult();
    //                    subitem.UserId = files.UserId;
    //                    subitem.FileId = files.Id;
    //                    subitem.AddressFile = "https://familyfarhang.com/Images/UploadRoot/" + files.UserId + files.FileName;

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = subitem;
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "فایل یافت نشد.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region Report
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult InsertReportWork()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        InsertReportWorkJsonModel jsonmodel = new InsertReportWorkJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<InsertReportWorkJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                #region Report
    //                TimeSpan StartTimeDate = new TimeSpan(Convert.ToInt32(jsonmodel.StartTimeHour), Convert.ToInt32(jsonmodel.StartTimeMin), 0);
    //                TimeSpan EndTimeDate = new TimeSpan(Convert.ToInt32(jsonmodel.EndTimeHour), Convert.ToInt32(jsonmodel.EndTimeMin), 0);

    //                ReportWork reportw = new ReportWork();
    //                reportw.GuidId = Guid.NewGuid();
    //                reportw.UserId = curentuser.Id;
    //                reportw.StartTime = StartTimeDate;
    //                reportw.EndTime = EndTimeDate;

    //                DateTime thistime = DateTime.Now.Date;

    //                if (FarhangDb.Calenders.Any(u => u.Today == thistime))
    //                {
    //                    reportw.CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == thistime).Id;
    //                }
    //                else
    //                {
    //                    Calender cal = new Calender();
    //                    cal.Today = thistime;

    //                    FarhangDb.Calenders.Add(cal);
    //                    FarhangDb.SaveChanges();

    //                    reportw.CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == thistime).Id;
    //                }
    //                reportw.ReportTitle = jsonmodel.ReportTitle;
    //                reportw.ReportDescription = jsonmodel.ReportDescription;

    //                if (jsonmodel.RestTime)
    //                    reportw.RestTime = true;
    //                else
    //                    reportw.RestTime = false;

    //                FarhangDb.ReportWorks.Add(reportw);
    //                #endregion

    //                try
    //                {
    //                    FarhangDb.SaveChanges();

    //                    long reportworkid = FarhangDb.ReportWorks.FirstOrDefault(u => u.GuidId == reportw.GuidId).Id;

    //                    if (jsonmodel.FilesId != null)
    //                    {
    //                        List<ReportWorkFile> workfiles = new List<ReportWorkFile>();
    //                        foreach (int item in jsonmodel.FilesId)
    //                        {
    //                            ReportWorkFile subworkfiles = new ReportWorkFile();
    //                            subworkfiles.ReportWorkId = reportworkid;
    //                            subworkfiles.FileId = item;

    //                            workfiles.Add(subworkfiles);
    //                        }

    //                        FarhangDb.ReportWorkFiles.AddRange(workfiles);
    //                        FarhangDb.SaveChanges();
    //                    }
    //                    LogSave(curentuser, "ثبت گزارش کار", "کاربر " + curentuser.FullName + " یک گزارش کار ثبت کرد.", DateTime.Now);

    //                    resultmodel.Result = true;
    //                    resultmodel.Message = "کاربر " + curentuser.FullName + " یک گزارش کار ثبت کرد.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = ex.Message;
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult InsertReportWorkByAllTime()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        InsertReportWorkByAllTimeJsonModel jsonmodel = new InsertReportWorkByAllTimeJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<InsertReportWorkByAllTimeJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                #region Report
    //                TimeSpan StartTimeDate = new TimeSpan(Convert.ToInt32(jsonmodel.StartTimeHour), Convert.ToInt32(jsonmodel.StartTimeMin), 0);
    //                TimeSpan EndTimeDate = new TimeSpan(Convert.ToInt32(jsonmodel.EndTimeHour), Convert.ToInt32(jsonmodel.EndTimeMin), 0);

    //                DateTime nowtime = DateTime.Now;
    //                long Calenderid = FarhangDb.Calenders.FirstOrDefault(u => u.Today == nowtime.Date).Id;
    //                var startappworking = FarhangDb.AppWorkings.FirstOrDefault(u => u.CalenderId == Calenderid && u.UserId == curentuser.Id);
    //                var appworking = FarhangDb.AppWorkings.FirstOrDefault(u => u.CalenderId == Calenderid && u.UserId == curentuser.Id && u.EndTime.HasValue && u.IsStop);

    //                TimeSpan AllWorkDate = appworking.EndTime.Value.TimeOfDay - startappworking.StartTime.TimeOfDay;

    //                bool istiming = false;

    //                TimeSpan Alled = new TimeSpan();
    //                var allreport = FarhangDb.ReportWorks.Where(u => u.UserId == curentuser.Id && u.CalenderId == Calenderid).ToList();
    //                foreach (var item in allreport)
    //                {
    //                    Alled += item.EndTime.Value - item.StartTime.Value;
    //                }

    //                if (allreport.Count > 0)
    //                {
    //                    var lasttimer = allreport.Last();
    //                    if (StartTimeDate.Hours > lasttimer.EndTime.Value.Hours ||
    //                            (StartTimeDate.Hours == lasttimer.EndTime.Value.Hours && StartTimeDate.Minutes > lasttimer.EndTime.Value.Minutes))
    //                    {
    //                        istiming = true;
    //                    }
    //                }

    //                AllWorkDate = new TimeSpan(AllWorkDate.Hours, AllWorkDate.Minutes + 1, 0);

    //                if (StartTimeDate.Hours == startappworking.StartTime.Hour && StartTimeDate.Minutes == startappworking.StartTime.Minute
    //            && EndTimeDate.Hours == appworking.EndTime.Value.Hour && EndTimeDate.Minutes == appworking.EndTime.Value.Minute)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "شما مجاز به ارسال تنها یک گزارش برای کل روز نیستید.";
    //                }
    //                else if ((StartTimeDate.Hours > startappworking.StartTime.Hour ||
    //                    (StartTimeDate.Hours == startappworking.StartTime.Hour && StartTimeDate.Minutes > startappworking.StartTime.Minute))
    //                    && (allreport.Count == 0 || istiming))
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "لطفا ساعت گزارش خود را بر اساس ساعت شروع مرتب درج کنید.";
    //                }
    //                else if (AllWorkDate - Alled < (EndTimeDate - StartTimeDate))
    //                {
    //                    TimeSpan a = AllWorkDate - Alled;
    //                    TimeSpan b = EndTimeDate - StartTimeDate;
    //                    TimeSpan c = b - a;
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "ساعات انتخاب شده بیش از حد مجاز است.";
    //                }
    //                else
    //                {
    //                    if ((EndTimeDate - StartTimeDate) > AllWorkDate)
    //                    {
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = "محدوده انتخابی خارج از ساعت کاری است.";
    //                    }
    //                    else
    //                    {
    //                        if (startappworking.StartTime.TimeOfDay.Hours > StartTimeDate.Hours && startappworking.StartTime.TimeOfDay.Minutes > StartTimeDate.Minutes)
    //                        {
    //                            resultmodel.Result = false;
    //                            resultmodel.Message = "محدوده انتخابی خارج از ساعت کاری است.";
    //                        }
    //                        else if (appworking.EndTime.Value.TimeOfDay.Hours < EndTimeDate.Hours && appworking.EndTime.Value.TimeOfDay.Minutes < EndTimeDate.Minutes)
    //                        {
    //                            resultmodel.Result = false;
    //                            resultmodel.Message = "محدوده انتخابی خارج از ساعت کاری است.";
    //                        }
    //                        else
    //                        {
    //                            ReportWork reportw = new ReportWork();
    //                            reportw.GuidId = Guid.NewGuid();
    //                            reportw.UserId = curentuser.Id;
    //                            reportw.StartTime = StartTimeDate;
    //                            reportw.EndTime = EndTimeDate;

    //                            DateTime thistime = DateTime.Now.Date;

    //                            if (FarhangDb.Calenders.Any(u => u.Today == thistime))
    //                            {
    //                                reportw.CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == thistime).Id;
    //                            }
    //                            else
    //                            {
    //                                Calender cal = new Calender();
    //                                cal.Today = thistime;

    //                                FarhangDb.Calenders.Add(cal);
    //                                FarhangDb.SaveChanges();

    //                                reportw.CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == thistime).Id;
    //                            }
    //                            reportw.ReportTitle = jsonmodel.ReportTitle;
    //                            reportw.ReportDescription = jsonmodel.ReportDescription;

    //                            if (jsonmodel.RestTime)
    //                                reportw.RestTime = true;
    //                            else
    //                                reportw.RestTime = false;

    //                            FarhangDb.ReportWorks.Add(reportw);

    //                            TimeSpan AlledEnd = new TimeSpan();
    //                            var allreportEnd = FarhangDb.ReportWorks.Where(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId).ToList();
    //                            foreach (var item in allreportEnd)
    //                            {
    //                                AlledEnd += (item.EndTime.Value - item.StartTime.Value);
    //                            }

    //                            TimeSpan mini = AllWorkDate - AlledEnd;

    //                            TimeSpan startFilter = FarhangDb.ReportWorks.Where(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.StartTime.HasValue && u.EndTime.HasValue).OrderByDescending(u => u.Id).First().EndTime.Value;
    //                            TimeSpan endFilter = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.IsStop == true).EndTime.Value.TimeOfDay;

    //                            string starttimefilter = "";
    //                            TimeSpan predaf = new TimeSpan(12, 0, 0);
    //                            if (startFilter > predaf)
    //                                starttimefilter = startFilter.Hours + ":" + startFilter.Minutes + " ب.ظ";
    //                            else
    //                                starttimefilter = startFilter.Hours + ":" + startFilter.Minutes + " ق.ظ";

    //                            string endtimefilter = "";
    //                            if (endFilter > predaf)
    //                                endtimefilter = endFilter.Hours + ":" + endFilter.Minutes + " ب.ظ";
    //                            else
    //                                endtimefilter = endFilter.Hours + ":" + endFilter.Minutes + " ق.ظ";

    //                            int Mintime = (startFilter.Hours * 60) + startFilter.Minutes;
    //                            int Maxtime = (endFilter.Hours * 60) + endFilter.Minutes;

    //                            TimeSpan StartUser = new TimeSpan();
    //                            TimeSpan EndUser = new TimeSpan();
    //                            if (FarhangDb.AppWorkings.Any(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.IsStop == false))
    //                            {
    //                                StartUser = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.IsStop == false).StartTime.TimeOfDay;
    //                            }
    //                            else
    //                            {
    //                                StartUser = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId).StartTime.TimeOfDay;
    //                            }

    //                            EndUser = FarhangDb.AppWorkings.FirstOrDefault(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.IsStop == true).EndTime.Value.TimeOfDay;
    //                            TimeSpan startReportFilter = FarhangDb.ReportWorks.Where(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.StartTime.HasValue && u.EndTime.HasValue).OrderBy(u => u.Id).First().StartTime.Value;
    //                            TimeSpan endReportFilter = FarhangDb.ReportWorks.Where(u => u.UserId == curentuser.Id && u.CalenderId == reportw.CalenderId && u.StartTime.HasValue && u.EndTime.HasValue).OrderByDescending(u => u.Id).First().EndTime.Value;

    //                            try
    //                            {
    //                                FarhangDb.SaveChanges();

    //                                long reportworkid = FarhangDb.ReportWorks.FirstOrDefault(u => u.GuidId == reportw.GuidId).Id;

    //                                if (jsonmodel.FilesId != null)
    //                                {
    //                                    List<ReportWorkFile> workfiles = new List<ReportWorkFile>();
    //                                    foreach (int item in jsonmodel.FilesId)
    //                                    {
    //                                        ReportWorkFile subworkfiles = new ReportWorkFile();
    //                                        subworkfiles.ReportWorkId = reportworkid;
    //                                        subworkfiles.FileId = item;

    //                                        workfiles.Add(subworkfiles);
    //                                    }

    //                                    FarhangDb.ReportWorkFiles.AddRange(workfiles);
    //                                    FarhangDb.SaveChanges();
    //                                }
    //                                LogSave(curentuser, "ثبت گزارش کار", "کاربر " + curentuser.FullName + " یک گزارش کار ثبت کرد.", DateTime.Now);

    //                                InsertReportWorkByAllTimeResult result = new InsertReportWorkByAllTimeResult();
    //                                if (Mintime == Maxtime)
    //                                {
    //                                    result.AllTime = mini.ToString();
    //                                    result.Starttime = starttimefilter;
    //                                    result.Endtime = endtimefilter;

    //                                    result.StartTimeHour = StartUser.Hours;
    //                                    result.StartTimeMin = StartUser.Minutes;
    //                                    result.EndTimeHour = endFilter.Hours;
    //                                    result.EndTimeMin = endFilter.Minutes;

    //                                    result.MinHourGreen = startReportFilter.Hours;
    //                                    result.MinMinGreen = startReportFilter.Minutes;
    //                                    result.MaxHourGreen = endReportFilter.Hours;
    //                                    result.MaxMinGreen = endReportFilter.Minutes;

    //                                    result.MinHourRed = 0;
    //                                    result.MinMinRed = 0;
    //                                    result.MaxHourRed = 0;
    //                                    result.MaxMinRed = 0;
    //                                    result.Status = "End";
    //                                }
    //                                else
    //                                {
    //                                    result.AllTime = mini.ToString();
    //                                    result.Starttime = starttimefilter;
    //                                    result.Endtime = endtimefilter;

    //                                    result.StartTimeHour = StartUser.Hours;
    //                                    result.StartTimeMin = StartUser.Minutes;
    //                                    result.EndTimeHour = endFilter.Hours;
    //                                    result.EndTimeMin = endFilter.Minutes;

    //                                    result.MinHourGreen = startReportFilter.Hours;
    //                                    result.MinMinGreen = startReportFilter.Minutes;
    //                                    result.MaxHourGreen = endReportFilter.Hours;
    //                                    result.MaxMinGreen = endReportFilter.Minutes;

    //                                    result.MinHourRed = endReportFilter.Hours;
    //                                    result.MinMinRed = (endReportFilter.Minutes + 1);
    //                                    result.MaxHourRed = endFilter.Hours;
    //                                    result.MaxMinRed = endFilter.Minutes;
    //                                    result.Status = "Start";
    //                                }

    //                                resultmodel.Result = true;
    //                                resultmodel.Model = result;
    //                                resultmodel.Message = "کاربر " + curentuser.FullName + " یک گزارش کار ثبت کرد.";
    //                            }
    //                            catch (Exception ex)
    //                            {
    //                                resultmodel.Result = false;
    //                                resultmodel.Message = ex.Message;
    //                            }
    //                        }
    //                    }
    //                }

    //                #endregion
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region Event
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult InsertEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        InsertEventJsonModel jsonmodel = new InsertEventJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<InsertEventJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                if (FarhangDb.AppUserEvents.Any(u => u.Title == jsonmodel.Title && u.Description == jsonmodel.Description && u.Periority == jsonmodel.Periority && u.Action == 1))
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "کاربر گرامی، پیش از این یک فعالیت با این عنوان و توضیحات در سامانه ثبت شده است";
    //                }
    //                else
    //                {
    //                    if (FarhangDb.AppUsers.Any(u => u.Id == curentuser.Id))
    //                    {
    //                        long CalenderId = 0;
    //                        DateTime dt = jsonmodel.Calender.ToDateTime();

    //                        if (FarhangDb.Calenders.Any(u => u.Today == dt.Date))
    //                            CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == dt.Date).Id;
    //                        else
    //                        {
    //                            Calender cal = new Calender();
    //                            cal.Today = dt.Date;

    //                            FarhangDb.Calenders.Add(cal);
    //                            FarhangDb.SaveChanges();

    //                            CalenderId = FarhangDb.Calenders.FirstOrDefault(u => u.Today == dt.Date).Id;
    //                        }

    //                        AppUserEvent userevent = new AppUserEvent();

    //                        if (jsonmodel.UserId > 0)
    //                            userevent.UserId = jsonmodel.UserId;
    //                        else
    //                            userevent.UserId = jsonmodel.CreatedUserId;

    //                        userevent.CalenderId = CalenderId;
    //                        if (userevent.Periority == 1)
    //                            userevent.Typed = 1;
    //                        else
    //                            userevent.Typed = 2;

    //                        userevent.Title = jsonmodel.Title;
    //                        userevent.Description = jsonmodel.Description;
    //                        userevent.Action = 1;
    //                        userevent.Periority = jsonmodel.Periority;

    //                        userevent.CreatedUserId = jsonmodel.CreatedUserId;

    //                        if (jsonmodel.CreatedUserId == 3)
    //                            userevent.AdminEvent = true;
    //                        else
    //                            userevent.AdminEvent = false;

    //                        FarhangDb.AppUserEvents.Add(userevent);

    //                        try
    //                        {
    //                            FarhangDb.SaveChanges();

    //                            if (jsonmodel.CreatedUserId == 3)
    //                            {
    //                                AppUser eruser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);
    //                                AppUserEvent eventuser = FarhangDb.AppUserEvents.FirstOrDefault(u => u.UserId == userevent.UserId
    //                                    && u.CalenderId == userevent.CalenderId && u.Title == userevent.Title
    //                                    && u.Description == userevent.Description);

    //                                string message = "کاربر " + eruser.FullName + " مدیریت فعالیت " + userevent.Description + " را به شما ارجاع داده است." + "سیستم مدیریت فرهنگ" + "  http://familyfarhang.com  ";
    //                                string usermobile = eruser.Mobile;
    //                                SmsSender.SendSms(message, usermobile, eruser.Id);

    //                                LogSave(curentuser, "ثبت فعالیت", "کاربر " + curentuser.FullName + " یک فعالیت به " + eruser.FullName + " ارجاع کرد.", DateTime.Now);

    //                                resultmodel.Result = true;
    //                                resultmodel.Message = "کاربر " + curentuser.FullName + " یک فعالیت به " + eruser.FullName + " ارجاع کرد.";
    //                            }
    //                            else
    //                            {
    //                                AppUserEvent eventuser = FarhangDb.AppUserEvents.FirstOrDefault(u => u.UserId == userevent.UserId
    //                                    && u.CalenderId == userevent.CalenderId && u.Title == userevent.Title
    //                                    && u.Description == userevent.Description);

    //                                string message = "فعالیت " + userevent.Description + " ثبت شد. " + "سیستم مدیریت فرهنگ" + "  http://familyfarhang.com  ";
    //                                string usermobile = curentuser.Mobile;
    //                                SmsSender.SendSms(message, usermobile, curentuser.Id);

    //                                LogSave(curentuser, "ثبت فعالیت", "کاربر " + curentuser.FullName + " یک فعالیت ثبت کرد.", DateTime.Now);

    //                                resultmodel.Result = true;
    //                                resultmodel.Message = "کاربر " + curentuser.FullName + " یک فعالیت ثبت کرد.";
    //                            }
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            resultmodel.Result = false;
    //                            resultmodel.Message = "متاسفانه خطایی در ثبت رویداد مورد نظر پیش امده است. لطفا بعدا امتحان کنید.";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = "کاربر مورد نظر یافت نشد.";
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetAllUserEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        GetAllUserEventJsonModel jsonmodel = new GetAllUserEventJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllUserEventJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var userevent = FarhangDb.AppUserEvents.Where(u => u.UserId == jsonmodel.UserId).ToList();

    //                resultmodel.Result = true;
    //                resultmodel.Model = userevent;
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetUserEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        GetUserEventJsonModel jsonmodel = new GetUserEventJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserEventJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                if (FarhangDb.AppUserEvents.Any(u => u.Id == jsonmodel.EventId))
    //                {
    //                    AppUserEvent userevent = FarhangDb.AppUserEvents.FirstOrDefault(u => u.Id == jsonmodel.EventId);
    //                    Calender cal = FarhangDb.Calenders.Find(userevent.CalenderId);

    //                    GetUserEventResult result = new GetUserEventResult();
    //                    result.Id = userevent.Id;
    //                    result.Title = userevent.Title;
    //                    result.Description = userevent.Description;
    //                    result.Typed = userevent.Typed;
    //                    result.Periority = userevent.Periority;
    //                    result.Action = userevent.Action;
    //                    result.Calender = cal.Today.ToPersianDate().ToString();
    //                    result.ReportWork = userevent.ReportWork;

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = result;
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "رویداد یافت نشد.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region EventAction
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult ConfirmEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserEventActionJsonModel jsonmodel = new UserEventActionJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserEventActionJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                if (FarhangDb.AppUserEvents.Any(u => u.Id == jsonmodel.EventId))
    //                {
    //                    var evented = FarhangDb.AppUserEvents.FirstOrDefault(u => u.Id == jsonmodel.EventId);

    //                    evented.Action = 3;

    //                    try
    //                    {
    //                        FarhangDb.SaveChanges();

    //                        LogSave(curentuser, "تایید فعالیت", "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را تایید کرد.", DateTime.Now);

    //                        resultmodel.Result = true;
    //                        resultmodel.Message = "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را تایید کرد.";
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = ex.Message;
    //                    }
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "رویداد یافت نشد.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult EndEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserEventActionJsonModel jsonmodel = new UserEventActionJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserEventActionJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                if (FarhangDb.AppUserEvents.Any(u => u.Id == jsonmodel.EventId))
    //                {
    //                    var evented = FarhangDb.AppUserEvents.FirstOrDefault(u => u.Id == jsonmodel.EventId);

    //                    evented.Action = 2;

    //                    try
    //                    {
    //                        FarhangDb.SaveChanges();

    //                        LogSave(curentuser, "اتمام فعالیت", "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را اتمام کرد.", DateTime.Now);

    //                        resultmodel.Result = true;
    //                        resultmodel.Message = "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را اتمام کرد.";
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = ex.Message;
    //                    }
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "رویداد یافت نشد.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult StartEvent()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserEventActionJsonModel jsonmodel = new UserEventActionJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserEventActionJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                var curentuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == jsonmodel.UserId);

    //                var evented = FarhangDb.AppUserEvents.FirstOrDefault(u => u.Id == jsonmodel.EventId);
    //                var notuser = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == evented.UserId);

    //                evented.Action = 4;

    //                string message = "مدیریت محترم، کاربر " + notuser.FullName + " فعالیت " + evented.Description + " را شروع کرد." + "سیستم مدیریت فرهنگ" + "  http://familyfarhang.com  ";

    //                long adminuserid = FarhangDb.AppUserInRoles.FirstOrDefault(u => u.RoleId == 1).UserId;
    //                var user = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == adminuserid);
    //                string usermobile = user.Mobile;
    //                SmsSender.SendSms(message, usermobile, user.Id);

    //                try
    //                {
    //                    FarhangDb.SaveChanges();

    //                    LogSave(curentuser, "شروع فعالیت", "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را شروع کرد.", DateTime.Now);

    //                    resultmodel.Result = true;
    //                    resultmodel.Message = "کاربر " + curentuser.FullName + " یک فعالیت با نام " + evented.Title + " را شروع کرد.";
    //                }
    //                catch (Exception ex)
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = ex.Message;
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion

    //    #region Salary
    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetUserSalary()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserSalaryJsonModel jsonmodel = new UserSalaryJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserSalaryJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                if(FarhangDb.AppUsers.Any(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId))
    //                {
    //                    var user = FarhangDb.AppUsers.FirstOrDefault(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId);

    //                    AppSalaryCaching appsalarycaching = new AppSalaryCaching();
    //                    AppLevelCaching applevelcaching = new AppLevelCaching();
    //                    var usersalary = appsalarycaching.GetSingleUserSalary(jsonmodel.CompanyId, user.Id);
    //                    var userlevel = applevelcaching.GetSingleUserLevel(user.Id, usersalary.AppLevelId);

    //                    UserSalary finalisersalary = new UserSalary();
    //                    finalisersalary.AppLevel = userlevel.LevelName;
    //                    finalisersalary.AppUser = user.FullName;
    //                    finalisersalary.SalaryBase = usersalary.SalaryBase.GetDisplayName();
    //                    finalisersalary.Salary = usersalary.Salary;
    //                    finalisersalary.WorkingHoursDay = usersalary.WorkingHoursDay;

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = finalisersalary;
    //                    resultmodel.Message = "میزان دستمزد کاربر " + user.FullName + " با موفقیت دریافت شد.";
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "کاربر با این کد ملی در سیستم ثبت نشده است.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetUserWallet()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserWalletJsonModel jsonmodel = new UserWalletJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserWalletJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                if (FarhangDb.AppUsers.Any(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId))
    //                {
    //                    var user = FarhangDb.AppUsers.FirstOrDefault(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId);

    //                    WalletCaching walletcaching = new WalletCaching();
    //                    var userwallet = walletcaching.GetUserWallet(user.Id, jsonmodel.CompanyId);

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = userwallet.Inventory;
    //                    resultmodel.Message = "میزان کیف پول کاربر " + user.FullName + " با موفقیت دریافت شد.";
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "کاربر با این کد ملی در سیستم ثبت نشده است.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult GetUserRequestCheckout()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        UserRequestCheckoutJsonModel jsonmodel = new UserRequestCheckoutJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRequestCheckoutJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                if (FarhangDb.AppUsers.Any(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId))
    //                {
    //                    var user = FarhangDb.AppUsers.FirstOrDefault(u => u.NationalId == jsonmodel.UserNationalId && u.CompanyId == jsonmodel.CompanyId);

    //                    var requestcheckout = FarhangDb.Checkouts.Where(u => u.AppUserId == user.Id && u.Status != Enumeration.GeneralEnums.CheckoutStatus.Paid 
    //                    && u.Status != Enumeration.GeneralEnums.CheckoutStatus.Failed);

    //                    List<RequestCheckout> finalrequestcheckout = new List<RequestCheckout>();
    //                    foreach(var item in requestcheckout)
    //                    {
    //                        RequestCheckout subrequestcheckout = new RequestCheckout();
    //                        subrequestcheckout.CheckoutId = item.Id;
    //                        subrequestcheckout.AppUser = user.FullName;
    //                        subrequestcheckout.Amount = item.Amount;
    //                        subrequestcheckout.CreateDate = item.CreateDate.ToPersianDate();
    //                        subrequestcheckout.Status = item.Status.GetDisplayName();

    //                        finalrequestcheckout.Add(subrequestcheckout);
    //                    }

    //                    resultmodel.Result = true;
    //                    resultmodel.Model = finalrequestcheckout;
    //                    resultmodel.Message = "درخواست های باز تسویه حساب کاربر " + user.FullName + " با موفقیت دریافت شد.";
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "کاربر با این کد ملی در سیستم ثبت نشده است.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IHttpActionResult EditUserCheckout()
    //    {
    //        ResultModel resultmodel = new ResultModel();

    //        var re = Request;
    //        var header = re.Headers;
    //        var content = re.Content.ReadAsStringAsync().Result;

    //        EditUserCheckoutJsonModel jsonmodel = new EditUserCheckoutJsonModel();
    //        jsonmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<EditUserCheckoutJsonModel>(content);

    //        if (header.Contains("Token"))
    //        {
    //            string token = header.GetValues("Token").First();

    //            if (token == "42345")
    //            {
    //                if (FarhangDb.Checkouts.Any(u => u.Id == jsonmodel.CheckoutId && u.CompanyId == jsonmodel.CompanyId))
    //                {
    //                    var checkout = FarhangDb.Checkouts.FirstOrDefault(u => u.Id == jsonmodel.CheckoutId && u.CompanyId == jsonmodel.CompanyId);
    //                    var user = FarhangDb.AppUsers.FirstOrDefault(u => u.Id == checkout.AppUserId);

    //                    if(jsonmodel.Status == 2)
    //                    {
    //                        checkout.Status = Enumeration.GeneralEnums.CheckoutStatus.Pending;
    //                        checkout.Description = jsonmodel.Description;
    //                    }
    //                    else if(jsonmodel.Status == 3)
    //                    {
    //                        checkout.Status = Enumeration.GeneralEnums.CheckoutStatus.Failed;
    //                        checkout.Description = jsonmodel.Description;
    //                    }
    //                    else if (jsonmodel.Status == 4)
    //                    {
    //                        checkout.Status = Enumeration.GeneralEnums.CheckoutStatus.Confirm;
    //                        checkout.Description = jsonmodel.Description;
    //                    }
    //                    else if (jsonmodel.Status == 5)
    //                    {
    //                        checkout.Status = Enumeration.GeneralEnums.CheckoutStatus.Paid;
    //                        checkout.Description = jsonmodel.Description;

    //                        WalletCaching walletcaching = new WalletCaching();

    //                        var userwallet = FarhangDb.Wallets.FirstOrDefault(u => u.AppUserId == user.Id);
    //                        userwallet.Inventory -= checkout.Amount;

    //                        walletcaching.ChangeUserWallet(userwallet, jsonmodel.CompanyId);

    //                        LogSave(user, "تسویه حساب", "درخواست تسویه حساب کاربر " + user.FullName + " انجام شد.", DateTime.Now);
    //                    }
    //                    try
    //                    {
    //                        FarhangDb.SaveChanges();


    //                        resultmodel.Result = true;
    //                        resultmodel.Message = "درخواست تسویه حساب کاربر " + user.FullName + " انجام شد.";
    //                    }
    //                    catch (Exception)
    //                    {
    //                        resultmodel.Result = false;
    //                        resultmodel.Message = "متاسفانه درخواست تسویه حساب کاربر " + user.FullName + " انجام نگرفت. با پشتیبان تماس بگیرید.";
    //                    }
    //                }
    //                else
    //                {
    //                    resultmodel.Result = false;
    //                    resultmodel.Message = "کاربر با این کد ملی در سیستم ثبت نشده است.";
    //                }
    //            }
    //            else
    //            {
    //                resultmodel.Result = false;
    //                resultmodel.Message = "توکن اشتباه است";
    //            }
    //        }
    //        else
    //        {
    //            resultmodel.Result = false;
    //            resultmodel.Message = "لطفا توکن را ارسال کنید";
    //        }

    //        return Json(resultmodel);
    //    }
    //    #endregion
    //}
}