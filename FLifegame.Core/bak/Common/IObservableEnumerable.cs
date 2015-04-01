using System.Collections.Generic;
using System.Collections.Specialized;

namespace FLifegame.Common
{
    public interface IObservableEnumerable<T> : IEnumerable<T>, INotifyCollectionChanged
    {}
}

