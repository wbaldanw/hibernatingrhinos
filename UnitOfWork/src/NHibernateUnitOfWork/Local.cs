using System;
using System.Collections;

namespace NHibernateUnitOfWork
{
    public static class Local
    {
        static readonly ILocalData _data = new LocalData();

        public static ILocalData Data
        {
            get { return _data; }
        }

        private class LocalData : ILocalData
        {
            [ThreadStatic]
            private static Hashtable _localData;

            private static Hashtable LocalHashtable
            {
                get 
                {
                    if (_localData == null)
                        _localData = new Hashtable();
                    return _localData;
                }
            }

            public object this[object key]
            {
                get { return LocalHashtable[key]; }
                set { LocalHashtable[key] = value; }
            }

            public int Count
            {
                get { return LocalHashtable.Count; }
            }

            public void Clear()
            {
                LocalHashtable.Clear();
            }
        }
    }
}