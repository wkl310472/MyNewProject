﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyNewProject.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool isSupported(string fileName)
        {
            return AcceptedFileTypes.Any(t => t == Path.GetExtension(fileName).ToLower());
        }
    }
}
