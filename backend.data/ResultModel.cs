using CRM.data;
using System;
using System.Collections.Generic;


namespace core.data
{
    public class ResultModel
    {
        public enum ResultCode
        {
            CungVu = -10,
            ExportError = -9,
            Exists_SoHieuCAND = -8,
            Exists_Email = -7,
            ImportError = -6,
            InvalidateData = -5,
            NotOK = -4,
            Exists = -3,
            Does_Not_Exists = -2,
            Exception = -1,
            Ok = 0,
            Acc_Does_Not_Exists = 1,
            Acc_Exists = 2,
            Id_Card_Exists = 3,
            Phone_Number_Exists = 4,
            UserName_Exists = 5,
            Acc_Or_Pass_Does_Not_Exists = 6,
            Pass_Wrong = 7,
            Acc_Lock = 8,
            Acc_Lock_By_Admin = 9,
            Pass_Err = 10,
            Exit_TTDD=11,
            PhieuAKhongTonTai = 12,
            DaDangKyHoSo = 13,
            Userid_Created_Error = 14, 
            Userid_Editted_Error = 15,
            RoleId_Error = 16,
            Userid_Editted_Error_TaiKhoanKhongTonTai = 17,
            Userid_Editted_Error_SaiPhienDangNhap = 18,
            ExCeptionLichHen = 19,
            RegistSuccess = 20,
            ExSendMail = 21,
            UserName_NotExists = 22,
            Email_NotExists = 23,
            ForgetPWSuccess = 24,
            DongBoMTP_Error = 25,
            RePassWord = 26,
            SamePassWord = 27,
            NotEnoughLenghtPW = 28,
            Err_Format_Email = 29,
            Err_Format_DDCN = 30,
            Err_Format_Phone = 31,
            Err_Format_UserName = 32,
            Err_PW_Character_Special = 33,
            Sys_NotSet_LoginFinger = 34,
            Err_MaThoiGian = 35,
            Err_TruThoiGianTamGiuTamGiam = 36,
            Err_NoneTitleMsg = 37,
            Err_PhongGiamTren18 = 38,
            Err_PhongGiamDuoi18 = 39,
            Err_SoGiamDaTonTai = 40,
            Not_Activated = 41,
            AlreadyActivated = 42,
            Unauthorized = 43,
        }

        public ResultModel(bool isSuccess, ResultCode code, string message, object id = null, object obj = null)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
            Id = id;
            Object = obj;
        }

        public ResultModel()
        {
        }
         
        public bool IsSuccess { get; set; }
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public object Id { get; set; }
        public object Object { get; set; }
        public static string BuildMessage(ResultCode code, string errorMsg = null)
        {
            string msg = string.Empty;
            switch (code)
            {
                case ResultCode.InvalidateData:
                    msg = "Dữ liệu không hợp lệ";
                    break;
                case ResultCode.NotOK:
                    msg = "Có lỗi trong quá trình cập nhật thông tin";
                    break;
                case ResultCode.Exists:
                    msg = "Thông tin đã tồn tại";
                    break;
                case ResultCode.Exists_SoHieuCAND:
                    msg = "Thông tin số hiệu CAND đã tồn tại";
                    break;
                case ResultCode.Does_Not_Exists:
                    msg = "Dữ liệu không tồn tại";
                    break;
                case ResultCode.Ok:
                    msg = "Thao tác thành công";
                    break;
                case ResultCode.Acc_Does_Not_Exists:
                    msg = "Tài khoản không tồn tại"; 
                    break;
                case ResultCode.Acc_Exists:
                    msg = "Tài khoản đã tồn tại";
                    break;
                case ResultCode.Id_Card_Exists:
                    msg = "Số CMND/CCCD đã tồn tại";
                    break;
                case ResultCode.Phone_Number_Exists:
                    msg = "Số điện thoại đã tồn tại";
                    break;
                case ResultCode.UserName_Exists:
                    msg = "Tên đăng nhập đã tồn tại";
                    break;
                case ResultCode.Acc_Or_Pass_Does_Not_Exists:
                    msg = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    break;
                case ResultCode.Pass_Wrong:
                    msg = "Sai mật khẩu";
                    break;
                case ResultCode.Acc_Lock:
                    msg = "Tài khoản của bạn đang bị khóa";
                    break;
                case ResultCode.Acc_Lock_By_Admin:
                    msg = "Tài khoản của bạn đang bị khóa. Vui lòng liên hệ quản trị để biết thêm thông tin";
                    break;
                case ResultCode.Pass_Err:
                    msg = "Mật khẩu cũ không đúng";
                    break;
                case ResultCode.Exit_TTDD:
                    msg = "Thông tin định danh sửa trùng với đối tượng khác trên hệ thống. Vui lòng kiểm tra lại thông tin";
                    break;
                case ResultCode.PhieuAKhongTonTai:
                    msg = "Thông tin hồ sơ không hợp lệ";
                    break;
                case ResultCode.DaDangKyHoSo:
                    msg = "Thông tin đăng ký rà soát hồ sơ của hộ gia đình đã được đăng ký trước đó trên  hệ thống";
                    break;
                case ResultCode.ImportError:
                    msg = "Có lỗi trong quá trình import dữ liệu. Vui lòng kiểm tra lại thông tin";
                    break;
                case ResultCode.Userid_Created_Error:
                    msg = "Thông tin người tạo không hợp lệ";
                    break;
                case ResultCode.Userid_Editted_Error:
                    msg = "Thông tin người cập nhật không hợp lệ";
                    break;
                case ResultCode.RoleId_Error:
                    msg = "Vai trò không hợp lệ";
                    break;
                case ResultCode.Exists_Email:
                    msg = "Thông tin Email đã được sử dụng cho một tài khoản khác trên hệ thống";
                    break;
                case ResultCode.Userid_Editted_Error_TaiKhoanKhongTonTai:
                    msg = "Tài khoản không tồn tại";
                    break;
                case ResultCode.Userid_Editted_Error_SaiPhienDangNhap:
                    msg = "Sai phiên đăng nhập";
                    break;
                case ResultCode.ExCeptionLichHen:
                    msg = "Thông tin đã được lưu, có lỗi khi gửi email!";
                    break;
                case ResultCode.RegistSuccess:
                    msg = "Đăng tài khoản thành công!";
                    break;
                case ResultCode.ExSendMail:
                    msg = "Không gửi được email kích hoạt, vui lòng kiểm tra lại";
                    break;
                case ResultCode.UserName_NotExists:
                    msg = "Tên đăng nhập không đúng, vui lòng kiểm tra lại";
                    break;
                case ResultCode.Email_NotExists:
                    msg = "Email không tồn tại, vui lòng kiểm tra lại";
                    break;
                case ResultCode.ForgetPWSuccess:
                    msg = "Mật khẩu mới đã được gửi vào Email của bạn!";
                    break;
                case ResultCode.RePassWord:
                    msg = "Mật khẩu cũ sai, xin nhập lại!";
                    break;
                case ResultCode.SamePassWord:
                    msg = "Mật khẩu mới không được trùng với mật khẩu cũ!";
                    break;
                case ResultCode.NotEnoughLenghtPW:
                    msg = "Độ dài mật khẩu phải lớn hơn độ dài cấu hình trong hệ thống";
                    break;
                case ResultCode.Err_Format_Email:
                    msg = "Email không đúng định dạng";
                    break;
                case ResultCode.Err_Format_DDCN:
                    msg = "Căn cước công dân chỉ được nhập 9 hoặc 12 chữ số, vui lòng kiểm tra lại các trường căn cước công dân";
                    break;
                case ResultCode.Err_Format_Phone:
                    msg = "Số điện thoại không đúng định dạng, vui lòng kiểm tra lại";
                    break;
                case ResultCode.Err_Format_UserName:
                    msg = "Tài khoản không được là tiếng việt có dấu và chứa khoảng trắng";
                    break;
                case ResultCode.Err_PW_Character_Special:
                    msg = "Mật khẩu không được chứa ký tự đặc biệt";
                    break;
                case ResultCode.Sys_NotSet_LoginFinger:
                    msg = "Tài khoản chưa cấu hình đăng nhập bằng vân tay";
                    break;
                case ResultCode.Err_MaThoiGian:
                    msg = "Mã thời gian không hợp lệ";
                    break;
                case ResultCode.Err_TruThoiGianTamGiuTamGiam:
                    msg = "Thời gian tạm giam chỉ nằm trong khoảng 0 đến 24 giờ";
                    break;
                case ResultCode.ExportError:
                    msg = "Có lỗi trong quá trình export dữ liệu. Vui lòng kiểm tra lại thông tin";
                    break;
                case ResultCode.Err_NoneTitleMsg:
                    msg = "Cảnh báo";
                    break;
                case ResultCode.Err_PhongGiamTren18:
                    msg = "Cảnh báo : Không được giam người trên vị thành niên vào phòng có người dưới vị thành niên";
                    break;
                case ResultCode.Err_PhongGiamDuoi18:
                    msg = "Cảnh báo : Không được giam người dưới vị thành niên vào phòng có người trên vị thành niên";
                    break; 
                case ResultCode.Err_SoGiamDaTonTai:
                    msg = "Cảnh báo : Số giam đã tồn tại trên hệ thống";
                    break;
                case ResultCode.Not_Activated:
                    msg = "Cảnh báo : Tài khoản chưa được kích hoạt";
                    break;
                case ResultCode.AlreadyActivated:
                    msg = "Cảnh báo : Tài khoản đã được kích hoạt";
                    break;
                case ResultCode.Unauthorized:
                    msg = "Cảnh báo : Bạn phải đăng nhập để thích sản phẩm";
                    break;
                default:
                    msg = "";
                    break;
            }
            return string.IsNullOrEmpty(errorMsg) ? msg : $"{msg} : {errorMsg}";
        }
    }

    public class LoginModel
    {
       // public LoginModel(bool isSuccess, ResultModel.ResultCode code, string message, Guid userId, Users obj = null)
        public LoginModel(bool isSuccess, ResultModel.ResultCode code, string message, Guid userId)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
            UserId = userId;
            //Object = obj;
        }
        /// <summary>
        /// Status of result api (True => Is success response request and object data when server return is effective)
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Is error code return when exception access request
        /// </summary>
        public ResultModel.ResultCode Code { get; set; }

        /// <summary>
        /// Is detail message error return when exception access request
        /// </summary>
        public string Message { get; set; }
        public Guid UserId { get; set; }
        //public Users Object { get; set; }
    }
}
