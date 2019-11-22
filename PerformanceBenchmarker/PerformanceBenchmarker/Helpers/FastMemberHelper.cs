using FastMember;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PerformanceBenchmarker.Helpers
{
    static class FastMemberHelper
    {
        public static List<T> ToList<T>(this SqlDataReader rd) where T : class, new()
        {
            List<T> list = new List<T>();
            while (rd.Read())
            {
                Type type = typeof(T);
                var accessor = TypeAccessor.Create(type);
                var members = accessor.GetMembers();
                var t = new T();

                for (int i = 0; i < rd.FieldCount; i++)
                {
                    if (!rd.IsDBNull(i))
                    {
                        string fieldName = rd.GetName(i);

                        if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                        {
                            try
                            {
                                accessor[t, fieldName] = rd.GetValue(i);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public static T ConvertToObject<T>(this SqlDataReader dataReader) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (!dataReader.IsDBNull(i))
                {
                    string fieldName = dataReader.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[t, fieldName] = dataReader.GetValue(i);
                    }
                }
            }

            return t;
        }
    }
}