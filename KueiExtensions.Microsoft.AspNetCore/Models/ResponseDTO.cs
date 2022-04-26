namespace KueiExtensions.Microsoft.AspNetCore.Models;

public class ResponseDTO
{
    public bool IsValid => IsFormValid && string.IsNullOrWhiteSpace(AlertMessage);
    
    /// <summary>
    /// 表單是否驗証成功
    /// </summary>
    public bool IsFormValid { get; set; }

    public string? Message { get; set; }

    /// <summary>
    /// 顯示 Alert Message
    /// </summary>
    public string? AlertMessage { get; set; }

    /// <summary>
    /// 表單驗証失敗時，顯示其錯誤訊息
    /// </summary>
    public object? Data { get; set; }

    public static ResponseDTO Ok(object? o = null, string messsage = "")
    {
        return new ResponseDTO
               {
                   IsFormValid = true,
                   Message     = messsage,
                   Data        = o
               };
    }

    public static ResponseDTO ShowAlert(string alertMessage = "")
    {
        return new ResponseDTO
               {
                   IsFormValid  = true,
                   AlertMessage = alertMessage,
               };
    }
}
