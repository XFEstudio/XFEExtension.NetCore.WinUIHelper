using System.Collections;
using System.Reflection;

namespace MultiPlatformTranslation.Core.Utilities.Helpers;

/// <summary>
/// 对象帮助类
/// </summary>
public static class ObjectHelper
{
    /// <summary>
    /// 判断对象及其子属性是否包含指定关键字
    /// </summary>
    public static bool Search(object obj, string keyword)
    {
        return SearchObject(obj, keyword, []);
    }

    private static bool SearchObject(object? obj, string keyword, HashSet<object> visited)
    {
        if (obj == null || visited.Contains(obj))
            return false;

        visited.Add(obj);

        // string
        if (obj is string str)
        {
            return str.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }

        // 集合
        if (obj is IEnumerable enumerable && obj is not string)
        {
            foreach (var item in enumerable)
            {
                if (SearchObject(item, keyword, visited))
                    return true;
            }
            return false;
        }

        // 基础类型
        if (obj.GetType().IsPrimitive || obj is decimal || obj is DateTime)
        {
            return obj.ToString()?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true;
        }

        // 复杂对象属性
        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            try
            {
                var value = prop.GetValue(obj);
                if (SearchObject(value, keyword, visited))
                    return true;
            }
            catch { /* 忽略无法访问的属性 */ }
        }

        return false;
    }

    /// <summary>
    /// 将 source 对象的值复制到 target 对象中，支持嵌套对象的递归复制
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyValues<T>(this T source, T target)
    {
        if (source == null || target == null) return;

        var type = typeof(T);

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead || !prop.CanWrite) continue;

            var value = prop.GetValue(source);

            if (value == null)
            {
                prop.SetValue(target, null);
            }
            else if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
            {
                // 值类型和字符串直接赋值
                prop.SetValue(target, value);
            }
            else
            {
                // 引用类型（子对象）
                var targetValue = prop.GetValue(target);
                if (targetValue == null)
                {
                    targetValue = Activator.CreateInstance(prop.PropertyType);
                    prop.SetValue(target, targetValue);
                }

                // 递归复制
                var method = typeof(ObjectHelper)
                    .GetMethod(nameof(CopyValues), BindingFlags.Static | BindingFlags.Public)!
                    .MakeGenericMethod(prop.PropertyType);
                method.Invoke(null, [value, targetValue]);
            }
        }

        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            var value = field.GetValue(source);
            if (value == null)
            {
                field.SetValue(target, null);
            }
            else if (field.FieldType.IsValueType || field.FieldType == typeof(string))
            {
                field.SetValue(target, value);
            }
            else
            {
                var targetValue = field.GetValue(target);
                if (targetValue == null)
                {
                    targetValue = Activator.CreateInstance(field.FieldType);
                    field.SetValue(target, targetValue);
                }

                var method = typeof(ObjectHelper)
                    .GetMethod(nameof(CopyValues), BindingFlags.Static | BindingFlags.Public)!
                    .MakeGenericMethod(field.FieldType);
                method.Invoke(null, [value, targetValue]);
            }
        }
    }
}
