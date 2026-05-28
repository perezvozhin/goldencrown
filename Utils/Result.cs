namespace WebApplication3.Utils;
public class Result<T> : Result
{
    public T value { get; set; }

    public static Result<T> Success(T value) => new Result<T> { value = value, isSuccess = true };
    public static  new Result<T> Fail(string errormessage) => new Result<T> { errormessage = errormessage, isSuccess = false };
    
    public static implicit operator Result<T>(T value) => Success(value);
}

public class Result
{   public bool isSuccess { get; set; }
    public string errormessage { get; set; }
    public static Result Success() => new Result { isSuccess = true };
    public static Result Fail(string errormessage) => new Result { errormessage = errormessage, isSuccess = false };
    
    public static implicit operator bool(Result result) => result.isSuccess;
   
}