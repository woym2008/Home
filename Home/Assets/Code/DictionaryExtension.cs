using System;
using System.Collections.Generic;

namespace PTGame.Common
{ 
    public class DictionaryEx<TKey, TValue> : Dictionary<TKey, TValue>    
    {
        public new TValue this[TKey indexKey]
        {
            set { base[indexKey] = value; }
            get
            {
                try
                {
                    return base[indexKey];
                }
                catch (Exception)
                {
                    return default(TValue);
                }
            }
        }
    }
}