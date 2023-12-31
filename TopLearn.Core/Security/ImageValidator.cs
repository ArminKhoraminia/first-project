﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile img)
        {
            try
            {
                var tmpimg = System.Drawing.Image.FromStream(img.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
