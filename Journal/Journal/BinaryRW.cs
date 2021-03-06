﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Journal
{
    class BinaryRW
    {
        public byte[] ReadBytes(string source)
        {
            try
            {
                return File.ReadAllBytes(source);
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}",
                                 _Exception.ToString());
            }
            return null;
        }
        public bool writeBytes(byte[] _write, string source)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(source, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_write, 0, _write.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
        public bool writeString(string _write, string _source)
        {
            if (_write.Length > 0 && _source.Length > 0)
            {
                File.WriteAllText(_source, _write);
                return true;
            }
            else
                return false;
        }
        public string readString(string _source)
        {
            try
            {
                if (_source.Length > 0)
                {
                    string _read = File.ReadAllText(_source);
                    return _read;
                }
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}",
                                 _Exception.ToString());
            }
                return null;
        }
    }
}
