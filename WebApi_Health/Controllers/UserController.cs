using Common.Helper;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Web.Http;
using Common.Extend;
using Common.Filter;
using Common.Result;
using WebApi_Health.Models.Response;
using WebApi_Health.BLL.Cache;
using DbOpertion.DBoperation;
using System;
using System.Linq;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Models.Food;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Http;
using Common.Enum;
using System.Runtime.CompilerServices;
using System.Text;
using System.Drawing;
using AliyunHelper.SendMail;
using WebApi_Health.BLL.Enum;
using WebApi_Health.Models.Variable;
using WebApi_Health.BLL.Function;
using Aliyun.Acs.Dysmsapi.Model.V20170525;

namespace WebApi_Health.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 用户登入
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetUserInfoResponse> Login(UserLoginRequest request)
        {
            ResultJson<GetUserInfoResponse> result = new ResultJson<GetUserInfoResponse>();
            #region 微信查询
            if (request.TransMode.ToLower() == "wechat")
            {
                if (request.UserId.IsNullOrEmpty())
                {
                    result.HttpCode = 400;
                    result.Message = "当登录方式是微信登入时，请输入UserId";
                }
                else
                {
                    var usermodel = CacheForModelUser.Instance.GetUserInfoByWeChat(request.UserId);
                    if (usermodel == null)
                    {
                        if (request.UserImage.IsNullOrEmpty() || request.UserSex.IsNullOrEmpty())
                        {
                            result.HttpCode = 400;
                            result.Message = "当首次用微信登入时，请输入UserImage与UserSex";
                        }
                        usermodel = new Customer();
                        usermodel.wechat = request.UserId;
                        if (request.UserSex == "男")
                        {
                            usermodel.sex = true;
                        }
                        else if (request.UserSex == "女")
                        {
                            usermodel.sex = false;
                        }
                        if (CustomerOper.Instance.Insert(usermodel))
                        {
                            usermodel = CacheForModelUser.Instance.GetUserInfoByWeChat(request.UserId);
                            result.HttpCode = 200;
                            result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                            result.ListData = new List<GetUserInfoResponse>();
                            CacheForModelUser.Instance.SetUserInfo(usermodel.id);
                            var token = CacheForModelUser.Instance.SetUserToken(usermodel);
                            GetUserInfoResponse user_model = new GetUserInfoResponse(usermodel, token, Request.Headers.Host);
                            result.ListData.Add(user_model);
                        }
                        else
                        {
                            result.HttpCode = 500;
                            result.Message = "数据插入失败";
                        }
                    }
                    else
                    {
                        result.HttpCode = 200;
                        result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                        result.ListData = new List<GetUserInfoResponse>();
                        var token = CacheForModelUser.Instance.SetUserToken(usermodel);
                        GetUserInfoResponse user_model = new GetUserInfoResponse(usermodel, token, Request.Headers.Host);
                        result.ListData.Add(user_model);
                    }
                }
            }
            #endregion

            #region 用户密码查询
            else if (request.TransMode.ToLower() == "password")
            {
                if (request.UserPhone.IsNullOrEmpty() || request.UserPassword.IsNullOrEmpty())
                {
                    result.HttpCode = 400;
                    result.Message = "当登录方式是账户登入时，请输入UserName与UserPassword";
                }
                else
                {
                    var usermodel = CacheForModelUser.Instance.GetUserInfoByPassWord(request.UserPhone, request.UserPassword);
                    if (usermodel == null)
                    {
                        result.HttpCode = 301;
                        result.Message = "用户名或密码不存在";
                    }
                    else
                    {
                        result.HttpCode = 200;
                        result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                        result.ListData = new List<GetUserInfoResponse>();
                        CacheForModelUser.Instance.SetUserInfo(usermodel.id);
                        var token = CacheForModelUser.Instance.SetUserToken(usermodel);
                        GetUserInfoResponse user_model = new GetUserInfoResponse(usermodel, token, Request.Headers.Host);
                        result.ListData.Add(user_model);
                    }
                }
            }
            #endregion

            #region 用户Token查询
            else if (request.TransMode.ToLower() == "token")
            {
                if (request.Token.IsNullOrEmpty())
                {
                    result.HttpCode = 400;
                    result.Message = "当登录方式是Token登入,请输入Token";
                }
                else
                {
                    Token token = CacheForModelUser.Instance.GetUserToken(request.Token);
                    if (token != null)
                    {
                        var usermodel = CacheForModelUser.Instance.GetUserInfo(token.Payload.UserID);

                        if (usermodel == null)
                        {
                            result.HttpCode = 300;
                            result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                        }
                        else
                        {
                            result.HttpCode = 200;
                            result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                            result.ListData = new List<GetUserInfoResponse>();
                            CacheForModelUser.Instance.SetUserInfo(usermodel.id);
                            GetUserInfoResponse user_model = new GetUserInfoResponse(usermodel, Request.Headers.Host);
                            result.ListData.Add(user_model);
                        }
                    }
                    else
                    {
                        result.HttpCode = 300;
                        result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                    }
                }
            }
            #endregion
            else
            {
                result.HttpCode = 300;
                result.Message = "请输入正确的登入方式";
            }
            return result;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson SendMail(SendEmailRequest request)
        {
            ResultJson result = new ResultJson();
            if (CacheForModelUser.Instance.GetUserResetVerificationCode(request.Phone))
            {
                string verification = CacheForModelUser.Instance.SetUserVerificationCode(request.Phone);
                Enum_SendEmailCode SendEmail;
                if (request.SendEmailType.IsNullOrEmpty() || request.SendEmailType.ToLower() == Enum_SearchType.Registration.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.UserRegistrationVerificationCode;
                }
                else if (request.SendEmailType.ToLower() == Enum_SearchType.ResetPassword.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.MessageChangeVerificationCode;
                }
                else if (request.SendEmailType.ToLower() == Enum_SearchType.ChangePhone.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.ModifyPasswordAuthenticationCode;
                }
                else
                {
                    SendEmail = Enum_SendEmailCode.AuthenticationCode;
                }
                SendSmsResponse Email = AliyunHelper.SendMail.SendMail.Instance.SendEmail(request.Phone, verification, SendEmail);
                if (Email.Code.ToUpper() == "OK")
                {
                    result.HttpCode = 200;
                    result.Message = "发送信息成功";
                }
                else
                {
                    result.HttpCode = 500;
                    result.Message = Email.Message;
                }
            }
            else
            {
                result.HttpCode = 500;
                result.Message = "请过段时间重新发送";
            }
            return result;
        }

        /// <summary>
        /// 手机注册
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetUserInfoResponse> MailRegister(MailRegisterRequest request)
        {
            ResultJson<GetUserInfoResponse> result = new ResultJson<GetUserInfoResponse>();
            Customer customer1 = new Customer();
            customer1.phone = request.Phone;
            var users = CustomerOper.Instance.Select(customer1);
            if (users.Count != 0)
            {
                result.HttpCode = 300;
                result.Message = "用户已注册";
            }
            else
            {
                var verification = CacheForModelUser.Instance.GetUserVerificationCode(request.Phone);
                if (verification == null)
                {
                    result.HttpCode = 500;
                    result.Message = "请重新发送验证码";
                }
                else
                {
                    if (request.Code == verification)
                    {
                        Customer customer = new Customer();
                        customer.phone = request.Phone;
                        customer.password = request.PassWord;
                        if (CustomerOper.Instance.Insert(customer))
                        {
                            CacheForModelUser.Instance.ClearUserVerificationCode(request.Phone);
                            var user = CacheForModelUser.Instance.GetUserInfoByPassWord(request.Phone, request.PassWord);
                            GetUserInfoResponse response = new GetUserInfoResponse(user, Request.Headers.Host);
                            result.HttpCode = 200;
                            result.Message = "注册成功";
                            result.ListData.Add(response);
                        }
                        else
                        {
                            result.HttpCode = 300;
                            result.Message = "注册失败";
                        }

                    }
                    else
                    {
                        result.HttpCode = 400;
                        result.Message = "验证码错误";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 用户密码重置
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ResetUserPassword(MailRegisterRequest request)
        {
            ResultJson result = new ResultJson();
            Customer customer = new Customer();
            customer.phone = request.Phone;
            var users = CustomerOper.Instance.Select(customer);
            if (users.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = "并未有此用户";
            }
            else
            {
                var user = users[0];
                var verification = CacheForModelUser.Instance.GetUserVerificationCode(request.Phone);
                if (verification == null)
                {
                    result.HttpCode = 500;
                    result.Message = "请重新发送验证码";
                }
                else
                {
                    if (verification == request.Code)
                    {
                        user.password = request.PassWord;
                        if (DbOpertion.DBoperation.CustomerOper.Instance.Update(user))
                        {
                            CacheForModelUser.Instance.ClearUserVerificationCode(request.Phone);
                            CacheForModelUser.Instance.SetUserInfo(user.id);
                            result.HttpCode = 200;
                            result.Message = "验证成功";
                        }
                        else
                        {
                            result.HttpCode = 500;
                            result.Message = "修改失败";
                        }
                    }
                    else
                    {
                        result.HttpCode = 400;
                        result.Message = "验证码不正常";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 设置用户身体信息
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetUserInfoResponse> SetUserBodyInfo(SetUserBodyInfoRequest request)
        {
            ResultJson<GetUserInfoResponse> result = new ResultJson<GetUserInfoResponse>();
            Customer user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (user == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
                return result;
            }
            user.sex = request.UserSex.ParseBool();
            user.height = request.UserHeight.GetValueOrDefault().ToString();
            user.weight = request.UserWeight.GetValueOrDefault().ToString();
            user.birthday = request.UserBirthTime.GetValueOrDefault();
            user.labourIntensity = request.labInten;
            user.HeadImage = request.HeadImage;
            user.name = request.UserName;
            var resultForUpDate = DbOpertion.DBoperation.CustomerOper.Instance.Update(user);
            if (resultForUpDate)
            {
                CacheForModelUser.Instance.SetUserInfo(request.UserId);
                var userInfo = CacheForModelUser.Instance.GetUserInfo(request.UserId);
                GetUserInfoResponse response = new GetUserInfoResponse(userInfo, Request.Headers.Host);
                result.HttpCode = 200;
                result.Message = "修改成功";
                result.ListData.Add(response);
            }
            else
            {
                result.HttpCode = 500;
                result.Message = "修改失败";
            }
            return result;
        }

        /// <summary>
        /// 获得用户身体信息
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetUserInfoResponse> GetUserBodyInfo(GetInfoByOtherIdRequest request)
        {
            ResultJson<GetUserInfoResponse> result = new ResultJson<GetUserInfoResponse>();
            ////重置用户信息
            //CacheForModelUser.Instance.GetUserInfo(request.id);
            Customer user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (user == null)
            {
                result.HttpCode = 300;
                result.Message = "此用户不存在";
                return result;
            }
            GetUserInfoResponse response = new GetUserInfoResponse(user, Request.Headers.Host);
            result.HttpCode = 200;
            result.ListData.Add(response);
            return result;
        }

        /// <summary>
        /// 筛选用户偏好
        /// </summary>
        [WebApiException]
        [ValidateModel]
        [HttpPost]
        public ResultJson<FoodModel, GetRestaurantListResponse> SelectUserPreference(GetUserPreferenceRequest request)
        {
            ResultJson<FoodModel, GetRestaurantListResponse> result = new ResultJson<FoodModel, GetRestaurantListResponse>();
            var List_Customer = CacheForModelCustomerLike.Instance.GetCustomLike(request.UserId);
            List_Customer = List_Customer.Where(p => p.type == request.Type_Like).ToList();
            var List_Food = CacheForModelFood.Instance.GetFoodList();
            foreach (var item in List_Customer)
            {
                var food = List_Food.Where(p => p.id == item.lid).FirstOrDefault();
                if (food != null)
                {
                    FoodModel model = new FoodModel();
                    model.FoodId = item.lid;
                    model.FoodName = food.name;
                    result.ListData.Add(model);
                }
            }
            if (result.ListData.Count != 0)
            {
                result.HttpCode = 200;
            }
            else
            {
                result.HttpCode = 400;
                result.Message = "没有对应的数据";
            }
            return result;
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        [HttpPost]
        [WebApiException]
        public ResultJsonModel<string> UploadImage()
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            var imageByte = Request.Content.ReadAsByteArrayAsync();
            string Content = HttpContext.Current.Server.MapPath("~/Content").Replace("\\", "/");
            string root = HttpContext.Current.Server.MapPath("~/Content/UserImage").Replace("\\", "/");
            if (Directory.Exists(Content) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Content);
            }
            if (Directory.Exists(root) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(root);
            }

            if (imageByte.Result != null && imageByte.Result.Length > 1024)
            {
                var fileName = RandHelper.Instance.Str(6) + DateTime.Now.Ticks + "." + "jpg";
                var FullFileName = root + "/" + fileName;
                File.WriteAllBytes(FullFileName, imageByte.Result);
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = "Content/UserImage/" + fileName;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UpLoadImageFailMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        [HttpPost]
        [WebApiException]
        public ResultJsonModel<string> UploadUserHeadImage()
        {
            //获取base64编码的图片
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];
            string text = context.Request.Form["imagestr"];

            ResultJsonModel<string> result = new ResultJsonModel<string>();
            if (!text.IsNullOrEmpty())
            {
                //获取文件储存路径
                string root = HttpContext.Current.Server.MapPath("~/Content/UserImage");
                if (Directory.Exists(root) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(root);
                }
                var fileName = RandHelper.Instance.Str(6) + DateTime.Now.Ticks + "." + "jpg";
                string path = context.Request.MapPath("~/"); //获取当前项目所在目录
                string strPath = root + "/" + fileName;
                //获取图片并保存
                ImageUploadHelper.Instance.Base64ToImg(text).Save(strPath);
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = "Content/UserImage/" + fileName;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UpLoadImageFailMessage.Enum_GetString();
            }
            return result;
        }


        /// <summary>
        /// 用户密码修改
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ModifyUserPassword(ModifyUserPasswordRequest request)
        {
            ResultJson result = new ResultJson();
            var usermodel = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (usermodel == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNameOrPasswordNotRightMessage.Enum_GetString();
            }
            else
            {
                if (usermodel.password.ToLower() == request.OldPassword.ToLower())
                {
                    usermodel.password = request.NewPassword;
                    CustomerOper.Instance.Update(usermodel);
                    CacheForModelUser.Instance.SetUserInfo(request.UserId);
                    result.HttpCode = 200;
                    result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.PasswordInvalidMessage.Enum_GetString();
                }
            }
            return result;
        }

        /// <summary>
        /// 用户积分信息
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<User_Current> UserScoreInfo(UserIDRequest request)
        {
            ResultJsonModel<User_Current> result = new ResultJsonModel<User_Current>();
            var usermodel = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            var user_Current = StringHandle.Instance.CalculateScore(usermodel.UserScore.GetValueOrDefault());
            if (user_Current == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = user_Current;
            }
            return result;
        }

        /// <summary>
        /// 验证码是否正确
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson VerificationCode(VerificationCodeRequest request)
        {
            ResultJson result = new ResultJson();
            var usermodel = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (usermodel == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNameOrPasswordNotRightMessage.Enum_GetString();
            }
            else
            {
                if (usermodel.phone.ToLower() == request.Phone.ToLower())
                {
                    var verification = CacheForModelUser.Instance.GetUserVerificationCode(request.Phone);
                    if (verification == null)
                    {
                        result.HttpCode = 500;
                        result.Message = "请重新发送验证码";
                    }
                    else
                    {
                        if (request.VerificaCode == verification)
                        {
                            result.HttpCode = 200;
                            result.Message = "验证成功";
                            CacheForModelUser.Instance.ClearUserVerificationCode(request.Phone);
                        }
                        else
                        {
                            result.HttpCode = 400;
                            result.Message = "验证码错误";
                        }
                    }
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.PasswordInvalidMessage.Enum_GetString();
                }
            }
            return result;
        }

        /// <summary>
        /// 更改用户手机号码
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ModifyUserPhone(VerificationCodeRequest request)
        {
            ResultJson result = new ResultJson();
            var users = CustomerOper.Instance.Select(new Customer { phone = request.Phone });
            if (users.Count != 0)
            {
                result.HttpCode = 300;
                result.Message = "该手机号码已被注册";
                return result;
            }
            var usermodel = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (usermodel == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNameOrPasswordNotRightMessage.Enum_GetString();
            }
            else
            {
                var verification = CacheForModelUser.Instance.GetUserVerificationCode(request.Phone);
                if (verification == null)
                {
                    result.HttpCode = 500;
                    result.Message = "请重新发送验证码";
                }
                else
                {
                    if (request.VerificaCode == verification)
                    {
                        usermodel.phone = request.Phone;
                        if (CustomerOper.Instance.Update(usermodel))
                        {
                            CacheForModelUser.Instance.ClearUserVerificationCode(request.Phone);
                            var user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
                            result.HttpCode = 200;
                            result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                        }
                        else
                        {
                            result.HttpCode = 300;
                            result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
                        }

                    }
                    else
                    {
                        result.HttpCode = 400;
                        result.Message = "验证码错误";
                    }
                }
            }
            return result;
        }
    }
}