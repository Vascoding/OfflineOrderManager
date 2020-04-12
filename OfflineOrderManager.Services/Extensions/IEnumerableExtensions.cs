using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineOrderManager.Services.Extensions
{
    public static class IEnumerableExtensions
    {
        public static async Task<IEnumerable<T>> ToListAsync<T>(this IEnumerable<T> list) => 
            await Task.Run(() => list.ToList());
    }
}
