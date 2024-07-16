using System.ComponentModel;

namespace dotnet60_example.Enums
{
    /// <summary>
    /// 一般使用HttpStatusCode
    /// 沒有的話再自訂義
    /// </summary>
    public enum CustomStatusCode
    {
        [Description("SessionTimeout")]
        SessionTimeout = 440
    }
}
