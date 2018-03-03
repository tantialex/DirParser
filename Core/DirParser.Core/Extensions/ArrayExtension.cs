using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core.Extensions {
    public static class ArrayExtension {
        public static T[] ForEachReverse<T>(this T[] arr, Action<T> action) {
            for(int i = arr.Length-1; i >= 0; i--) {
                action(arr[i]);
            }

            return arr;
        }
    }
}
