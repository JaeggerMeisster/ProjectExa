﻿using System;
using System.IO;

namespace Exa.IO
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Deserialize items from directory and add to collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="directory"></param>
        public static void LoadJsonCollectionFromDirectory<T>(string directory, Action<T> callback)
            where T : class
        {
            if (!Directory.Exists(directory)) return;

            foreach (var filePath in Directory.GetFiles(directory, "*.json"))
            {
                T item;

                IOUtils.TryJsonDeserializeFromPath(filePath, out item);

                callback(item);
            }
        }
    }
}