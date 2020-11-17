// ReSharper disable CheckNamespace

namespace System
{
    using System.ArrayExtensions;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    namespace ArrayExtensions
    {
        public static class ArrayExtensions
        {
            [DebuggerHidden]
            public static void ForEach(this Array array, Action<Array, int[]> action)
            {
                if (array.LongLength == 0)
                    return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do
                    action(array, walker.Position);
                while (walker.Step());
            }
        }

        internal class ArrayTraverse
        {
            public int[] Position;

            private int[] maxLengths;

            public ArrayTraverse(Array array)
            {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }

    /// <summary>Class ObjectExtensions.</summary>
    /// <seealso href="https://github.com/Burtsev-Alexey/net-object-deep-copy" >Github : Burtsev-Alexey / net-object-deep-copy</seealso>
    /// <seealso href="https://stackoverflow.com/a/11308879/752515">Stack Overflow : How do you do a deep copy of an object in .NET? [duplicate]</seealso>
    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>Perform fast object cloning.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">The original object to copy.</param>
        /// <returns>A deep copy of the original object.</returns>
        [DebuggerHidden]
        public static T Copy<T>(this T original)
        {
            return (T)Copy((Object)original);
        }

        /// <summary>Perform a deep copy of an object.</summary>
        /// <param name="originalObject">The original object to copy.</param>
        /// <returns>A deep copy of the original object.</returns>
        [DebuggerHidden]
        public static Object Copy(this Object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }

        /// <summary>Gets a value indicating whether the System.Type is one of the primitive types.</summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c>if the System.Type is one of the primitive types; otherwise, <c>false</c>.</returns>
        [DebuggerHidden]
        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String))
                return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false)
                    continue;
                if (IsPrimitive(fieldInfo.FieldType))
                    continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }

        private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            if (originalObject == null)
                return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect))
                return originalObject;
            if (visited.ContainsKey(originalObject))
                return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect))
                return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }
            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        [DebuggerHidden]
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        [DebuggerHidden]
        public override int GetHashCode(object obj)
        {
            if (obj == null)
                return 0;
            return obj.GetHashCode();
        }
    }
}