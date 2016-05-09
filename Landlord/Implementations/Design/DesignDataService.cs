﻿using Landlord.Interface;
using Landlord.Model;
using System;
using System.IO;

namespace Landlord.Implementations.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }

        private static byte[] StreamFile(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                // Create a byte array of file stream length
                var data = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(data, 0, Convert.ToInt32(fs.Length));

                return data; //return the byte data
            }
        }
    }
}