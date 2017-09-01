﻿using System;
using System.IO;

namespace iTextBuildingBlocks
{
    public static class Paths
    {
        public static string ResultsPath =>
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../results"));

        public static string FontResourcesPath => 
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../resources/fonts"));

        public static string TextResourcesPath =>
            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../resources/txt"));
    }
}