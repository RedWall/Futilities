using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Futilities.ObjectExtensions
{
    public static class ObjectExtensions
    {
        public static void SetProperty<T>(this T obj, object value, Expression<Func<T, string>> selector)
        {
            object target = obj;

            string[] bits = selector.Body.ToString().Split('.');

            //Get the object that is the child property if needed
            for (int i = 0; i < bits.Length - 1; i++)
            {
                var member = target.GetType().GetMember(bits[i]);

                if (member == null || member.Length == 0)
                    continue;

                if (member[0].MemberType == MemberTypes.Property)
                    target = ((PropertyInfo)member[0]).GetValue(target, null);
                else
                    if (member[0].MemberType == MemberTypes.Field)
                    target = ((FieldInfo)member[0]).GetValue(target);
            }

            var memberToSet = target.GetType().GetMember(bits.Last());

            if (memberToSet == null || memberToSet.Length == 0)
                return;

            if (memberToSet[0].MemberType == MemberTypes.Property)
                ((PropertyInfo)memberToSet[0]).SetValue(target, value);
            else
            if (memberToSet[0].MemberType == MemberTypes.Field)
                ((FieldInfo)memberToSet[0]).SetValue(target, value);

        }

        public static V GetValue<T, V>(this T obj, Expression<Func<T, V>> selector)
        {

            V value = default;

            object target = obj;

            string[] bits = selector.Body.ToString().Split('.');

            //Get the object that is the child property if needed
            for (int i = 0; i < bits.Length - 1; i++)
            {
                var member = target.GetType().GetMember(bits[i]);

                if (member == null || member.Length == 0)
                    continue;

                if (member[0].MemberType == MemberTypes.Property)
                    target = ((PropertyInfo)member[0]).GetValue(target, null);
                else
                    if (member[0].MemberType == MemberTypes.Field)
                    target = ((FieldInfo)member[0]).GetValue(target);
            }

            var memberToGet = target.GetType().GetMember(bits.Last());

            if (memberToGet == null || memberToGet.Length == 0)
                return default;

            if (memberToGet[0].MemberType == MemberTypes.Property)
                return (V)((PropertyInfo)memberToGet[0]).GetValue(target);
            else if (memberToGet[0].MemberType == MemberTypes.Field)
                return (V)((FieldInfo)memberToGet[0]).GetValue(target);

            return value;

        }
    }
}
