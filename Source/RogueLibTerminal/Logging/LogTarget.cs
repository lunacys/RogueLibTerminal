﻿using System;
using System.IO;

namespace RogueLibTerminal.Logging
{
    [Flags]
    public enum LogTarget
    {
        None = 0,
        /// <summary>
        /// Console target provided by <see cref="Console"/> class
        /// </summary>
        Console = 1,
        /// <summary>
        /// File target provided by <see cref="FileStream"/> class
        /// </summary>
        File = 2,
    }
}

