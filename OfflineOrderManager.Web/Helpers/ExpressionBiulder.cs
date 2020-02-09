using System.Linq.Expressions;
using System;
using System.Reflection;

namespace OfflineOrderManager.Web.Helpers
{
    public static class ExpressionBuilder
    {
        public static Expression BuildDefault() =>
            Expression.Constant(true);

        public static ParameterExpression CreateParameter<TParam>() =>
            Expression.Parameter(typeof(TParam), "x");

        public static Expression AndAlso(Expression expression, Expression binaryExpression) =>
            Expression.AndAlso(expression, binaryExpression);

        public static TLambda CreateLambda<TLambda>(Expression expression, ParameterExpression parameter) =>
            Expression.Lambda<TLambda>(expression, new[] { parameter })
            .Compile();

        public static Expression Equal(ParameterExpression parameter, string propertyName, object value)
        {
            var member = Expression.Property(parameter, propertyName);
            
            var constant = Expression.Constant(value);

            return Expression.Equal(member, constant);
        }

        public static Expression ToLowerEqual(ParameterExpression parameter, string propertyName, object value)
        {
            var property = Expression.Property(parameter, propertyName);
            
            var constant = Expression.Constant(value);

            var toLowerMethod = GetMethod(property, "ToLower", Type.EmptyTypes);

            var containsMethod = GetMethod(property, "Contains", new[] { typeof(string) });

            var left = Expression.Call(property, toLowerMethod);

            var right = Expression.Call(constant, toLowerMethod);

            var isNotNullExpression = Expression.NotEqual(property, Expression.Constant(null));

            var containsExpression = Expression.Call(left, containsMethod, right);

            return Expression.AndAlso(isNotNullExpression, containsExpression);
        }

        public static Expression GreaterThanOrEqual(ParameterExpression parameter, string propertyName, object value)
        {
            var member = Expression.Property(parameter, propertyName);

            var constant = Expression.Constant(value);

            return Expression.GreaterThanOrEqual(member, constant);
        }

        public static Expression LessThanOrEqual(ParameterExpression parameter, string propertyName, object value)
        {
            var member = Expression.Property(parameter, propertyName);

            var constant = Expression.Constant(value);

            return Expression.LessThanOrEqual(member, constant);
        }

        private static MethodInfo GetMethod(MemberExpression property, string methodName, Type[] types) =>
            property.Type.GetMethod(methodName, types);
    }
}
