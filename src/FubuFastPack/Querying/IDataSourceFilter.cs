using System;
using System.Linq.Expressions;

namespace FubuFastPack.Querying
{
    public interface IDataSourceFilter<T>
    {
        void WhereEqual(Expression<Func<T, object>> property, object value);
        void WhereNotEqual(Expression<Func<T, object>> property, object value);
        void Or(Action<IOrOptions<T>> left, Action<IOrOptions<T>> right);
        void Or(params Action<IOrOptions<T>>[] orOperations);
    }
}