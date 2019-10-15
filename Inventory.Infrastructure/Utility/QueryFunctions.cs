using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Inventory.Persistance.Utility
{
    public static class QueryFunctions
    {
        public static IQueryable<TResult> SelectBuilder<TResult>(IQueryable query)
        {
            var sourceType = query.ElementType;
            var resultType = typeof(TResult);
            var parameter = Expression.Parameter(sourceType, "e");
            var properties = resultType.GetProperties().Select(x => x.Name);
            var bindings = properties.Select(column => Expression.Bind(
                resultType.GetProperty(column), Expression.PropertyOrField(parameter, column)));
            var body = Expression.MemberInit(Expression.New(resultType), bindings);
            var selector = Expression.Lambda(body, parameter);
            return query.Provider.CreateQuery<TResult>(
                Expression.Call(typeof(Queryable), "Select", new Type[] { sourceType, resultType },
                    query.Expression, Expression.Quote(selector)));
        }
        public static Tuple<string, object[]> PredicateBuilder(List<string> query)
        {
            var result = "";
            List<object> values = new List<object>();

            for (int i = 0; i < query.Count; i++)
            {
                if (query[i] == null)
                {
                    return null;
                }
                var conditions = query[i].Split("&&");
                result += "(";
                for (int j = 0; j < conditions.Length; j++)
                {
                    //to not add delimtter befor first splitter or after last
                    if (j == conditions.Length - 1 && j != 0)
                    {
                        result += "&&";
                    }
                    //if * means contain
                    var splitted = Regex.Split(conditions[j], @"(?<=[=><*])").ToList();
                    result += $"{QueryFunctions.ConvertFirstToUpperCase(splitted[0])}=@{j}";
                    values.Add(splitted[2]);
                }
                result += ")";
            }

            return Tuple.Create(result, values.ToArray());
        }
        public static string ConvertFirstToUpperCase(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return char.ToUpper(value[0]) + value.Substring(1);
        }


    }
}
