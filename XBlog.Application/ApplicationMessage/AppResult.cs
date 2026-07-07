namespace XBlog.Application.ApplicationMessage;

public class AppResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public AppResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public static AppResult IsFaulted(string txt) => new AppResult(false, txt);
    public static AppResult IsSuccessed(string txt) => new AppResult(true, txt);
}
