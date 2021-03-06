﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Carbon.Css
{
    using Color;

    public static class CssFunctions
	{
		// http://lesscss.org/functions/#color-operations-saturate

		private static readonly Dictionary<string, Func<CssValue[], CssValue>> dic = new Dictionary<string, Func<CssValue[], CssValue>>(9) {
			["darken"]		= Darken,
			["lighten"]		= Lighten,
			["saturate"]	= Saturate,
			["desaturate"]	= Desaturate,
			["adjust-hue"]	= AdjustHue,
			["mix"]			= Mix,
			["rgba"]		= Rgba,
            ["if"]			= If
        };

        // red(color)
        // green(color)
        // blue(blue)
        // hsl
        // hsla
        // hue

        public static bool TryGet(string name, out Func<CssValue[], CssValue> func)
		    => dic.TryGetValue(name, out func);
		
		// if($condition, $if-true, $if-false)
		public static CssValue If(CssValue[] args) => ToBoolean(args[0]) ? args[1] : args[2];

        public static CssValue Mix(CssValue[] args)
		{
            Rgba32 color1 = GetColor(args[0]);
            Rgba32 color2 = GetColor(args[1]);
            
            double amount = args.Length == 3 ? GetAmount(args[2]) : 0.5;

			return new CssColor(color1.BlendWith(color2, (float)amount));
		}

		public static CssValue Saturate(CssValue[] args)
		{
            Rgba32 color = GetColor(args[0]);
			var amount = GetAmount(args[1]);

			return new CssColor(color.Saturate((float)amount));
		}

		public static CssValue Desaturate(CssValue[] args)
		{
            Rgba32 color	= GetColor(args[0]);
			var amount	= GetAmount(args[1]);

			return new CssColor(color.Desaturate((float)amount));
		}

		public static CssValue Lighten(CssValue[] args)
		{
            Rgba32 color = GetColor(args[0]);
			var amount = GetAmount(args[1]);

			return new CssColor(color.Lighten((float)amount));
		}

		public static CssValue Darken(CssValue[] args)
		{
            Rgba32 color = GetColor(args[0]);
			var amount = GetAmount(args[1]);

			return new CssColor(color.Darken((float)amount));
		}

		public static CssValue AdjustHue(CssValue[] args)
		{
            Rgba32 color = GetColor(args[0]);
			var amount = GetAmount(args[1]);

			return new CssColor(color.ToHsla().RotateHue((float)amount * 360).ToRgba32());
		}

		public static CssValue Rgba(CssValue[] args)
		{
			if (args.Length == 4)
			{
				return new CssFunction("rgba", new CssValueList(args));
			}

			var color = Rgba32.Parse(args[0].ToString());
			
			return CssColor.FromRgba(color.R, color.G, color.B, float.Parse(args[1].ToString()));
		}

        #region Helpers

        private static bool ToBoolean(CssValue value)
        {
            if (value is CssBoolean cssBoolean)
            {
                return cssBoolean.Value;
            }

            var text = value.ToString();

            return text == "true" || text == "True";
        }

		private static Rgba32 GetColor(CssValue value) => Rgba32.Parse(value.ToString());

        private static double ParseDouble(string text)
        {
            if (text[text.Length - 1] == 'd')
            {
                text = text.Replace("deg", string.Empty);
            }

            return double.Parse(text, CultureInfo.InvariantCulture);
        }

		private static double GetAmount(CssValue value) => value.Kind switch
		{
			NodeKind.Angle      => (ParseDouble(value.ToString()) % 360) / 360,
			NodeKind.Percentage => (((CssUnitValue)value).Value / 100d),
			NodeKind.Number     => ((CssUnitValue)value).Value,
			_                   => throw new Exception("Unknown numeric value: " + value.Kind + ":" +  value)
		};
		
		#endregion
	}
}