using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FLifegame.Common
{
    public static class PropertyChangedEventHandlerExtensions
    {
        public static void Raise(this PropertyChangedEventHandler onPropertyChanged, object sender, [CallerMemberName] string propertyName = "")
        {
            if (onPropertyChanged != null)
                onPropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        }

        public static void Raise<PropertyType>(this PropertyChangedEventHandler onPropertyChanged, object sender, Expression<Func<PropertyType>> propertyExpression)
        { onPropertyChanged.Raise(sender, propertyExpression.GetMemberName()); }

        static string GetMemberName<MemberType>(this Expression<Func<MemberType>> expression)
        { return ((MemberExpression)expression.Body).Member.Name; }
    }
}
