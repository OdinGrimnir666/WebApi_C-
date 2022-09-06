public class XmlResult<T> : IResult
{
    public static readonly XmlSerializer _xmlSerealizr = new(typeof(T));
    private readonly T _result;

    public XmlResult(T result) => _result=result;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        using var ms = new MemoryStream();
        _xmlSerealizr.Serialize(ms,_result);
        httpContext.Response.ContentType= "application/xml";
        ms.Position=0;
        return ms.CopyToAsync(httpContext.Response.Body);
    }
}

static class XmlResultExtension
{
    public static IResult Xml<T>(this IResultExtensions _,T result)=>
    new XmlResult<T>(result);
}