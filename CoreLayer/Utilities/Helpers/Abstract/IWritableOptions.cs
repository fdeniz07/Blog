using Microsoft.Extensions.Options;
using System;

namespace CoreLayer.Utilities.Helpers.Abstract
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges); // (x=>x.Header = "Yeni Baslik") x=> {x.Header="Yeni Baslik; x.Content="Yeni Icerik"}
    }
}
