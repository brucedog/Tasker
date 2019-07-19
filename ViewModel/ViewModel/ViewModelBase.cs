using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Messenger;

namespace MorningReminder.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IMessenger WindowMessenger { get; set; }

        public void NotifyPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            PropertyChangedExtensions.Notify(PropertyChanged, propertySelector);
        }

        private static class PropertyChangedExtensions
        {
            public static void Notify<T>(PropertyChangedEventHandler handler, Expression<Func<T>> propertySelector)
            {
                if (handler == null)
                {
                    return;
                }

                MemberExpression memberExpression = PropertyChangedExtensions.GetMemberExpression<T>(propertySelector);
                if (memberExpression == null)
                {
                    return;
                }

                object value = ((ConstantExpression)memberExpression.Expression).Value;
                handler(value, new PropertyChangedEventArgs(memberExpression.Member.Name));
            }

            public static MemberExpression GetMemberExpression<T>(Expression<Func<T>> propertySelector)
            {
                MemberExpression memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    if (memberExpression.Member.MemberType != MemberTypes.Property)
                    {
                        throw new ArgumentException("PropertySelector must select a property type.");
                    }

                    return memberExpression;
                }

                UnaryExpression unaryExpression = propertySelector.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    MemberExpression memberOperandExpression = unaryExpression.Operand as MemberExpression;
                    if (memberOperandExpression != null)
                    {
                        if (memberOperandExpression.Member.MemberType != MemberTypes.Property)
                        {
                            throw new ArgumentException("PropertySelector must select a property type.");
                        }

                        return memberOperandExpression;
                    }
                }

                return null;
            }
        }
    }
}