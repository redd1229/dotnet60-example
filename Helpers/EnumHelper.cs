using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace dotnet60_example.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// 取得Enum的Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T source)
        {
            Type type = source.GetType();
            MemberInfo[] memberInfo = type.GetMember(source.ToString());
            if (memberInfo != null && memberInfo.Any())
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Any())
                {
                    return ((DescriptionAttribute)attrs.ElementAt(0)).Description;
                }
            }
            return source.ToString();
        }

        /// <summary>
        /// Enum下拉選單
        /// key: Enum
        /// value: Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(
                x => new SelectListItem() { Text = GetDescription(x), Value = x.ToString() }).ToList();
        }
    }
}
