﻿using System;

namespace Moonlight.Utility.Conversion
{
    public interface IConversionFactory
    {
        /// <summary>
        ///     Convert selected to string to object of defined type
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <param name="type">target type</param>
        /// <returns>object converted</returns>
        object ToObject(string value, Type type);

        T ToObject<T>(string value);

        /// <summary>
        ///     Convert selected object to string
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <param name="type">type of the object</param>
        /// <returns>string converted</returns>
        string ToString(object value, Type type);

        string ToString<T>(T value);
    }
}